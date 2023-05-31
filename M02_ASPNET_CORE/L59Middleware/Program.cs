using L59Middleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Dang ky Middleware ke thua tu IMiddleware truoc khi su dung
builder.Services.AddSingleton<SecondCustomMidldleware>();

var app = builder.Build();




//app.UseMiddleware<CustomMiddleware>();
// khong can su dung cach tren khi mo rong phuong thuc cho
// lop WebApplication
app.UseCustomMiddleware();

app.UseSecondCustomMiddleware();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/about", async (context) =>
    {
        await context.Response.WriteAsync("Day la trang gioi thieu");
    });

    endpoints.MapGet("/product", async (context) =>
    {
        await context.Response.WriteAsync("Day la trang san pham");
    });

});

// Re nhanh pipeline
// Thuc hien neu khong bi cac Middleware phia truoc chan lai
app.Map("/admin", (app1) =>
{
    //... Cac Middleware cua nhanh
    app1.UseRouting();
    app1.UseEndpoints((endpoints) =>
    {
        endpoints.MapGet("/product", async (context) =>
        {
            await context.Response.WriteAsync("Trang quan ly san pham");
        });

        endpoints.MapGet("/user", async (context) =>
        {
            await context.Response.WriteAsync("Trang quan ly nguoi dung");
        });
    });

    app1.Run(async (context) =>
    {
        await context.Response.WriteAsync("Day la trang admin");
    });
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Xin chao cac ban");
});

app.Run();
