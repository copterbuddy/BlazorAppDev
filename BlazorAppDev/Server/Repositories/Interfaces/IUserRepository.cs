using BlazorAppDev.Server.Repositories.MyDb.Model;

namespace BlazorAppDev.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Register(string Email, string Password);
        Task<UserDetail> Login(string Email, string Password);
    }
}
