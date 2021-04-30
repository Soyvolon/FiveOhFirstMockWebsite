using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class MynockSeectionData
    {
        public Trooper Lead { get; set; }
        public Trooper RT { get; set; }
        public TeamData[] Teams { get; set; } = new TeamData[] { new(), new() };

        public void AssignTrooper(Trooper t)
        {
            switch (t.Team)
            {
                case Team.NA:
                    switch (t.Role)
                    {
                        case Role.Lead:
                            Lead = t;
                            break;
                        case Role.RTO:
                            RT = t;
                            break;
                    }
                    break;
                case Team.Alpha:
                    Teams[0].AssignTrooper(t);
                    break;
                case Team.Bravo:
                    Teams[1].AssignTrooper(t);
                    break;
            }
        }
    }
}
