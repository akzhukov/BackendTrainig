using FluentValidation;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2.Validation
{
    public class FactoryValidator : AbstractValidator<FactoryCreateUpdateDto>
    {
        public FactoryValidator()
        {
            RuleFor(factory => factory.Name).NotEmpty().WithMessage("the Name field cannot be empty");
            RuleFor(factory => factory.Description).NotEmpty().WithMessage("the Description field cannot be empty");
        }
    }
 
}
