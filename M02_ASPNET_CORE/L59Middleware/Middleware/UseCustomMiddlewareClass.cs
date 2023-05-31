namespace L59Middleware.Middleware
{
    public static class UseCustomMiddlewareClass
    {
        public static void UseCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<CustomMiddleware>();
        }

        public static void UseSecondCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<SecondCustomMidldleware>();
        }

    }
}
