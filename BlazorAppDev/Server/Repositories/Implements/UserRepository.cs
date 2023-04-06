using BlazorAppDev.Server.Repositories.Interfaces;
using BlazorAppDev.Server.Repositories.MyDb;
using BlazorAppDev.Server.Repositories.MyDb.Model;

namespace BlazorAppDev.Server.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _myDb;

        public UserRepository(MyDbContext myDb)
        {
            _myDb = myDb;
        }
        public async Task<bool> Register(string Email, string Password)
        {
            try
            {
                UserDetail user = new()
                {
                    Email = Email,
                    Password = Password,
                };
                await _myDb.AddAsync(user);
                int result = await _myDb.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
