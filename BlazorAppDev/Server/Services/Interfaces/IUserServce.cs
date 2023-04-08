using BlazorAppDev.Server.Repositories.MyDb.Model;

namespace BlazorAppDev.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(RegisterRequest request);
        Task<UserDetail> Login(LoginRequest request);
    }
}
