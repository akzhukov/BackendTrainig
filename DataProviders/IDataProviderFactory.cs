using SocketsShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProviders
{
    public interface IDataProviderFactory
    {
        IDataProvider GetProvider(HubType type);
    }
}
