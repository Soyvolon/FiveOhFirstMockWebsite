using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Enums
{
    [Flags]
    [TypeConverter(typeof(QualificationTypeConverter))]
    public enum Qualifications : long
    {
        None = 0x0000000000,
        Zeus = 0x0000000001, // 1 << 0
        RTO = 0x0000000002, // 1 << 1
        Assault = 0x0000000004, // 1 << 2
        Marksman = 0x0000000008, // 1 << 3
        Grenadier = 0x0000000010, // 1 << 4
        Support = 0x0000000020, // 1 << 5
        Medic = 0x0000000040, // 1 << 6
        Jumpmaster = 0x0000000080, // 1 << 7
        CombatEngineer = 0x0000000100 // 1 << 8
    }

    public static class QualificationsExtensions
    {
        public static Qualifications GetAllowedQualifications(this ClaimsPrincipal claim)
        {
            Qualifications output = Qualifications.None;
            foreach(var qual in Enum.GetValues<Qualifications>())
            {
                switch (qual)
                {
                    case Qualifications.Zeus:
                        if (claim.HasClaim("Instructor", "Zeus") || claim.IsInRole("Admin"))
                            output |= Qualifications.Zeus;
                        break;
                    case Qualifications.RTO:
                        if (claim.HasClaim("Instructor", "RTO") || claim.IsInRole("Admin"))
                            output |= Qualifications.RTO;
                        break;
                    case Qualifications.Assault:
                        if (claim.HasClaim("Instructor", "Assault") || claim.IsInRole("Admin"))
                            output |= Qualifications.Assault;
                        break;
                    case Qualifications.Marksman:
                        if (claim.HasClaim("Instructor", "Marksman") || claim.IsInRole("Admin"))
                            output |= Qualifications.Marksman;
                        break;
                    case Qualifications.Grenadier:
                        if (claim.HasClaim("Instructor", "Grenadier") || claim.IsInRole("Admin"))
                            output |= Qualifications.Grenadier;
                        break;
                    case Qualifications.Support:
                        if (claim.HasClaim("Instructor", "Support") || claim.IsInRole("Admin"))
                            output |= Qualifications.Support;
                        break;
                    case Qualifications.Medic:
                        if (claim.HasClaim("Instructor", "Medic") || claim.IsInRole("Admin"))
                            output |= Qualifications.Medic;
                        break;
                    case Qualifications.Jumpmaster:
                        if (claim.HasClaim("Instructor", "Jumpmaster") || claim.IsInRole("Admin"))
                            output |= Qualifications.Jumpmaster;
                        break;
                    case Qualifications.CombatEngineer:
                        if (claim.HasClaim("Instructor", "Combat Engineer") || claim.IsInRole("Admin"))
                            output |= Qualifications.CombatEngineer;
                        break;
                }
            }

            return output;
        }
    }
}
