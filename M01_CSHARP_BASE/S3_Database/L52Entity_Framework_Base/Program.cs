using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace L52Entity_Framework_Base
{
    internal class Program
    {
        static void CreateDatabase()
        {
            using var dbcontext = new MyDBContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;
            Console.WriteLine("Database name: " + dbname);

            // Tạo database, nếu chưa có thì tạo mới
            // và tự động thêm các bảng được cấu hình trong dbcontext
            // Có thể dùng EnsureCreatedAsync nếu dùng bất đồng bộ
            var isCreated = dbcontext.Database.EnsureCreated();
            if(isCreated == true)
            {
                Console.WriteLine($"Tao thanh cong database: {dbname}");
            }
            else
            {
                Console.WriteLine($"Tao that bai database: {dbname}");
            }
        }

        static void DropDatabase()
        {
            using var dbcontext = new MyDBContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;
            Console.WriteLine("Database name: " + dbname);

            // True neu database co ton tai
            // False neu database khong ton tai
            var isDeleted = dbcontext.Database.EnsureDeleted();
            if (isDeleted == true)
            {
                Console.WriteLine($"Xoa thanh cong database: {dbname}");
            }
            else
            {
                Console.WriteLine($"Xoa that bai database: {dbname}");
            }
        }

        static void InsertProduct()
        {
            var dbcontext = new MyDBContext();
            /*
             * 1. Model: Tao Product
             * 2. Add, AddAsync
             * 3. SaveChanges
             */

            //var product1 = new Product() { ProductName = "San pham 1", Provider = "Nha cung cap 1" };
            //dbcontext.Add(product1);

            var products = new object[]
            {
                new Product() {ProductName = "San pham 2", Provider = "Cong ty 2"},
                new Product() {ProductName = "San pham 3", Provider = "Cong ty 3"},
                new Product() {ProductName = "San pham 4", Provider = "Cong ty 4"},
            };
            // Them nhieu san pham 
            dbcontext.AddRange(products);

            int rowChanged = dbcontext.SaveChanges();
            Console.WriteLine($"So dong bi thay doi {rowChanged}");
        }

        static void ReadProducts()
        {
            var dbcontext = new MyDBContext();
            var products = dbcontext.products.ToList();

            //products.ToList().ForEach((product) => { product.Print(); });

            var query1 = from product in products
                         where product.ProductId >= 3
                         orderby product.ProductId descending
                         select product;
            query1.ToList().ForEach((product) => { product.Print(); });
        }

        static void UpdateProductName(int productId, string productName)
        {
            var dbcontext = new MyDBContext();
            var products = dbcontext.products;

            Product? product1 = (from product in products
                          where product.ProductId == productId
                          select product).FirstOrDefault();
            if(product1!= null)
            {
                // DBContext se giam sat su thay doi cua du lieu
                // neu co thay doi thi moi cap nhat
                // EntityState.Detached -> Khong theo doi su thay doi -> Khong thay doi
                //EntityEntry<Product> entry = dbcontext.Entry(product1);
                //entry.State = EntityState.Detached;

                product1.ProductName = productName;
                int rowsChanged = dbcontext.SaveChanges();
                Console.WriteLine($"So dong cap nhat: {rowsChanged}");
            }
        }

        static void DeleteProduct(int productId)
        {
            var dbcontext = new MyDBContext();
            var products = dbcontext.products;

            Product? product1 = (from product in products
                                 where product.ProductId == productId
                                 select product).FirstOrDefault();
            if (product1 != null)
            {
                dbcontext.Remove(product1);
                int rowsChanged = dbcontext.SaveChanges();
                Console.WriteLine($"So dong da bi xoa: {rowsChanged}");
            }
        }

        static void Main(string[] args)
        {
            // Entity -> là 1 bảng trong database
            // Biểu diễn 1 database kế thừa từ lớp DBContext
            //CreateDatabase();
            //DropDatabase();
            //InsertProduct();
            ReadProducts();
            //UpdateProductName(1, "San pham 999");
            DeleteProduct(7);
            

        }

    }
}