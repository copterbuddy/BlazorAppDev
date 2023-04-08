using FluentValidation;
using Blazored.FluentValidation;
using BlazorAppDev.Shared.Models;
using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorAppDev.Client.Pages
{
    public partial class Register
    {
        [Inject] private HttpClient _http { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private FluentValidationValidator? _validatorModel;
        private RegisterModel Model = new RegisterModel();
        private RegisterRequest req;
        private RegisterResponse res;

        public async Task HandleSubmit()
        {
            if (await _validatorModel.ValidateAsync() == false) return;

            req = new RegisterRequest
            {
                Email = Model.Email,
                Password = Model.Password,
                ConfirmPassword = Model.ConfirmPassword,
            };
            var result = await _http.PostAsJsonAsync("User/Register", req);
            if (result.IsSuccessStatusCode)
            {
                res = await result.Content.ReadFromJsonAsync<RegisterResponse>();
                if (res?.Result == true)
                {
                    NavigationManager.NavigateTo("/login");
                }
            }

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
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Please input email")
                    .EmailAddress()
                    .WithMessage("Please input correct email");
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Please input password")
                    .MinimumLength(4)
                    .WithMessage("Please input correct password");
                RuleFor(x => x.ConfirmPassword)
                    .NotEmpty()
                    .WithMessage("Please input password")
                    .Equal(x => x.Password)
                    .WithMessage("Please input match password");
            }
        }
    }
}