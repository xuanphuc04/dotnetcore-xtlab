namespace L37LINQ
{
    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }         // tên
        public double Price { set; get; }        // giá
        public string[] Colors { set; get; }     // các màu sắc
        public int Brand { set; get; }           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
           => $"{ID,3} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";

    }

    public class Brand
    {
        public string Name { set; get; }
        public int ID { set; get; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>() {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty CCC"},
            };

            var products = new List<Product>()
            {
                new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };

            //# Câu lệnh truy vấn LINQ
            var query = from p in products
                        where p.Price == 400
                        select p;

            //foreach(var product in query)
            //{
            //    Console.WriteLine(product);
            //}


            /*
             * Các phương thức trong LINQ:
             * Select - Trả về 1 danh sách sau khi biến đổi (giống map JS)
             * SelectMany - Giống Select nhưng nếu phần tử là mảng sẽ được phân rã
             * 
             * Where - Trả về danh sách các phần tử thỏa mãn đk (giống filter JS)
             * 
             * Min, Max, Sum, Average
             * 
             * Join - gộp hai đối tượng (giống join SQL)
             * GroupJoin - giống Join nhưng được chia theo nhóm
             * 
             * Take - Lấy các phần tử đầu tiên
             * Skip - Bỏ qua các phần tử đầu tiên
             * 
             * OrderBy (tăng dần)
             * OrderByDescending (giảm dần)
             * 
             * Reverse - Đảo ngược
             * 
             * GroupBy - Gộp nhóm
             * 
             * Distinct - Lấy danh sách không trùng nhau
             * 
             * Single - Trả về phần tử thỏa mãn đk logic (không có/có nhiều ptu thõa mãn thì lỗi)
             * SingleOrDefault - Giống Single, chỉ khác là không có ptu thỏa mãn -> null
             * 
             * Any - Kiểm tra xem có ptu nào thỏa mãn không (có 1 -> true)
             * All - Tất cả ptu thỏa mãn -> true
             * 
             * Count - Đếm tất cả ptu (nếu truyền predicate -> đếm ptu thỏa mãn logic)
             */


            //# Phương thức Select()
            // - Biến đổi danh sách phần tử thành danh sách mới (giống map JS)
            var result = products.Select((product) =>
            {
                //return product.Name;
                return new
                {
                    Name = product.Name,
                    Price = product.Price,
                };
            });

            //foreach(var product in result)
            //{
            //    Console.WriteLine(product);
            //}


            //# Phương thức SelectMany()
            //- Trả về danh sách được phân tách (mảng) của tất cả các phần tử
            var result1 = products.SelectMany((product) =>
            {
                return product.Colors; // Colors là kiểu mảng string[]
            });

            //foreach (var colors in result1)
            //{
            //    Console.WriteLine(colors);
            //}

            //# Phương thức Where() - Tìm các phần tử thỏa mãn điều kiện (giống filter JS)
            var result2 = products.Where((product) =>
            {
                return product.Name.Contains("tr");
            });

            //foreach (var product in result2)
            //{
            //    Console.WriteLine(product);
            //}



            ////# Phương thức: Min, Max, Sum, Average
            //int[] numbers = { 1, 2, 4, 6, 4, 2, 8, 9 };
            //// VD1: Tìm số chẵn lớn nhất:
            //Console.WriteLine(numbers.Where(num => num %2 == 0).Max());

            //// VD2: Tìm giá nhỏ nhất của các sản phẩm
            //Console.WriteLine(products.Min(p => p.Price));

            //# Phương thức Join
            //- Lấy ra các phần tử có product.Brand = brand.ID
            var result3 = products.Join(
                brands,
                p => p.Brand,
                b => b.ID,
                (product, brand) =>
                {
                    return new
                    {
                        Name = product.Name,
                        Brand = brand.Name
                    };
                }
            );

            //foreach(var item in result3)
            //{
            //    Console.WriteLine(item);
            //}



            //# Phương thức GroupJoin
            //- Giống Join nhưng được gộp theo nhóm
            var result4 = brands.GroupJoin(
                products,
                b => b.ID,
                p => p.Brand,
                (brand, products) =>
                {
                    return new
                    {
                        Brand = brand.Name,
                        Products = products
                    };
                }
            );

            //foreach(var item in result4)
            //{
            //    Console.WriteLine(item.Brand);
            //    foreach(var product in item.Products)
            //    {
            //        Console.WriteLine(product);
            //    }
            //}


            //# Phương thức Take và Skip
            //VD: Lấy ra 3 phần tử từ products
            //- Kết quả có thể dùng foreach hoặc:
            //products.Take(3).ToList().ForEach(product => Console.WriteLine(product));


            //# Phương thức OderBy / OderByDescending
            //products.OrderBy(product => product.Price).ToList()
            //    .ForEach(item => Console.WriteLine(item));



            //# Phương thức GroupBy
            //- Gộp nhóm các phần tử có tính chất giống nhau
            var result5 = products.GroupBy((product) => product.Price);
            // foreach (var group in result5)
            // {
            //     Console.WriteLine(group.Key);
            //     foreach (var item in group)
            //     {
            //         Console.WriteLine(item);
            //     }
            // }


            //# Phương thức Distinct
            //products.SelectMany(product => product.Colors).Distinct()
            //    .ToList().ForEach(product => Console.WriteLine(product));


            //# Phương thức Single / SingleOrDefault
            //var product = products.Single(product => product.Price == 600);
            //Console.WriteLine(product);



            //### Bài tập:
            // 1.In ra tên sản phẩm, tên thương hiệu của sản phẩm
            // có giá từ 300 đến 400 và sắp xếp theo giá giảm dần
            products.Where(product => product.Price >= 300 && product.Price <= 400)
                    .OrderByDescending(product => product.Price)
                    .Join(
                        brands,
                        product => product.Brand,
                        brand => brand.ID,
                        (product, brand) =>
                        {
                            return new
                            {
                                Brand = brand.Name,
                                Name = product.Name,
                                Price = product.Price,
                            };
                        }
                     )
                    .ToList();
            //.ForEach(item => Console.WriteLine(item));

            //==============================================================
            /*
             * Cú pháp LINQ
             * 1. Xác định nguồn: from tenphantu in IEnumerables
             * -> với mỗi phần tử tenphantu trong IEnumerables
             * 2. join, where, oderby,...
             * 3. Lấy dữ liệu: select, groupby,...
             */

            //**VD: Lấy ra tên của tất cả sản phẩm:
            var query1 = from product in products
                         select product.Name;

            //query1.ToList().ForEach(item => Console.WriteLine(item));

            // select có thể trả về sau khi được biến đổi, tính toán
            var query2 = from product in products
                         select new
                         {
                             Name = product.Name,
                             Price = product.Price,
                         };
            //query2.ToList().ForEach(item => Console.WriteLine(item));

            //**VD2: Lấy tên, giá của sản phẩm có giá = 400
            var query3 = from product in products
                         where product.Price == 400
                         select new
                         {
                             Name = product.Name,
                             Price = product.Price,
                         };
            //query3.ToList().ForEach(product => Console.WriteLine(product));

            //**VD3: Lấy tên, giá, màu sắc của sản phẩm có giá <= 500, màu xanh
            // sắp xếp theo giá giảm dần
            var query4 = from product in products
                         from color in product.Colors
                         where product.Price <= 500 & color == "Xanh"
                         orderby product.Price descending
                         select new
                         {
                             Name = product.Name,
                             Price = product.Price,
                             Colors = product.Colors
                         };
            //query4.ToList().ForEach(product => {
            //    Console.WriteLine($"{product.Name} - {product.Price} - {String.Join(", ", product.Colors)}");
            //});

            //**VD4: Nhóm sản phẩm theo giá
            var query5 = from product in products
                         group product by product.Price;

            //query5.ToList().ForEach(product => {
            //    Console.WriteLine(product.Key);
            //    product.ToList().ForEach(item => Console.WriteLine(item));
            //});

            //**VD5: Nhóm sản phẩm theo giá và sắp xếp theo giá tăng dần
            var query6 = from product in products
                         group product by product.Price into gr // gán vào biến ảo
                         orderby gr.Key
                         select gr;

            //query6.ToList().ForEach(product => {
            //    Console.WriteLine(product.Key);
            //    product.ToList().ForEach(item => Console.WriteLine(item));
            //});

            //**VD6: Sử dụng từ khóa let để lưu giá trị
            var query7 = from product in products
                         group product by product.Price into gr // gán vào biến ảo
                         orderby gr.Key
                         let quantity = "So luong: " + gr.Count()
                         select new
                         {
                             Price = gr.Key,
                             Products = gr.ToList(),
                             Quantity = quantity
                         };

            //query7.ToList().ForEach(productsGroup => {
            //    Console.WriteLine(productsGroup.Price);
            //    Console.WriteLine(productsGroup.Quantity);
            //    productsGroup.Products.ForEach(item => Console.WriteLine(item));
            //});

            //**VD7: Sử dụng join
            //- Đưa ra tên, giá, tên thương hiệu của sản phẩm
            var query8 = from product in products
                         join brand in brands on product.Brand equals brand.ID
                         select new
                         {
                             Name = product.Name,
                             Brand = brand.Name,
                             Price = product.Price
                         };
            query8.ToList().ForEach(item => Console.WriteLine($"{item.Name,15} {item.Brand,15} {item.Price,5}"));
        }
    }
}