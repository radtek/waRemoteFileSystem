using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Toolbelt.ComponentModel.DataAnnotations;

namespace waRemoteFileSystem.DataBase
{
    public partial class MyDbContext : DbContext
    {

        private readonly IConfiguration conf;
        private readonly ILoggerFactory logger;

        public MyDbContext(IConfiguration _conf)
        {
            conf = _conf;
        }

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
            var connectionStr = conf.GetConnectionString("DefaultConnection");

            builder.UseSqlite(connectionStr);
            builder.EnableDetailedErrors();
            builder.UseLoggerFactory(logger);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.BuildIndexesFromAnnotations();
        }
    }

}
