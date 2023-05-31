using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L52Entity_Framework_Base
{
    internal class MyDBContext : DbContext
    {
        private const string connectionString = @"
                    Data Source=XUANPHUC\XUANPHUC;
                    Initial Catalog=Database01;
                    UID=xuanphuc;
                    PWD=123456;
                    TrustServerCertificate=True;
                    ";

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create((builder) =>
        {
            //builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddFilter(DbLoggerCategory.Database.Name, LogLevel.Information);
            builder.AddConsole(); // Hien thi ra Consoles
        });

        // Khai bao bang Product
        public DbSet<Product> products { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory); // Su dung logger
            optionsBuilder.UseSqlServer(connectionString); // Ket noi database
        }
    }
}
