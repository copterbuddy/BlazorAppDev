namespace BlazorAppDev.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> Register(string Email, string Password);
    }
}
