namespace L38Asynchronous
{
    internal class Program
    {
        static void DoSomething(int seconds, string message, ConsoleColor color)
        {
            // Sử dụng lock để khóa lại không cho các luồng khác truy cập
            // khối lệnh bên trong lock, muốn truy cập thì phải đợi luồng hiện tại 
            // thực hiện xong khối lệnh trong lock và mở khóa thì mới được sử dụng

            lock(Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"--- {message}-Start ---");
                Console.ResetColor();
            }

            for (int i = 0; i < seconds; i++)
            {
                // Khi chạy đa luồng sẽ xảy ra trường hợp
                // luồng này vừa set màu xong chưa kịp in ra message
                // thì đã bị luồng khác thay đổi màu làm sai lệch kết quả
                // => sử dụng lock
                lock(Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{message, 9} - {i}");
                    Console.ResetColor();
                }

                Thread.Sleep(1000);
            }

            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"--- {message}-End ---");
                Console.ResetColor();
            }
        }

        // ============= Bất đồng bộ async/await không có giá trị trả về =============
        // # Tạo tác vụ:
        // Task task = new Task(Action action); -> () => {}
        // Task task = new Task(Action<object> action, object state) -> (object) => {}
        // object state sẽ được truyền vào làm tham số cho action
        static async Task Task2()
        {
            Task t2 = new Task(() => {
                DoSomething(4, "T2", ConsoleColor.DarkGreen);
            });

            t2.Start(); // Thread

            // Khi muốn thực hiện một việc gì đó tại thời điểm task thực hiện xong
            // nhưng nếu dùng t2.Wait() thì sẽ bị đồng bộ, phải đợi t2 thực hiện xong
            // thì mới thực hiện các task khác
            // => Sử dụng async/await sẽ giải quyết được vấn đề này

            await t2; // Vì await đã trả về giá trị nên không cần return t2
            Console.WriteLine("T2 đã hoàn thành");
        }

        static async Task Task3()
        {
            Task t3 = new Task(
                (obj) => {
                    string taskName = (string)obj;
                    DoSomething(10, taskName, ConsoleColor.Magenta);
                }, "T3"
            );

            t3.Start(); // Thread

            await t3;
            Console.WriteLine("T3 đã hoàn thành");
        } 
        static async Task Main(string[] args)
        {


            //// Khi task chính là hàm Main (task T1) kết thúc thì các task con của nó cũng
            //// kết thúc theo, ở đây t3 chưa chạy xong đã kết thúc nên không thấy "T3-End"
            //// => thêm Console.ReadKey() để giữ T1 không kết thúc

            // Task t2 = Task2();
            // Task t3 = Task3();
            // DoSomething(5, "T1", ConsoleColor.DarkYellow); // Thread

            //// Sử dụng phương thức Wait hoặc WaitAll để đợi các task hoàn thành
            //// rồi mới thực hiện các câu lệnh phía sau nó
            //// t2.Wait();
            //// t3.Wait();
            //// Task.WaitAll(t2, t3);

            //// Task.Wait sẽ khóa tất cả các luồng khác lại
            //// đợi task hiện tại thực hiện xong thì mới thực hiện các task sau nó
            //// sử dụng async/await sẽ không bị khóa các luồng khác
            // await t2;
            // await t3;
            // Console.WriteLine("Press any key");
            // Console.ReadKey();

            // ============= Bất đồng bộ async/await có giá trị trả về =============
            // Tạo tác vụ:
            // Task<string> t4 = new Task<sting>(Func<string>) -> () => { return string }
            // Task<string> t5 = new Task<string>(Func<object, string> func, object state)
            // -> (object) => { return string }

            Task<string> t4 = Task4();
            Task<string> t5 = Task5();
            DoSomething(5, "T1", ConsoleColor.DarkYellow);
            Task<string> getWeb = GetWeb("https://www.xuanphuc.space/room/room-id");

            var result4 = await t4;
            var result5 = await t5;
            Console.WriteLine(result4);
            Console.WriteLine(result5);

            var resultWeb = await getWeb;
            Console.WriteLine(resultWeb);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        static async Task<string> Task4()
        {
            Task<string> t4 = new Task<string>(() => {
                DoSomething(10, "T4", ConsoleColor.DarkRed);
                return "Return from T4";
            });

            t4.Start();

            string result = await t4;
            return result; // trả về kết quả t4.Result
        }

        static async Task<string> Task5()
        {
            Task<string> t5 = new Task<string>(
                (obj) => {
                    string taskName = (string)obj;
                    DoSomething(4, taskName, ConsoleColor.DarkCyan);
                    return $"Return from {taskName}";
                }, "T5"
            );

            t5.Start();

            string result = await t5;
            return result;
        }

        static async Task<string> GetWeb(string url)
        {
            HttpClient httpClient = new HttpClient();
            // Phương thức bất đồng bộ đọc nội dung của trang web
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }
}