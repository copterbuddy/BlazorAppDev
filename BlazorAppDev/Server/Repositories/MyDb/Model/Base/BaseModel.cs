namespace BlazorAppDev.Server.Repositories.MyDb.Model.Base
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
