namespace BlazorAppDev.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(RegisterRequest request);
    }
}
