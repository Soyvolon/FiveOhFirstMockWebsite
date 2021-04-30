using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class PlatoonData
    {
        public Trooper Commander { get; set; }
        public Trooper SergeantMajor { get; set; }
        public Trooper Medic { get; set; }
        public Trooper RT { get; set; }
        public Trooper ARC { get; set; }
        public SquadData[] Squads { get; set; } = new SquadData[] { new(), new(), new() };

        public void AssignTrooper(Trooper t)
        {
            var val = (int)t.Slot % 10;
            if(val == 0)
            {
                switch(t.Role)
                {
                    case Role.Commander:
                        Commander = t;
                        break;
                    case Role.SergeantMajor:
                        SergeantMajor = t;
                        break;
                    case Role.Medic:
                        Medic = t;
                        break;
                    case Role.RTO:
                        RT = t;
                        break;
                    case Role.ARC:
                        ARC = t;
                        break;
                }
            }
            else if (val >= 1 && val <= Squads.Length)
            {
                Squads[val - 1].AssignTrooper(t);
            }
        }
    }
}
