using L54FluentAPI.Models;
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

        public DbSet<CategoryDetail> CategoryDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var entity = modelBuilder.Entity(typeof(Product));
            // hoặc sử dụng cách sau:
            //var entity = modelBuilder.Entity<Product>();
            // hoặc sử dụng cách sau: (*)
            modelBuilder.Entity<Product>(entity =>
            {
                // Fluent API sẽ thiết lập đè lên Attribute

                //// Table mapping

                // Tạo bảng tên Product ~ [Table("Product")]
                entity.ToTable("Product");

                // Thiết lập khóa chính ~ [Key]
                entity.HasKey(p => p.ProductId);

                // Index: Tạo chỉ mục và đặt tên cho chỉ mục
                entity.HasIndex(p => p.Price).HasDatabaseName("Index_Product_Price");


                // Relative (mối quan hệ)
                // Mối quan hệ 1-N (1 Category có nhiều sản phẩm)

                // HasOne: chỉ ra property là phần 1 lưu trong Product
                // Tạo ra đối tượng ReferenceNavigationBuilder (1-N)
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products) // Chỉ ra Collection tập Product lưu ở phía một (Category)
                      .HasForeignKey("CategoryId") // Chỉ ra tên của FK
                      // DeleteBehavior.Cascade: khi xóa phần 1 -> phần nhiều cũng bị xóa theo
                      // DeleteBehavior.Cascade: khi xóa phần 1 -> phần nhiều thành null (chỉ khi phần nhiều có khả năng nullable?)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("FK_Product_Category") // Đặt tên constraint (nếu không có thì tự động đặt)
                      ;

                //// Property mapping
                entity.Property(p => p.Name)
                      .HasColumnName("ProductName") // Đặt tên cột
                      .HasColumnType("nvarchar") // Kiểu dữ liệu Nvarchar
                      .HasMaxLength(60) // Tối đa 60 ký tự
                      .IsRequired(true) // NOT NULL
                      .HasDefaultValue("Ten san pham mac dinh")
                      ;

            });

            // Thiết lập mối quan hệ 1-1
            modelBuilder.Entity<CategoryDetail>(entity =>
            {
                entity.HasOne(cd => cd.Category)
                      .WithOne(c => c.CategoryDetail)
                      // Thiết lập cột CategoryDetailId vừa là khóa chính
                      // vừa là khóa ngoại liên kết đến bảng Category
                      // (CategoryDetailId = CategoryId)
                      .HasForeignKey<CategoryDetail>(cd => cd.CategoryDetailId)
                      .OnDelete(DeleteBehavior.Cascade)
                      ;
            });
        }
    }
}
