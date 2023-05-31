using Microsoft.EntityFrameworkCore;

namespace L53Model
{

    internal class Program
    { 
        public static void CreateDatabase()
        {
            using var dbContext = new ShopContext();
            string dbName = dbContext.Database.GetDbConnection().Database;

            var isCreated = dbContext.Database.EnsureCreated();
            if(isCreated == true)
            {
                Console.WriteLine($"Tao thanh cong database {dbName}");
            }
            else
            {
                Console.WriteLine($"Tao that bai database {dbName}");
            }
        }

        public static void DropDatabase()
        {
            using var dbContext = new ShopContext();
            string dbName = dbContext.Database.GetDbConnection().Database;

            var isCreated = dbContext.Database.EnsureDeleted();
            if (isCreated == true)
            {
                Console.WriteLine($"Xoa thanh cong database {dbName}");
            }
            else
            {
                Console.WriteLine($"Xoa that bai database {dbName}");
            }
        }

        public static void InsertMockDatabase()
        {
            using var dbContext = new ShopContext();

            Category c1 = new Category() { Name = "Dien thoai", Description = "Ban dien thoai" };
            Category c2 = new Category() { Name = "Do uong", Description = "Ban do uong" };
            dbContext.Categories.Add(c1);
            dbContext.Categories.Add(c2);

            //Category? c1 = (from category in dbContext.Categories
            //               where category.CategoryId == 1
            //               select category).FirstOrDefault();
            //Category? c2 = (from category in dbContext.Categories
            //                where category.CategoryId == 2
            //                select category).FirstOrDefault();

            // Su dung thuoc tinh CategoryId
            dbContext.Products.Add(new Product() { Name="Iphone 8", Price=1000, CategoryId=1 });
            // Khong can dbContext.Products va su dung Category thay vi CategoryId
            dbContext.Add(new Product() { Name="Samsung", Price=900, Category = c1 });
            dbContext.Add(new Product() { Name="Ruou vang ABC", Price=500, Category = c2 });
            dbContext.Add(new Product() { Name="Nokia", Price=600, Category = c1 });
            dbContext.Add(new Product() { Name="Cafe abc", Price=100, Category = c2 });
            dbContext.Add(new Product() { Name="Nuoc ngot", Price=50, Category = c2 });
            dbContext.Add(new Product() { Name="Bia", Price=200, Category = c2 });

            dbContext.SaveChanges();
        }

        public static void GetCategoryReferenceNavigation()
        {
            using var dbContext = new ShopContext();

            Product? pd = (from product in dbContext.Products
                          where product.ProductId == 6
                          select product).FirstOrDefault();

            // Khi truy cap den 1 thuoc tinh tham chieu den doi tuong khac
            // thi se co gia tri la NULL mac dinh
            // de truy cap den tham chieu nay phai su dung dbContext.Entry();
            // de load gia tri tham chieu
            var entry = dbContext.Entry(pd);
            entry.Reference(product => product.Category).Load(); // Truy cap Reference navigation

            if(pd.Category != null)
            {
                Console.WriteLine($"{pd.Category.Name} - {pd.Category.Description}");
            }
            else
            {
                Console.WriteLine("Category = NULL");
            }
        }

        public static void GetProductsReferenceCollection()
        {
            using var dbContext = new ShopContext();

            var categoryR = (from category in dbContext.Categories
                             where category.CategoryId == 2
                             select category).FirstOrDefault();
            // Load Collection navigation: Products trong Category
            dbContext.Entry(categoryR).Collection(category => category.Products).Load();

            if(categoryR.Products != null)
            {
                Console.WriteLine($"So san pham trong {categoryR.Name}: {categoryR.Products.Count}");
                categoryR.Products.ForEach(product => product.Print());
            }
            else
            {
                Console.WriteLine("CategoryR.Product == NULL");
            }
        }

        public static void QueryWithLinq()
        {
            using var dbContext = new ShopContext();

            var query = from p in dbContext.Products
                        join c in dbContext.Categories on p.CategoryId equals c.CategoryId
                        select new
                        {
                            Name = p.Name,
                            Price = p.Price,
                            Category = c.Name
                        };
            // Chuyen sang ToList de truy van den database
            query.ToList().ForEach(p => Console.WriteLine($"{p.Name} - {p.Category} - {p.Price}"));

        }

        static void Main(string[] args)
        {
            //DropDatabase();
            //CreateDatabase();
            //InsertMockDatabase();
            //GetCategoryReferenceNavigation();
            //GetProductsReferenceCollection();
            QueryWithLinq();
        }
    }
}