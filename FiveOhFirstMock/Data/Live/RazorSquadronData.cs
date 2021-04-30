using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class RazorSquadronData
    {
        public Trooper Commander { get; set; }
        public Trooper SubCommander { get; set; }
        public RazorFlightData[] Flights { get; set; } = new RazorFlightData[] { new(), new(), new(), new(), new() };
        public WardenData Warden { get; set; } = new();

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
                    case Role.SubCommander:
                        SubCommander = t;
                        break;
                }
            }
            else if(val >= 1 && val <= Flights.Length)
            {
                Flights[val - 1].AssignTrooper(t);
            }
            else if(val == Flights.Length + 1)
            {
                Warden.AssignTrooper(t);
            }
        }
    }
}
