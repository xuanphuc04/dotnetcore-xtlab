using L60Map_Request_Response_Cookie_Json_Upload.myLib;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async (context) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
        var html = HtmlHelper.HtmlDocument("Trang chu", menu + HtmlHelper.HtmlTrangchu());
        await context.Response.WriteAsync(html);
    });

    endpoints.MapGet("/request-info", async (context) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
        var info = RequestProcess.RequestInfo(context.Request).HtmlTag("div", "container");
        var html = HtmlHelper.HtmlDocument("Info", menu + info);
        await context.Response.WriteAsync(html);
    });

    endpoints.MapGet("/encoding", async (context) =>
    {
        await context.Response.WriteAsync("Encoding");
    });

    endpoints.MapGet("/cookies/{*action}", async (context) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
        var html = HtmlHelper.HtmlDocument(
                "cookies",
                menu + RequestProcess.Cookies(context.Request, context.Response)
                .HtmlTag("div", "container")
            );
        await context.Response.WriteAsync(html);
    });

    endpoints.MapGet("/json", async (context) =>
    {
        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);

        var product = new
        {
            Name = "Macbook pro M2",
            Price = 1500,
            Date = new DateTime(2022, 10, 1)
        };

        context.Response.ContentType = "application/json";

        var jsonTest = JsonConvert.SerializeObject(product);

        await context.Response.WriteAsync(jsonTest);
    });

    // MapMethods ho tro nhieu phuong thuc trong cung dia chi
    endpoints.MapMethods("/form", new string[] { "GET", "POST" }, async (context) =>
    {

        var menu = HtmlHelper.MenuTop(HtmlHelper.DefaultMenuTopItems(), context.Request);
        string htmlForm = RequestProcess.FormProcess(context.Request).HtmlTag("div", "container");
        var html = HtmlHelper.HtmlDocument("Trang chu", menu + htmlForm);
        await context.Response.WriteAsync(html);
    });
});

app.Run();
