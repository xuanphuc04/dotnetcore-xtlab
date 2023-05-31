namespace L59Middleware.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        public CustomMiddleware(RequestDelegate _next) {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Xu ly
            Console.WriteLine("URL: " + context.Request.Path);
            //await context.Response.WriteAsync($"<p>{context.Request.Path}</p>");
            // khong su dung WriteAsync vi neu Middleware phia sau co su dung context.Headers
            // se phat sinh Exceiption

            // Su dung context.Items de truyen du lieu cho cac Middleware phia sau
            context.Items.Add("DataCustomMiddleware", $"<p>URL: {context.Request.Path}</p>");

            // Chuyen context cho Middleware tiep theo
            await next(context);
        }
    }
}
