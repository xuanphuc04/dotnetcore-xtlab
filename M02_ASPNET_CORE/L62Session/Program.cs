using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);

// Sử dụng Session
//install-package Microsoft.AspNetCore.Session

// Lưu Session vào bộ nhớ cache của máy chủ
//install-package Microsoft.Extensions.Caching.Memory



// Đăng ký dịch vụ lưu cache trong bộ nhớ cache
builder.Services.AddDistributedMemoryCache();

// Đăng ký dịch vụ cookie
builder.Services.AddSession((option) =>
{
    // Đặt tên cho session - sử dụng ở browser (cookie)
    option.Cookie.Name = "xuanphuc";

    // Thời gian tồn tại của session
    option.IdleTimeout = new TimeSpan(0, 30, 0); // 30 phút
});



var app = builder.Build();



// Session middleware gửi về cho client 1 Cookie chứa ID của Session
// trình duyệt sẽ lưu trữ cookie này và gửi lại cho server vào
// lần truy vấn tiếp theo, Session middleware căn cứ vào ID
// của cookie này để phục hồi lại dữ liệu tại context.Session
// Đăng ký Session middleware
app.UseSession();

app.MapGet("/", async (context) =>
{
    int? count;
    count = context.Session.GetInt32("count");

    if (count == null)
    {
        count = 0;
    }

    count += 1;

    await context.Response.WriteAsync($"So lan truy cap Session: {count}");

    await context.Response.WriteAsync($"Hello World! {context.Session.GetInt32("count1")}");
});

// Đọc ghi Session
app.MapGet("/session", async (context) =>
{

    int? count;
    count = context.Session.GetInt32("count");

    if (count == null)
    {
        count = 0;
    }

    count += 1;

    // Luu bien count vao session
    context.Session.SetInt32("count", count.Value);
    context.Session.SetInt32("count1", 1000);

    await context.Response.WriteAsync($"So lan truy cap Session: {count}");
});

app.Run();
