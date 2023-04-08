using FluentValidation;
using Blazored.FluentValidation;
using BlazorAppDev.Shared.Models;
using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorAppDev.Client.Pages
{
    public partial class Login
    {
        [Inject] private HttpClient? _http { get; set; }
        private FluentValidationValidator? _validatorModel { get; set; }

        //public Login(FluentValidationValidator? ValidatorModel, HttpClient? Http)
        //{
        //    _validatorModel = ValidatorModel;
        //    _http = Http;
        //}

        public LoginModel? Model = new LoginModel();
        public LoginRequest? req;
        public LoginResponse? res;

        private async Task HandleSubmit()
        {
            try
            {
                if (await _validatorModel.ValidateAsync() == false) return;

                req = new LoginRequest
                {
                    Email = Model.Email,
                    Password = Model.Password,
                };

                var result = await _http.PostAsJsonAsync("User/Login", req);
                if (result.IsSuccessStatusCode)
                {
                    res = await result.Content.ReadFromJsonAsync<LoginResponse>();
                    if (res is not null)
                    {
                        Console.WriteLine("token is : " + res.Token);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error is : " + e);
                throw;
            }
        }


        //Model
        public class LoginModel
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        //Validator Model
        public class LoginModelValidatorModel : AbstractValidator<LoginModel>
        {
            public LoginModelValidatorModel()
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Please enter Email Address")
                    .EmailAddress()
                    .WithMessage("Invalid email address");
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Please enter Password")
                    .MinimumLength(4)
                    .WithMessage("Password must be at least 4 characters");
            }
        }
    }
}