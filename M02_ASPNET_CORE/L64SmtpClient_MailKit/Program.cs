using System.Collections.Immutable;
using L64SmtpClient_MailKit.MailUtils;
using L64SmtpClient_MailKit.Service;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ Options cho MailSettings
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.AddOptions();
builder.Services.Configure<MailSettings>(mailSettings);

// Đăng ký dịch vụ cho MailKitService
builder.Services.AddTransient<MailKitService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/TestSendMail", async (context) =>
{
    var message = await MailUtils.SendMail(
        "xuanphuca1k66@gmail.com",
        "dev.xuanphuc@gmail.com",
        "Test Gửi Email",
        "Xin chào đây chỉ là email test"
    );
    await context.Response.WriteAsync(message);
});
app.MapGet("/testgmail", async (context) =>
{
    var message = await MailUtils.SendGMail(
        "xuanphuca1k66@gmail.com",
        "dev.xuanphuc@gmail.com",
        "Test Gửi Email",
        "Xin chào đây là video tống tiền",
        "xuanphuca1k66@gmail.com",
        "lvjtxsbzieabiwoq"
    );
    await context.Response.WriteAsync(message);
});
app.MapGet("/testmailkit", async (context) =>
{

    var mailKitService = context.RequestServices.GetService<MailKitService>();

    var mailContent = new MailContent();
    mailContent.To = "tientrung14122012@gmail.com";
    mailContent.Subject = "Thư mời phỏng vấn Công ty xuanphuc.space - Vị trí Senior .NET Developer";
    mailContent.Body = "<h1>Test</h1><p>Test description</p>";
    var message = await mailKitService.SendMail(mailContent);

    await context.Response.WriteAsync(message);
});

app.Run();


// Các nguồn gửi mail:
// - Máy chủ localhost: phải cài Mail Server, Mail Transporter
// - Máy chủ Google smtp.gmail.com: Bật IMAP/IPOP,
//   bật xác minh 2 bước và tạo mật khẩu ứng dụng
// - Sử dụng thư viện MailKit:
//      - Cấu hình appsetting.json
//      - Tạo Service có các thuộc tính tương ứng với appsetting
//      - Đăng ký Configure vào hệ thống
//      - Tạo lớp MailKitService có phương thức SendEmail
//      - Đăng ký MailKitService vào dịch vụ