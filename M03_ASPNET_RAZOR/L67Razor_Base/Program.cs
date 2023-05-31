var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRazorPages();
// Thêm tùy chọn cho Razor page
builder.Services.AddRazorPages().AddRazorPagesOptions((options) =>
{
    // Thay đổi thư mục chứa Razor page
    options.RootDirectory = "/Pages";

    // Rewrite url đến Razor page
    //options.Conventions.AddPageRoute("/FirstPage", "/trang-dau-tien");
    //options.Conventions.AddPageRoute("/SecondPage", "/trang-thu-hai.html");
    // Thường chỉ định url đến Razor page bằng cách: @page "/dia-chi-truy-cap"
});

builder.Services.Configure<RouteOptions>((options) =>
{
    // Chuyển các URL tạo bởi tagHelper (asp-page) thành lowercase
    options.LowercaseUrls = true;
});

var app = builder.Build();

app.MapRazorPages();

app.MapGet("/", () => "Hello World!");

app.Run();

/*
 * Có thể lưu trữ Razor page ở thư mục:
 * - Pages
 * - Areas
 * 
 * @page "url"
 * @bien, @(bieuthuc), @phuongthuc
 * @{
 *      //khoi lenh...
 *      <HTML></HTML>
 * }
 * 
 * @functions {
 *      // khai báo thuộc tính, phương thức
 *      // có thể dùng mọi nơi trong cùng trang với toán tử this
 * }
 * 
 * Rewrite URL
 * 
 * TagHelper -> HTML
 * @addTagHelper
 * 
 * Truyền dữ liệu giữa các phương thức trong Razor page
 * ViewData["key"]
 * 
 * - Tự động thêm (layout, tagHelpers,...) vào các trang Razor
 * ViewStart.cshtml
 * ViewImports.cshtml
 * 
 */
