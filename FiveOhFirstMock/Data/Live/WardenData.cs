using FiveOhFirstMock.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class WardenData
    {
        public Trooper Master { get; set; }
        public Trooper Chief { get; set; }
        public List<Trooper> Wardens { get; set; } = new();

        public void AssignTrooper(Trooper t)
        {
            switch(t.Role)
            {
                case Role.MasterWarden:
                    Master = t;
                    break;
                case Role.CheifWarden:
                    Chief = t;
                    break;
                case Role.Warden:
                    Wardens.Add(t);
                    break;
            }
        }
    }
}
