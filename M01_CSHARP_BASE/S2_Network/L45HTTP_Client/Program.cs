using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;

namespace L45HTTP_Client
{
    internal class Program
    {
        public static void ShowHeaders(HttpResponseHeaders headers)
        {
            Console.WriteLine("Show Headers: ");
            headers.ToList().ForEach(header => Console.WriteLine($"{header.Key,30}: {header.Value}"));
        }


        //# Su dung GetAsync de thuc hien phuong thuc GET
        //## Get noi dung trang web
        public static async Task<string> GetWebContent(string url)
        {

            try
            {
                using var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                // Hien thi cac headers
                ShowHeaders(response.Headers);

                string content = await response.Content.ReadAsStringAsync();

                return content;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
        
        //## Download file
        public static async Task<byte[]> DownloadDataBytes(string url)
        {

            try
            {
                using var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                // Hien thi cac headers
                ShowHeaders(response.Headers);

                byte[] bytes = await response.Content.ReadAsByteArrayAsync();

                return bytes;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        
        //# Su dung SendAsync thuc hien: GET/PUT/POST/DELETE
        //## GET noi dung trang web
        public static async Task<string> GetWebContentSendAsync(string url)
        {
            using var client = new HttpClient();

            var httpMessageRequest = new HttpRequestMessage();
            httpMessageRequest.Method = HttpMethod.Get;
            httpMessageRequest.RequestUri = new Uri(url);
            //...

            var response = await client.SendAsync(httpMessageRequest);

            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

        //## POST
        public static async Task<string> PostWebContentSendAsync(string url)
        {
            using var client = new HttpClient();

            var httpMessageRequest = new HttpRequestMessage();
            httpMessageRequest.Method = HttpMethod.Post;
            httpMessageRequest.RequestUri = new Uri(url);
            //...

            // Content
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("key1", "value 1"));
            parameters.Add(new KeyValuePair<string, string>("key2", "value 2"));
            parameters.Add(new KeyValuePair<string, string>("key2", "value 3"));

            var content = new FormUrlEncodedContent(parameters);

            httpMessageRequest.Content = content;


            var response = await client.SendAsync(httpMessageRequest);

            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

        //## POST JSON
        public static async Task<string> PostWebContentJson(string url)
        {
            using var client = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri= new Uri(url);

            string dataJson = @"{
                ""key1"": ""value 1"",
                ""key2"": 1234
            }";
            Console.WriteLine("Data Json: " + dataJson);

            var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
            httpRequestMessage.Content = content;

            var response = await client.SendAsync(httpRequestMessage);

            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        static async Task Main(string[] args)
        {
            string content = await GetWebContent("https://www.xuanphuc.space");
            //Console.WriteLine(content);

            // Download file
            var bytes = await DownloadDataBytes("https://raw.githubusercontent.com/xuanthulabnet/learn-cs-netcore/master/imgs/cs026.png");

            string filePath = "download-bytes.png";
            using var fileStream = new FileStream(path: filePath, mode: FileMode.Create, access: FileAccess.Write, share: FileShare.None);
            fileStream.Write(bytes, 0, bytes.Length);


            var html = await GetWebContentSendAsync("https://www.xuanphuc.space");
            //Console.WriteLine(html);

            var htmlPost = await PostWebContentSendAsync("https://postman-echo.com/post");
            //Console.WriteLine(htmlPost);

            var htmlPostJson = await PostWebContentJson("https://postman-echo.com/post");
            Console.WriteLine(htmlPostJson);
        }
    }
}