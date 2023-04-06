using BlazorAppDev.Server.Repositories.Interfaces;
using BlazorAppDev.Server.Services.Interfaces;

namespace BlazorAppDev.Server.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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

                bool result = await userRepository.Register(Email: request.Email, Password: request.Password);

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
