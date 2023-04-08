using FluentValidation;
using Blazored.FluentValidation;
using BlazorAppDev.Shared.Models;
using System;
using System.Net.Http.Json;

namespace BlazorAppDev.Client.Pages
{
    public partial class Register
    {
        private HttpClient _http;

        private FluentValidationValidator? _validatorModel;
        private RegisterModel Model = new RegisterModel();
        private RegisterRequest req = new RegisterRequest();
        private RegisterResponse res;
        //public Register(HttpClient Http)
        //{
        //    _http = Http;
        //}

        public async Task HandleSubmit()
        {
            if (await _validatorModel.ValidateAsync() == false) return;

            //await Http.PostAsJsonAsync();

        }

        //Model
        public class RegisterModel
        {
            public string Email { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }
        }

        //Validator Model
        public class RegisterModelValidatorModel : AbstractValidator<RegisterModel>
        {
            public RegisterModelValidatorModel()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Please input email").EmailAddress().WithMessage("Please input correct email");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Please input password").EmailAddress().WithMessage("Please input correct password");
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Please input password").Equal(x => x.Password).WithMessage("Please input match password");
            }
        }
    }
}