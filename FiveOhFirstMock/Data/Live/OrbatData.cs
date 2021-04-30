using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Live
{
    public class OrbatData
    {
        public HailstormData Hailstorm { get; set; } = new();
        public CompanyData Avalanche { get; set; } = new();
        public CompanyData Cyclone { get; set; } = new();
        public CompanyData Acklay { get; set; } = new();
        public MynockDetachmentData Mynock { get; set; } = new();
        public RazorSquadronData Razor { get; set; } = new();
        public MilitaryPolice MilitaryPolice { get; set; } = new();
        public ChiefWarrantOfficerData ChiefWarrantOfficers { get; set; } = new();

        public void AssignTrooper(Trooper t)
        {
            if (t.Slot == Enums.Slot.Hailstorm)
                Hailstorm.AssignTrooper(t);
            else if (t.Slot < Enums.Slot.CycloneCompany && t.Slot >= Enums.Slot.AvalancheCompany)
                Avalanche.AssignTrooper(t);
            else if (t.Slot < Enums.Slot.AcklayCompany && t.Slot >= Enums.Slot.CycloneCompany)
                Cyclone.AssignTrooper(t);
            else if (t.Slot < Enums.Slot.Mynock && t.Slot >= Enums.Slot.AcklayCompany)
                Acklay.AssignTrooper(t);
            else if (t.Slot < Enums.Slot.Razor && t.Slot >= Enums.Slot.Mynock)
                Mynock.AssignTrooper(t);
            else if (t.Slot < Enums.Slot.ActiveReserve && t.Slot < Enums.Slot.Razor)
                Razor.AssignTrooper(t);
        }
    }
}
