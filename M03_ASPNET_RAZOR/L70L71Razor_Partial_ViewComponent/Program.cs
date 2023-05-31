using L70L71Razor_Partial_ViewComponent.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Thêm ProductService vào ServiceCollection
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

/*
	Partial View: là file .cshtml không có @page
		- Chia nhỏ page thành các file nhỏ
		- Sử dụng lại các thành phần (tránh trùng lặp code)
		- Có thể tạo file Partial View trong các thư mục:
		  /Pages/_PartialView.cshtml
		  /Pages/Shared/_PartialView.cshtml
		  /Views/Shared/_PartialView.cshtml

	Component giống Partial View nhưng sử dụng được với
	kỹ thuật DI = Mini Razor Page
		- Các Component có thể lưu trữ trong các thư mục sau
			/Views/{Controller Name}/Components/{View Component Name}/{View Name}
			/Views/Shared/Components/{View Component Name}/{View Name}
			/Pages/Shared/Components/{View Component Name}/{View Name}
*/