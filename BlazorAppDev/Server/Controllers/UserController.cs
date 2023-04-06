﻿using BlazorAppDev.Server.Services.Implements;
using BlazorAppDev.Server.Services.Interfaces;
using BlazorAppDev.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorAppDev.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IConfiguration configuration,IUserService userServce)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = userServce;
        }

        [HttpGet("Greeting")]
        public IActionResult Get()
        {
            _logger.LogInformation("Greeting Log");

            return Ok($"Service Running On {_configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT")}");
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterRequest request)
        {
            try
            {
                if (request.Email is null ||
                    request.Password is null ||
                    request.ConfirmPassword is null ||
                    request.Password != request.ConfirmPassword)
                {
                    return Unauthorized();
                }

                var result = _userService.Register(request);

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginRequest loginModel)
        {
            try
            {

                if (loginModel.Email == "test@test.com" && loginModel.Password == "12345678")
                {
                    var issuer = _configuration.GetValue<string>("Jwt:Issuer");
                    var audience = _configuration.GetValue<string>("Jwt:Audience");
                    var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, loginModel.Email),
                        new Claim(JwtRegisteredClaimNames.Email, loginModel.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                        Expires = DateTime.UtcNow.AddSeconds(30),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    var result = new LoginResponse
                    {
                        Token = jwtToken,
                    };

                    return Ok(result);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("GreetingAuthen")]
        [Authorize]
        public IActionResult GreetingAuthen()
        {
            _logger.LogInformation("Greeting Log");

            return Ok($"Service Running On {_configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT")}");
        }
    }
}
