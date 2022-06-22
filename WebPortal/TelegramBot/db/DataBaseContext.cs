using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TelegramBot
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var builderObject = builder.Build();
            string connectionString = builderObject.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Link> Links { get; set; }

    }
}