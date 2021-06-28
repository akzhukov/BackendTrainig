using FluentValidation;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2.Validation
{
    
    public class TankValidator : AbstractValidator<TankCreateUpdateDto>
    {
        public TankValidator()
        {
            RuleFor(tank => tank.Name).NotEmpty().WithMessage("the Name field cannot be empty");
            RuleFor(tank => tank.Volume).LessThanOrEqualTo(tank => tank.MaxVolume)
               .WithMessage("the current volume of the tank cannot exceed the maximum volume");
        }
    }
   }
