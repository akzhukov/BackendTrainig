using SocketsShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProviders
{
    public class DataProviderFactory : IDataProviderFactory
    {
        private Dictionary<HubType, IDataProvider> dataProvidersDict = new();

        public DataProviderFactory(IEnumerable<IDataProvider> dataProviders)
        {
            dataProvidersDict.Add(HubType.LinePageChart, dataProviders.FirstOrDefault(prov => prov.GetType() == typeof(LinePageDataProvider)));
            dataProvidersDict.Add(HubType.SimpleChart, dataProviders.FirstOrDefault(prov => prov.GetType() == typeof(SimpleChartDataProvider)));
        }

        public IDataProvider GetProvider(HubType type)
        {
            return dataProvidersDict[type];
        }
    }
}
