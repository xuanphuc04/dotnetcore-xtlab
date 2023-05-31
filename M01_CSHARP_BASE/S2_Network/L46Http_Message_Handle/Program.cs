namespace L46Http_Message_Handle
{
    internal class Program
    {
        public static async Task<string> TestPost(string url)
        {
            using var client = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(url);
            //...

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("key1", "value 1"));
            parameters.Add(new KeyValuePair<string, string>("key2", "value 2"));

            httpRequestMessage.Content = new FormUrlEncodedContent(parameters);

            var response = await client.SendAsync(httpRequestMessage);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        static async Task Main(string[] args)
        {
            string url = "https://postman-echo.com/post";
            string html1 = await TestPost(url);
            Console.WriteLine(html1);

        }
    }
}