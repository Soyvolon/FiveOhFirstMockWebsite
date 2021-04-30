using FiveOhFirstMock.Data;
using FiveOhFirstMock.Data.Live;
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
    }
}
