using SocketsShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketsService.Hubs;

namespace WebSocketsService.Mapper
{
    public static class HubMapper
    {
        public static HubDto Map(this BaseHub hub)
        {
            return new HubDto
            {
                guid = hub.guid.ToString(),
                type = hub.hubType.ToString(),
                name = hub.GetType().Name
            };
        }

        public static IEnumerable<HubDto> Map(this IEnumerable<BaseHub> hubs)
        {
            List<HubDto> listHubs = new();
            foreach(var hub in hubs)
            {
                listHubs.Add(hub.Map());
            }
            return listHubs;
        }
    }
}
