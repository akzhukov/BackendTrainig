using AutoMapper;
using Lesson2.Autorization;
using Lesson2.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Policy = "Administrator")]
    [ApiController]
    [Route("api/[controller]")]
    public class FactoriesController : ControllerBase
    {
        protected readonly IFactoryRepository repository;
        private readonly IMapper mapper;
        private readonly FactoryValidator validator;

        public FactoriesController(IFactoryRepository repository, IMapper mapper, FactoryValidator validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<IList<Tank>> GetAll()
        {
            return Ok(mapper.Map<IList<FactoryDto>>(repository.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = repository.GetFactoryWithUnits(id);
            if (item == null)
            {
                return NotFound($"Factory with ID {id} not found.");
            }
            return Ok(mapper.Map<FactoryWithUnitsDto>(item));
        }

        [HttpPost]
        public IActionResult Add(FactoryCreateUpdateDto itemDto)
        {
            var res = validator.Validate(itemDto);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors.Select(v => v.ErrorMessage));
            }
            var item = mapper.Map<Factory>(itemDto);
            return Ok(mapper.Map<FactoryDto>(repository.Create(item)));
        }

        [HttpPut("{id}")]
        public IActionResult Update(FactoryCreateUpdateDto itemDto, int id)
        {
            var res = validator.Validate(itemDto);
            if (!res.IsValid)
            {
                return BadRequest(res.Errors.Select(v => v.ErrorMessage));
            }
            var oldItem = repository.Get(id);
            if (oldItem == null)
            {
                return NotFound($"Failed to update factory. Could not find factory with ID = {id}");
            }
            var item = mapper.Map<Factory>(itemDto);
            item.Id = id;
            return Ok(repository.Update(item));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = repository.Delete(id);
            if (item == null)
            {
                return NotFound($"Failed to delete factory. Could not find factory with ID = {id}");
            }
            return Ok(item);
        }
    }
}
