using L56Migration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace L56Migration
{
    public class WebContext : DbContext
    {
        private const string connectionString = @"
                    Data Source=XUANPHUC\XUANPHUC;
                    Initial Catalog=WebDatabase;
                    UID=xuanphuc;
                    PWD=123456;
                    TrustServerCertificate=True;
                    ";

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create((builder) =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();

        });

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
        }

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ArticleTag>((entity) =>
            {
                // Danh chi muc cho TagId va ArticleId
                // va chi dinh 2 truong nay la duy nhat (2 khoa chinh)
                entity.HasIndex((articleTag) => new { articleTag.TagId, articleTag.ArticleId })
                      .IsUnique();
            });
        }
    }
}