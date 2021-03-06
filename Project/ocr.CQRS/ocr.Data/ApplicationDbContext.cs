using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ocr.Domain;
    
namespace ocr.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }

    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql<ApplicationDbContext>("PORT = 5432; HOST =127.0.0.1; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'ocr-event-sourcing'; PASSWORD = 'Welcome1**'; USER ID = 'postgres'");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
