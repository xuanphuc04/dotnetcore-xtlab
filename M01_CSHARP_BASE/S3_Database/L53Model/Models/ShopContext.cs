using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Logging;

namespace L53Model
{
    public class ShopContext : DbContext
    {
        private const string connectionString = @"
                    Data Source=XUANPHUC\XUANPHUC;
                    Initial Catalog=ShopDatabase;
                    UID=xuanphuc;
                    PWD=123456;
                    TrustServerCertificate=True;
                    ";
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create((builder) =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();

        });

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
