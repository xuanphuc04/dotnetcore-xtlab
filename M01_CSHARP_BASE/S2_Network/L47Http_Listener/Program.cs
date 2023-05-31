using System.Net;
using System.Text;

namespace L47Http_Listener
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if(HttpListener.IsSupported)
            {
                Console.WriteLine("Support HttpListener");
            } else
            {
                Console.WriteLine("Not support HttpListener");
                throw new Exception("Not support HttpListener");
            }

            var server = new HttpListener();

            server.Prefixes.Add("http://*:8080/");

            server.Start();

            Console.WriteLine("Server HTTP start");
            do
            {
                // Nếu chỉ có 1 phương thức GetContexAsync mà không gọi tiếp
                // thì server này sẽ kết thúc -> để server luôn lắng nghe thì
                // cần sử dụng vòng lặp
                var context = await server.GetContextAsync();
                Console.WriteLine("Client connected!");

                var response = context.Response; // Trả response về cho client
                var outputStream = response.OutputStream;

                response.Headers.Add("content-type", "text/html"); // Bổ sung thêm header

                var html = "<h1>Hello World!</h1>";
                var bytes = Encoding.UTF8.GetBytes(html);
                await outputStream.WriteAsync(bytes);
                outputStream.Close();

            } while (server.IsListening);
        }
    }
}