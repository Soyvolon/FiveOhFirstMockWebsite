using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class MynockDetachmentData
    {
        public Trooper Commander { get; set; }
        public Trooper NCOIC { get; set; }
        public Trooper Medic { get; set; }
        public Trooper RT { get; set; }
        public MynockSeectionData[] Sections { get; set; } = new MynockSeectionData[] { new(), new(), new() };

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
                    case Role.NCOIC:
                        NCOIC = t;
                        break;
                    case Role.Medic:
                        Medic = t;
                        break;
                    case Role.RTO:
                        RT = t;
                        break;
                }
            }
            else if (val >= 1 && val <= Sections.Length)
            {
                Sections[val - 1].AssignTrooper(t);
            }
        }
    }
}
