using BlazorAppDev.Shared.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppDev.Shared.ValidatorModels
{
    public class RegisterRequestValidatorModel : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidatorModel() {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Please input email")
                .EmailAddress()
                .WithMessage("Please input correct email");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please input password")
                .EmailAddress()
                .WithMessage("Please input correct password");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Please input match password");
        }
    }
}
