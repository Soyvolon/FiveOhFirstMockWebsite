using FiveOhFirstMock.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data
{
    public class Trooper : IdentityUser<int>
    {
        public Role Role { get; set; }
        public Slot Slot { get; set; }
        public Team Team { get; set; }
        public Flight? Flight { get; set; } = null;
        public Qualifications Qualifications { get; set; }
    }
}
