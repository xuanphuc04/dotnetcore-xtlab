namespace L39Type_Attribute
{
    internal class Program
    {
        // - Type: Chứa thông tin về 1 kiểu dữ liệu nào đó: class, struct,... int, bool,...
        // là thành phần cơ bản trong .NET được dùng cho kỹ thuật Reflection
        // - Attribute: 1 phần của siêu dữ liệu (metadata), cung cấp thông tin bổ sung
        // cho 1 lớp hoặc thành viên của lớp
        // - Reflection: Thông tin kiểu dữ liệu, thời điểm thực thi

        static void Main(string[] args)
        {
            //========= Type =========
            int[] a = {1, 2, 3};

            //Type t1 = typeof(int);
            //var t2 = typeof(String);
            //var t3 = typeof(Array);
            //var t4 = a.GetType();

            //Type t = a.GetType(); // typeof(Array)
            //Console.WriteLine(t.FullName);

            //Console.WriteLine("---- Các thuộc tính ----");
            //t.GetProperties().ToList().ForEach(p => Console.WriteLine(p));
            //Console.WriteLine("---- Các thuộc tính ----");
            //t.GetMethods().ToList().ForEach(p => Console.WriteLine(p));

            //----
            User user = new User()
            {
                Name = "Xuân Phúc",
                Age = 20,
                PhoneNumber = "1234567890",
                Email = "abc@abc.com"
            };

            var properties = user.GetType().GetProperties();
            foreach ( var prop in properties )
            {
                var name = prop.Name;
                var value = prop.GetValue(user);
                Console.WriteLine($"{name}: {value}");
            }
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}