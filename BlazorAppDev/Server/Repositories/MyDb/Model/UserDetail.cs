using BlazorAppDev.Server.Repositories.MyDb.Model.Base;

namespace BlazorAppDev.Server.Repositories.MyDb.Model
{
    public class UserDetail : BaseModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
