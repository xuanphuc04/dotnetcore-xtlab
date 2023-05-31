/*
 * Host (IHost) object:
 *  - Dependency Injection (DI): IServiceProvider (ServiceCollection)
 *  - Logging (ILogging)
 *  - Configuration
 *  - IHostedService => StartAsync: Run HTTP Server (Kestrel Http)
 * 
 * 1) Tao IHostBuider
 * 2) Cau hinh, dang ky cac dich vu (goi ConfigureWebHostDefaults)
 * 3) IHostBuider.Buid() => Tao Host (IHost)
 * 4) Host.Run() => Chay IHostedService => Chay StartAsync
 *    => Run server
 *    
 * * ConfigureServices(IServiceCollection services){}
 *   -> Dang ky cac dich vu cua ung dung (DI)
 * * Configure(IApplicationBuider app, IWebHostEnviroment env){}
 *   -> Xay dung pipeline (chuoi Middleware) va tra ve response
 */



var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

//-----------------------------------------------------------------
// File luu trong thu muc wwwroot
app.UseStaticFiles();

//-----------------------------------------------------------------
// EnpointRoutingMiddleware
// Dieu huong cac request va tao ra cac endpoint
app.UseRouting();
// UseRouting phai di kem voi UseEndpoints
app.UseEndpoints((enpoints) =>
{
    // GET /
    enpoints.MapGet("/", async (context) =>
    {
        string html = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>Trang web đầu tiên</title>
                    <link rel=""stylesheet"" href=""lib/bootstrap/css/bootstrap.min.css"" />
                    <script src=""lib/jquery/jquery.min.js""></script>
                    <script src=""lib/bootstrap/js/bootstrap.js""></script>


                </head>
                <body>
                    <nav class=""navbar navbar-expand-lg navbar-dark bg-danger"">
                            <a class=""navbar-brand"" href=""#"">Brand-Logo</a>
                            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#my-nav-bar"" aria-controls=""my-nav-bar"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                                    <span class=""navbar-toggler-icon""></span>
                            </button>
                            <div class=""collapse navbar-collapse"" id=""my-nav-bar"">
                            <ul class=""navbar-nav"">
                                <li class=""nav-item active"">
                                    <a class=""nav-link"" href=""#"">Trang chủ</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link"" href=""#"">Học HTML</a>
                                </li>
                            
                                <li class=""nav-item"">
                                    <a class=""nav-link disabled"" href=""#"">Gửi bài</a>
                                </li>
                        </ul>
                        </div>
                    </nav> 
                    <p class=""display-4 text-danger"">Đây là trang đã có Bootstrap</p>
                </body>
                </html>
    ";
        
        await context.Response.WriteAsync(html);
    });

    enpoints.MapGet("/about", async (context) =>
    {
        await context.Response.WriteAsync("Trang gioi thieu");
    });

    enpoints.MapGet("/contact", async (context) =>
    {
        await context.Response.WriteAsync("Trang lien he");
    });
});

//---------------------------------------------------------------
// Terminate Middleware (middleware cuoi cung)
// neu thuc hien thanh cong thi khong thuc hien cac middleware dung sau no (khong thuc hien Run)
app.Map("/abc", (app1) =>
{
    app1.Run(async (context) =>
    {
        await context.Response.WriteAsync("Noi dung tra ve tu ABC");
    });
});

// Trong thuc te khong su dung cach nay de xu ly middleware cuoi cung
// ma su dung UseStatusCodePages
// Terminate Middleware (middleware cuoi cung)
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("xin chao cac ban");
//});

//---------------------------------------------------------------
// StatusCodePage xu ly request cuoi cung trong pipeline
// neu khong co middleware nao xu ly
// co the dat o dau hoac o cuoi deu duoc
app.UseStatusCodePages();


app.Run();