using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class HailstormData
    {
        public Trooper Commander { get; set; }
        public Trooper XO { get; set; }
        public Trooper CShopCommander { get; set; }
        public Trooper CShopXO { get; set; }
        public Trooper NCOIC { get; set; }
        public Trooper Medic { get; set; }
        public Trooper RT { get; set; }

        public void AssignTrooper(Trooper t)
        {
            switch(t.Role)
            {
                case Enums.Role.Commander:
                    Commander = t;
                    break;
                case Enums.Role.XO:
                    XO = t;
                    break;
                case Enums.Role.CShopCommander:
                    CShopCommander = t;
                    break;
                case Enums.Role.CShopXO:
                    CShopXO = t;
                    break;
                case Enums.Role.NCOIC:
                    NCOIC = t;
                    break;
                case Enums.Role.Medic:
                    Medic = t;
                    break;
                case Enums.Role.RTO:
                    RT = t;
                    break;
            }
        }
    }
}
