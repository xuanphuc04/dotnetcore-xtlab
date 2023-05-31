using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace L47Http_Listener_Class
{
    public class MyHttpServer
    {
        private HttpListener listener;

        public MyHttpServer(string[] prefixes)
        {
            if (!HttpListener.IsSupported) throw new Exception("HttpListener is not supported");

            listener = new HttpListener();

            foreach(string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
        }

        // Khoi dong server
        public async Task Start()
        {
            listener.Start();
            Console.WriteLine("Http server started");

            do
            {
                Console.WriteLine(DateTime.Now.ToString() + " Waiting a client connect");
                var context = await listener.GetContextAsync();
                Console.WriteLine(DateTime.Now.ToString() + " Client connected");

                await ProcessRequest(context);
                

            } while (listener.IsListening);
        }

        // Xu ly noi dung tra ve cho client
        private async Task ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            Console.WriteLine($"{request.HttpMethod} | {request.RawUrl} | {request.Url.AbsolutePath}");

            var outputStream = response.OutputStream;

            switch(request.Url.AbsolutePath)
            {
                case "/":
                    {
                        var buffer = Encoding.UTF8.GetBytes("Xin chao cac ban");
                        response.ContentLength64 = buffer.Length; // cho client biet do dai content
                        await outputStream.WriteAsync(buffer, 0, buffer.Length);
                    }

                    break;
                case "/json":
                    {
                        response.Headers.Add("Content-Type", "application/json");

                        var product = new
                        {
                            Name = "Macbook Pro 2022",
                            Price = 2000
                        };

                        string json = JsonConvert.SerializeObject(product);
                        var buffer = Encoding.UTF8.GetBytes(json);
                        response.ContentLength64 = buffer.Length;
                        await outputStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                    break;
                case "/code.png":
                    {
                        response.Headers.Add("Content-Type", "image/png");

                        var buffer = await File.ReadAllBytesAsync("code.png");
                        response.ContentLength64 = buffer.Length;
                        await outputStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                    break;
                default:
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        var buffer = Encoding.UTF8.GetBytes("NOT FOUND");
                        response.ContentLength64 = buffer.Length;
                        await outputStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                    break;
            }

            outputStream.Close(); // Dong ket noi va tra ve response cho client
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var myHttpServer = new MyHttpServer(new string[] { "http://*:8080/" });
            await myHttpServer.Start();
        }
    }
}