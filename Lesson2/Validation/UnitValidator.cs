using FluentValidation;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2.Validation
{

    public class UnitValidator : AbstractValidator<UnitCreateUpdateDto>
    {
        public UnitValidator()
        {
            RuleFor(unit => unit.Name).NotEmpty().WithMessage("the Name field cannot be empty");
        }
    }
}
