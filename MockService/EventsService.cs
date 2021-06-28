using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockService
{
    public class EventsService
    {
        public EventsService(IList<Event> events)
        {
            Events = events;
        }

        public IList<Event> Events { get; set; }
    }
}
