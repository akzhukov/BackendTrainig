using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository repository;
        private readonly IMapper mapper;

        public EventsController(IEventRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Event>> GetByUnitId([FromQuery] int unitId = 1, int take = 1, int skip = 0)
        {
            return Ok(mapper.Map<IList<EventDto>>(repository.GetByUnitId(unitId, take, skip)));
        }

        [HttpPut("{id}")]
        public IActionResult Update(EventDto eventDto, int id)
        {
            var oldItem = repository.Get(id);
            if (oldItem == null)
            {
                return NotFound($"Failed to update event. Could not find event with ID = {id}");
            }
            var item = mapper.Map<Event>(eventDto);
            item.Id = id;
            return Ok(repository.Update(item));
        }
    }
}
