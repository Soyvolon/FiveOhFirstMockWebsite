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
using System.Security.Claims;
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

        public async Task<(SquadData?, string?)> GetSquadAsync(ClaimsPrincipal claim)
        {
            var scope = _services.CreateScope();
            var manager = scope.ServiceProvider.GetRequiredService<UserManager<Trooper>>();

            var user = await manager.GetUserAsync(claim);

            if (user.Slot < Slot.Mynock && (int)user.Slot % 10 != 0)
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var users = new HashSet<Trooper>();
                await db.Users.AsNoTracking().ForEachAsync(x =>
                {
                    if (x.Slot == user.Slot)
                        users.Add(x);
                });

                var data = new SquadData();
                foreach (var t in users)
                    data.AssignTrooper(t);

                return (data, user.Slot.ToString());
            }

            return (null, null);
        }

        public async Task<Trooper?> GetTrooperAsync(string rawId)
        {
            var scope = _services.CreateScope();
            var manager = scope.ServiceProvider.GetRequiredService<UserManager<Trooper>>();
            var user = await manager.FindByIdAsync(rawId);
            return user;
        }

        public async Task UpdateAsync(Trooper updateData, bool clerk = false)
        {
            var scope = _services.CreateScope();
            var manager = scope.ServiceProvider.GetRequiredService<UserManager<Trooper>>();
            var user = await manager.FindByIdAsync(updateData.Id.ToString());

            if(user is not null)
            {
                user.Slot = updateData.Slot;
                user.Team = updateData.Team;
                user.Role = updateData.Role;
                user.Flight = updateData.Flight;
                user.Qualifications = updateData.Qualifications;

                await manager.UpdateAsync(user);

                var claims = await manager.GetClaimsAsync(user);
                var slotClaim = claims.FirstOrDefault(x => x.Type == "Slotted");
                if (user.Slot >= Slot.InactiveReserve)
                {
                    if(slotClaim is not null)
                    {
                        await manager.RemoveClaimAsync(user, slotClaim);
                    }
                }
                else if (user.Slot <= Slot.InactiveReserve)
                {
                    if(slotClaim is null)
                    {
                        await manager.AddClaimAsync(user, new("Slotted", DateTime.Now.ToShortDateString()));
                    }
                    else
                    {
                        await manager.ReplaceClaimAsync(user, slotClaim, new("Slotted", DateTime.Now.ToShortDateString()));
                    }
                }

                var clerkClaim = claims.FirstOrDefault(x => x.Type == "Clerk");
                if(clerk)
                {
                    if(clerkClaim is null)
                        await manager.AddClaimAsync(user, new("Clerk", "Roster"));
                }
                else
                {
                    if (clerkClaim is not null)
                        await manager.RemoveClaimAsync(user, clerkClaim);
                }
            }
        }
    }
}
