using BlazorAppDev.Server.Repositories.MyDb.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace BlazorAppDev.Server.Repositories.MyDb
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetValue<string>("ConnectionStrings_DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetail>()
                .HasKey(p => new { p.Id, p.Email });
            modelBuilder.Entity<UserDetail>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserDetail>()
                .Property(p => p.CreateDate)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserDetail>()
                .Property(a => a.LastUpdated)
                .ValueGeneratedOnAddOrUpdate();

            //modelBuilder.Entity<UserDetail>().HasData(
            //    new UserDetail { Email = "test1@test.com", Password = "1234" },
            //    new UserDetail { Email = "test2@test.com", Password = "1234" }
            //    );
        }

        public DbSet<UserDetail> UserDetail { get; set; }
    }
}
