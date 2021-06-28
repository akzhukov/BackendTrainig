using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Models;
using Shared.Services.Json.Services;
using Microsoft.AspNetCore.Authorization;

namespace MockService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EventsController : ControllerBase
    {
        private readonly EventsService eventsService;

        public EventsController(EventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        [HttpGet]
        [Route("keys")]
        public ActionResult<IList<int>> GetEventIdsByUnit([FromQuery] int unitId = 1, int take = 3, int skip = 0)
        {
            if (take > 100)
            {
                return BadRequest("Event limit overflowed");
            }
            var events = eventsService.Events;
            return Ok(events.Where(e => e.UnitId == unitId).Skip(skip).Take(take).Select(e => e.Id));
        }

        [HttpPost]
        public ActionResult<IList<Event>> GetEventsByIds([FromBody] int[] ids)
        {
            var events = eventsService.Events;
            return Ok(events.Where(e => ids.Contains(e.Id)));
        }

    }
}
