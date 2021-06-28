using AutoMapper;
using Lesson2.Validation;
using Microsoft.AspNetCore.Mvc;
using Shared;
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
    public class TanksController : ControllerBase
    {
        protected readonly ITankRepository repository;
        private readonly IMapper mapper;
        private readonly TankValidator validator;

        public TanksController(ITankRepository repository, IMapper mapper, TankValidator validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<IList<Tank>> GetAll()
        {
            return Ok(mapper.Map<IList<TankDto>>(repository.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound($"Tank with ID {id} not found.");
            }
            return Ok(mapper.Map<TankDto>(item));
        }

        [HttpPost]
        public IActionResult Add(TankCreateUpdateDto itemDto)
        {
            var res = validator.Validate(itemDto);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors.Select(v => v.ErrorMessage));
            }
            var item = mapper.Map<Tank>(itemDto);
            return Ok(mapper.Map<TankDto>(repository.Create(item)));
        }

        [HttpPut("{id}")]
        public IActionResult Update(TankCreateUpdateDto itemDto, int id)
        {
            var res = validator.Validate(itemDto);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors.Select(v => v.ErrorMessage));
            }
            var oldItem = repository.Get(id);
            if (oldItem == null)
            {
                return NotFound($"Failed to update tank. Could not find tank with ID = {id}");
            }
            var item = mapper.Map<Tank>(itemDto);
            item.Id = id;
            return Ok(repository.Update(item));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = repository.Delete(id);
            if (item == null)
            {
                return NotFound($"Failed to delete tank. Could not find tank with ID = {id}");
            }
            return Ok(item);
        }


    }
}
