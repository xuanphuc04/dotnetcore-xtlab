using System.Diagnostics;
using System.Xml.Linq;

namespace L31List
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //ListTest.Run();
            //SortedListTest.Run();
        }

        public class ListTest
        {
            public static void Run()
            {
                /*
             * Thêm phần tử:
             * Add( object ) - Thêm 1 phần tử
             * AddRange( arrayObject ) - Nối một mảng vào list
             * 
             * Chèn phần tử:
             * Insert( index, object ) - Chèn 1 phần tử vào vị trí index
             * InsertRange( index, arrayObject ) - Chèn một mảng phần tử vào vị trí index
             * 
             * Xóa phần tử:
             * RemoveAt( index ) - Xóa 1 phần tử tại vị trí index
             * Remove( object ) - Xóa 1 phần tử khi có tham chiếu đến phần tử đó
             *                  (với kiểu tham trị thì chỉ cần tryền giá trị)
             * RemoveRange( index, count ) - Xóa từ vị trí index với count phần tử
             * RemoveAll() - Xóa toàn bộ phần tử (làm rỗng)
             * Clear() - Xóa toàn bộ phần tử (làm rỗng)
             * 
             * Tìm kiếm phần tử
             * Sắp xếp phần tử
             */


                List<Product> products = new List<Product>()
            {
                new Product() { Id = "PD01", Name = "iPhone", Price = 1200, Origin = "America"},
                new Product() { Id = "PD02", Name = "Xiaomi", Price = 900, Origin = "China"},
                new Product() { Id = "PD03", Name = "Oppo", Price = 800, Origin = "China"},
                new Product() { Id = "PD04", Name = "Samsung", Price = 1000, Origin = "Korea"},
                new Product() { Id = "PD05", Name = "Sony", Price = 1100, Origin = "Japan"},

            };


                // Tìm kiếm
                var result = products.Find((product) => {
                    return product.Origin == "China";
                });

                if (result != null)
                {
                    Console.WriteLine($"{result.Id} {result.Name} {result.Price} {result.Origin}");
                }
                Console.WriteLine("----\n");


                // Sắp xếp
                foreach (Product product in products)
                {
                    Console.WriteLine($"{product.Id} {product.Name} {product.Price} {product.Origin}");
                }
                Console.WriteLine("----\n");

                products.Sort((p1, p2) => {
                    if (p1.Price < p2.Price) return 1;
                    else if (p1 == p2) return 0;
                    return -1;
                });

                foreach (Product product in products)
                {
                    Console.WriteLine($"{product.Id} {product.Name} {product.Price} {product.Origin}");
                }
            }
        }

        public class Product
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Origin { get; set; }

        }

        public class SortedListTest
        {
            public static void Run()
            {
                var products = new SortedList<string, string>();
                products.Add("Apple", "iPhone 6");
                products["Samsung"] = "Galaxy Fold";
                products["Oppo"] = "Oppo neo";

                foreach(var product in products)
                {
                    Console.WriteLine($"{product.Key} | {product.Value}");
                }

                // Duyệt qua keys
                Console.WriteLine("----- keys -----");
                foreach(var key in products.Keys)
                {
                    Console.WriteLine(key);
                }

                // Duyệt qua values
                Console.WriteLine("----- values -----");
                foreach (var value in products.Values)
                {
                    Console.WriteLine(value);
                }
            }
        }
    }
}