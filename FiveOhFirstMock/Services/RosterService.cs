using FiveOhFirstMock.Data;
using FiveOhFirstMock.Data.Enums;
using FiveOhFirstMock.Data.Live;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Services
{
    public class RosterService
    {
        private readonly IServiceProvider _services;

        public RosterService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task<List<Trooper>> GetFullRosterAsync()
        {
            var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            List<Trooper> troopers = new();
            await db.Users.AsNoTracking().ForEachAsync(x => troopers.Add(x));
            return troopers;
        }

        public async Task<OrbatData> GetOrbatAsymc()
        {
            var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            OrbatData data = new();

            await db.Users.AsNoTracking().ForEachAsync(x =>
            {
                if (((int)x.Slot) < 500)
                {
                    data.AssignTrooper(x);
                }
            });

            return data;
        }

        public async Task UpdateAsync(Trooper updateData, bool clerk = false)
        {
            var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UserManager<Trooper>>();
            var user = await db.FindByIdAsync(updateData.Id.ToString());

            if(user is not null)
            {
                user.Slot = updateData.Slot;
                user.Team = updateData.Team;
                user.Role = updateData.Role;
                user.Flight = updateData.Flight;
                user.CShops = updateData.CShops;

                await db.UpdateAsync(user);

                var claims = await db.GetClaimsAsync(user);
                var slotClaim = claims.FirstOrDefault(x => x.Type == "Slotted");
                if (user.Slot >= Slot.InactiveReserve)
                {
                    if(slotClaim is not null)
                    {
                        await db.RemoveClaimAsync(user, slotClaim);
                    }
                }
                else if (user.Slot <= Slot.InactiveReserve)
                {
                    if(slotClaim is null)
                    {
                        await db.AddClaimAsync(user, new("Slotted", DateTime.Now.ToShortDateString()));
                    }
                    else
                    {
                        await db.ReplaceClaimAsync(user, slotClaim, new("Slotted", DateTime.Now.ToShortDateString()));
                    }
                }

                var clerkClaim = claims.FirstOrDefault(x => x.Type == "Clerk");
                if(clerk)
                {
                    if(clerkClaim is null)
                        await db.AddClaimAsync(user, new("Clerk", "Roster"));
                }
                else
                {
                    if (clerkClaim is not null)
                        await db.RemoveClaimAsync(user, clerkClaim);
                }
            }
        }
    }
}
