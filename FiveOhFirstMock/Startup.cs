using FiveOhFirstMock.Areas.Identity;
using FiveOhFirstMock.Data;
using FiveOhFirstMock.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FiveOhFirstMock
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var Secrets = new ConfigurationBuilder()
                .AddJsonFile(Path.Join("Config", "website_config.json"))
                .Build();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Secrets.GetConnectionString("database")));
            services.AddIdentity<Trooper, TrooperRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddRoleManager<TrooperRoleManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSlotted",
                    policy =>
                    {
                        policy.RequireAssertion(ctx =>
                        {
                            return ctx.User.HasClaim(x => x.Type == "Slotted")
                                || ctx.User.IsInRole("Admin");
                        });
                    });
                options.AddPolicy("RequireAdmin",
                    policy =>
                    {
                        policy.RequireRole("Admin");
                    });
                options.AddPolicy("RequireTrooper",
                    policy =>
                    {
                        policy.RequireRole("Trooper", "Admin");
                    });
                options.AddPolicy("RequireClerk",
                    policy =>
                    {
                        policy.RequireAssertion(ctx =>
                        {
                            return ctx.User.HasClaim(x => x.Type == "Clerk")
                                || ctx.User.IsInRole("Admin");
                        });
                    });
                options.AddPolicy("RequireNCO",
                    policy =>
                    {
                        policy.RequireRole("Admin", "NCO");
                    });
            });

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Trooper>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;
            });

            #region Roster
            services.AddScoped<RosterService>();
            #endregion

            #region Example Tools
            services.AddScoped<TestDataService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            ApplyDatabaseMigrations(db);

            var roleManager = scope.ServiceProvider.GetRequiredService<TrooperRoleManager>();
            roleManager.SeedRoles<WebsiteRoles>().GetAwaiter().GetResult();

            var data = scope.ServiceProvider.GetRequiredService<TestDataService>();
            data.InitalizeAsync().GetAwaiter().GetResult();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void ApplyDatabaseMigrations(DbContext database)
        {
            if (!(database.Database.GetPendingMigrations()).Any())
            {
                return;
            }

            database.Database.Migrate();
            database.SaveChanges();
        }
    }
}
