using L61IServiceCollection_MapWhen_Configuration_IOptions_DI.Middleware;
using L61IServiceCollection_MapWhen_Configuration_IOptions_DI.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddSingleton<TestOptionsMiddleware>();

// Them Options lam tham so khoi tao cho cac doi tuong
var testOptionsConfigure = builder.Configuration.GetSection("TestOptions");
// Them TestOptions vao IOptions
services.AddOptions();
services.Configure<TestOptions>(testOptionsConfigure);


var app = builder.Build();

// Su dung custom middleware
app.UseMiddleware<TestOptionsMiddleware>();

app.MapGet("/", async (context) =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.MapGet("/show-options", async (context) =>
{
    //var configuration = context.RequestServices.GetService<IConfiguration>();
    //var testOptions = configuration.GetSection("TestOptions");
    //var opt_key1 = testOptions["opt_key1"];
    //var opt_key2 = testOptions.GetSection("opt_key2");
    //var k1 = opt_key2["k1"];
    //var k2 = opt_key2["k2"];

    //string noti = $"opt_key1: {opt_key1}\n" + $"k1: {k1}\n" + $"k2: {k2}\n";
    //await context.Response.WriteAsync(noti);

    //await context.Response.WriteAsync("------------------------\n");
    //// Co the chuyen configuration section thanh doi tuong
    //var testOptions1 = configuration.GetSection("TestOptions").Get<TestOptions>();
    //var opt_key11 = testOptions1.opt_key1;
    //var opt_key21 = testOptions1.opt_key2;
    //var k11 = opt_key21.k1;
    //var k21 = opt_key21.k2;
    //string noti1 = $"opt_key1: {opt_key11}\n" + $"k1: {k11}\n" + $"k2: {k21}\n";
    //await context.Response.WriteAsync(noti1);

    await context.Response.WriteAsync("Show Options");
});


app.Run();
