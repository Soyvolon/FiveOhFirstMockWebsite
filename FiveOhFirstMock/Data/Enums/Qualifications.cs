using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Enums
{
    [Flags]
    [TypeConverter(typeof(QualificationTypeConverter))]
    public enum Qualifications : long
    {
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
}
