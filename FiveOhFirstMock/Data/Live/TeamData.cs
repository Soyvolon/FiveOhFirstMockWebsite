using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class TeamData
    {
        public Trooper Lead { get; set; }
        public List<Trooper> Troopers { get; set; } = new();
        public Trooper Medic { get; set; }

        public void AssignTrooper(Trooper t)
        {
            switch(t.Role)
            {
                case Role.Lead:
                    Lead = t;
                    break;
                case Role.Trooper:
                    Troopers.Add(t);
                    break;
                case Role.Medic:
                    Medic = t;
                    break;
            }
        }
    }
}
