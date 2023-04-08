using BlazorAppDev.Server.Repositories.Interfaces;
using BlazorAppDev.Server.Repositories.MyDb;
using BlazorAppDev.Server.Repositories.MyDb.Model;
using BlazorAppDev.Server.Services.Interfaces;
using static BlazorAppDev.Client.Pages.Login;

namespace BlazorAppDev.Server.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public MyDbContext myDbContext { get; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            try
            {
                if (request.Email is null ||
                        request.Password is null ||
                        request.ConfirmPassword is null ||
                        request.Password != request.ConfirmPassword)
                {
                    return false;
                }

                bool result = await _userRepository.Register(Email: request.Email, Password: request.Password);

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<UserDetail> Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password)) return null;

            try
            {
                var result = await _userRepository.Login(request.Email, request.Password);

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
