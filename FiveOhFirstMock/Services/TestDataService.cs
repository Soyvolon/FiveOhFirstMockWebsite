using FiveOhFirstMock.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Services
{
    public class TestDataService
    {
        private readonly UserManager<Trooper> _manager;

        public TestDataService(UserManager<Trooper> manager)
        {
            _manager = manager;
        }

        public async Task InitalizeAsync()
        {
            var data = TestData;
            foreach (var set in data)
            {
                var trooper = await EnsureUser(set.Item1, "foo");
                await _manager.AddClaimsAsync(trooper, set.Item2);
                await _manager.AddToRolesAsync(trooper, set.Item3);
            }
        }

        private async Task<Trooper> EnsureUser(Trooper trooper, string password)
        {
            var user = await _manager.FindByIdAsync(trooper.Id.ToString());
            if(user is null)
            {
                user = trooper;
                await _manager.CreateAsync(user, password);
            }

            if(user is null)
            {
                throw new Exception("The password is probaly not strong enough!");
            }

            return user;
        }

        private static List<(Trooper, List<Claim>, List<string>)> TestData {
            get
            {
                return new()
                {
                    (new Trooper()
                    {
                        Id = 42127,
                        UserName = "Soyvolon",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.RTO,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.NA
                    },
                    new()
                    {
                        new("Slotted", "SomeDate"),
                        new("Instructor", "RTO")
                    },
                    new()
                    {
                        "Trooper",
                        "RTO",
                        "Admin"
                    }),
                    (new Trooper()
                    {
                        Id = 11345,
                        UserName = "Del",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Lead,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.NA
                    },
                    new()
                    {
                        new("Slotted", "SomeDate"),
                        new("Instructor", "Grenadier"),
                    },
                    new()
                    {
                        "Trooper",
                        "NCO"
                    }),
                    (new Trooper()
                    {
                        Id = 70303,
                        UserName = "Crebar",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Lead,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Alpha
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper",
                        "NCO"
                    }),
                    (new Trooper()
                    {
                        Id = 56273,
                        UserName = "Knight",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Trooper,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Alpha
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper"
                    }),
                    (new Trooper()
                    {
                        Id = 30253,
                        UserName = "Chimera",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Trooper,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Alpha
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper"
                    }),
                    (new Trooper()
                    {
                        Id = 23996,
                        UserName = "Deytow",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Medic,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Alpha
                    },
                    new()
                    {
                        new("Slotted", "SomeDate"),
                        new("Instructor", "Medic")
                    },
                    new()
                    {
                        "Trooper",
                        "Medic"
                    }),
                    (new Trooper()
                    {
                        Id = 54077,
                        UserName = "Klinger",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Lead,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Bravo
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper",
                        "NCO"
                    }),
                    (new Trooper()
                    {
                        Id = 14577,
                        UserName = "Clover",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Trooper,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Bravo
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper"
                    }),
                    (new Trooper()
                    {
                        Id = 42361,
                        UserName = "Negeta",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Trooper,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Bravo
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper"
                    }),
                    (new Trooper()
                    {
                        Id = 87071,
                        UserName = "Bones",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Medic,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.Bravo
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper",
                        "Medic"
                    }),
                    (new Trooper()
                    {
                        Id = 17077,
                        UserName = "Tal",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.ARC,
                        Slot = Data.Enums.Slot.AvalancheThreeThree,
                        Team = Data.Enums.Team.NA
                    },
                    new()
                    {
                        new("Slotted", "SomeDate")
                    },
                    new()
                    {
                        "Trooper",
                        "ARC"
                    }),
                    (new Trooper()
                    {
                        Id = 123456,
                        UserName = "Reserve",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Trooper,
                        Slot = Data.Enums.Slot.ActiveReserve,
                        Team = Data.Enums.Team.NA
                    },
                    new()
                    {

                    },
                    new()
                    {
                        "Trooper"
                    }),
                    (new Trooper()
                    {
                        Id = 23456,
                        UserName = "Clerk",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Data.Enums.Role.Trooper,
                        Slot = Data.Enums.Slot.InactiveReserve,
                        Team = Data.Enums.Team.NA
                    },
                    new()
                    {
                        new("Clerk", "Roster")
                    },
                    new()
                    {
                        "Trooper"
                    }),
                };
            }
        }
    }
}
