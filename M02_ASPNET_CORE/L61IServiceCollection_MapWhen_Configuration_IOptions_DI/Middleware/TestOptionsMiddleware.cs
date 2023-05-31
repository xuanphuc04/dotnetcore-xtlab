using L61IServiceCollection_MapWhen_Configuration_IOptions_DI.Options;
using Microsoft.Extensions.Options;

namespace L61IServiceCollection_MapWhen_Configuration_IOptions_DI.Middleware
{
    public class TestOptionsMiddleware : IMiddleware
    {
        TestOptions _testOptions;

        public TestOptionsMiddleware(IOptions<TestOptions> options)
        {
            _testOptions = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Show options in TestOptionsMiddleware\n");
            string noti = $"opt_key1: {_testOptions.opt_key1}\n" + $"k1: {_testOptions.opt_key2.k1}\n" + $"k2: {_testOptions.opt_key2.k2}\n";
            await context.Response.WriteAsync(noti);
            await next(context);
        }
    }
}
