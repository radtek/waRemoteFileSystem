using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace waRemoteFileSystem.DataBase
{
    public partial class MyDbContext : DbContext
    {

        private readonly IConfiguration conf;
        private readonly ILoggerFactory logger;

        public MyDbContext(DbContextOptions<MyDbContext> options, ILoggerFactory _logger, IConfiguration _conf) : base(options)
        {
            logger = _logger;
            conf = _conf;
        }

        #region Declare

        public DbSet<spRole> spRoles { get; set; }
        public DbSet<tbUser> tbUsers { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=./data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<spRole>().HasData(
            //  new spRole() { Id = 1, Name = "Admin", UserAccess = "999" });

            //modelBuilder.Entity<tbUser>().HasData(
            //    new tbUser() { Id = 1, FirstName = "admin", LastName = "admin", Username = "admin", Password = HashSha256.Get("1"), EMail = "tdavron@gmail.com", RoleId = 1 });

            
        }
    }
}
