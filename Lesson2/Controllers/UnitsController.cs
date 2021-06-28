using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Data;
using Shared.Models;
using Shared;
using Shared.Repository;
using AutoMapper;
using Shared.DTOs;
using Lesson2.Validation;

namespace Lesson2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        protected readonly IUnitRepository repository;
        private readonly IMapper mapper;
        private readonly UnitValidator validator;

        public UnitsController(IUnitRepository repository, IMapper mapper, UnitValidator validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<IList<Tank>> GetAll()
        {
            return Ok(mapper.Map<IList<UnitDto>>(repository.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = repository.GetUnitWithTanks(id);
            if (item == null)
            {
                return NotFound($"Unit with ID {id} not found.");
            }
            return Ok(mapper.Map<UnitDto>(item));
        }

        [HttpPost]
        public IActionResult Add(UnitCreateUpdateDto itemDto)
        {
            var res = validator.Validate(itemDto);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors.Select(v => v.ErrorMessage));
            }
            var item = mapper.Map<Unit>(itemDto);
            return Ok(mapper.Map<UnitWithTanksDto>(repository.Create(item)));
        }

        [HttpPut("{id}")]
        public IActionResult Update(UnitCreateUpdateDto itemDto, int id)
        {
            var res = validator.Validate(itemDto);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors.Select(v => v.ErrorMessage));
            }
            var oldItem = repository.Get(id);
            if (oldItem == null)
            {
                return NotFound($"Failed to update unit. Could not find unit with ID = {id}");
            }
            var item = mapper.Map<Unit>(itemDto);
            item.Id = id;
            return Ok(repository.Update(item));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = repository.Delete(id);
            if (item == null)
            {
                return NotFound($"Failed to delete unit. Could not find unit with ID = {id}");
            }
            return Ok(item);
        }
    }
}
