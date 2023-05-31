namespace L59Middleware.Middleware
{
    public class SecondCustomMidldleware : IMiddleware
    {
        /*
         * * Neu URL: /xxx.html
         *  - Khong goi Middleware phia sau
         *  - Tra ve: Ban khong duoc truy cap
         *  - Header: SecondCustomMiddleware: Ban khong duoc truy cap
         * * Neu URL: != xxx.html
         *  - Header: SecondCustomMiddleware: Ban duoc truy cap
         *  - Chuyen context cho Middleware phia sau
         */

        // Doi voi loai Middleware ke thua tu IMiddleware
        // thi phai dang ky dich vu DI truoc (AddSingleton...)

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == "/xxx.html")
            {
                var response = context.Response;
                // Neu them Headers sau khi WriteAsync thi se phat sinh Exceiption
                response.Headers.Add("SecondCustomMiddleware", "Ban khong duoc truy cap");

                var dataFromCustomMiddleware = context.Items["DataCustomMiddleware"];
                if(dataFromCustomMiddleware != null)
                {
                    await response.WriteAsync((string)dataFromCustomMiddleware);
                }
                await response.WriteAsync("<h1>Ban khong duoc truy cap</h1>");
            }
            else
            {
                var response = context.Response;
                response.Headers.Add("SecondCustomMiddleware", "Ban duoc truy cap");

                var dataFromCustomMiddleware = context.Items["DataCustomMiddleware"];
                if (dataFromCustomMiddleware != null)
                {
                    await response.WriteAsync((string)dataFromCustomMiddleware);
                }
                await next(context);
            }

        }
    }
}
