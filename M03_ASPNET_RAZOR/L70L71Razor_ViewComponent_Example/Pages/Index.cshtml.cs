using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using L70L71_ViewComponent_Example.Pages.Shared.Components.MessagePage;
namespace L70L71_ViewComponent_Example.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

        public IActionResult OnPost()
        {
            return ViewComponent("MessagePage", new MessagePage.Message
            {
                title = "Thông báo",
                htmlcontent = $"<strong>{this.Request.Form["username"]} đã đăng ký thành công. Đang chuyển đến trang Privacy</strong>",
                secondwait = 10,
                urlredirect = "/privacy"
            });
        }

        public void OnGet() { }

    }
}