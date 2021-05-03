using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data
{
    public class RosterSearchData
    {
        public string IdFilter { get; set; } = "";
        public string UsernameFilter { get; set; } = "";
        public string UnitFilter { get; set; } = "";
        public string RoleFilter { get; set; } = "";
        public bool Ascending { get; set; } = true;
    }
}
