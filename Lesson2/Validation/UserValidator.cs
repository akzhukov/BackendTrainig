using FluentValidation;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2.Validation
{
    public class UserValidator: AbstractValidator<UserLogInDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Login).NotEmpty().WithMessage("the Login field cannot be empty");
            RuleFor(user => user.Password).NotEmpty().WithMessage("the Password field cannot be empty");
        }
    }
}
