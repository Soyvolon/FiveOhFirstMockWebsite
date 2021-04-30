using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class RazorFlightData
    {
        public Trooper Commander { get; set; }
        public RazorSectionData[] Sections { get; set; } = new RazorSectionData[] { new(), new() };

        public void AssignTrooper(Trooper t)
        {
            switch(t.Team)
            {
                case Team.NA:
                    if (t.Role == Role.Commander)
                        Commander = t;
                    break;
                case Team.Alpha:
                    Sections[0].AssignTrooper(t);
                    break;
                case Team.Bravo:
                    Sections[1].AssignTrooper(t);
                    break;
            }
        }
    }

    public class RazorSectionData
    {
        public Trooper Alpha { get; set; }
        public Trooper Bravo { get; set; }
        public Trooper Charlie { get; set; }
        public Trooper Delta { get; set; }

        public void AssignTrooper(Trooper t)
        {
            switch (t.Flight)
            {
                case Flight.Alpha:
                    Alpha = t;
                    break;
                case Flight.Bravo:
                    Bravo = t;
                    break;
                case Flight.Charlie:
                    Charlie = t;
                    break;
                case Flight.Delta:
                    Delta = t;
                    break;
            }
        }
    }
}
