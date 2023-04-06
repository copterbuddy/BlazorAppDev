using BlazorAppDev.Shared.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppDev.Shared.ValidatorModels
{
    public class LoginRequestValidatorModel : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidatorModel() {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Please enter Email Address")
                .EmailAddress()
                .WithMessage("Invalid email address");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter Password")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");
        }
    }
}
