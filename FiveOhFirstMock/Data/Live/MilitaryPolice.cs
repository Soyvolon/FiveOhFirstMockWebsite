using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class MilitaryPolice
    {
        public List<Trooper> CoLeadMP { get; set; } = new();
        public List<Trooper> MP { get; set; } = new();
    }
}
