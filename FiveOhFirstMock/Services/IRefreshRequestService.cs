using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Services
{
    public interface IRefreshRequestService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh();
    }
}
