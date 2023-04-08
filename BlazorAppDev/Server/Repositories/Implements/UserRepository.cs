using BlazorAppDev.Server.Repositories.Interfaces;
using BlazorAppDev.Server.Repositories.MyDb;
using BlazorAppDev.Server.Repositories.MyDb.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDev.Server.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _myDb;

        public UserRepository(MyDbContext myDb)
        {
            _myDb = myDb;
        }

        public async Task<UserDetail> Login(string Email, string Password)
        {
            UserDetail result = await _myDb.UserDetail.FirstOrDefaultAsync(user => 
                                                                    user.Email.Equals(Email) == true && 
                                                                    user.Password.Equals(Password) == true
                                                                    );

            return result;
        }

        public async Task<bool> Register(string Email, string Password)
        {
            try
            {
                UserDetail user = new()
                {
                    Email = Email,
                    Name = Email,
                    Password = Password,
                };
                await _myDb.UserDetail.AddAsync(user);
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
