namespace BlazorAppDev.Server.Repositories.MyDb.Model
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
