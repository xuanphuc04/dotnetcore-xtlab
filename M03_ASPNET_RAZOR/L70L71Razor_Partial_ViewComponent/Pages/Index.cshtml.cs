using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L70L71Razor_Partial_ViewComponent.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        //public IActionResult OnGet()
        //{
        //	// Trả về trực tiếp một Partial View bỏ qua file .cshtml:
        //	// - Trong PageModel: Partial()
        //	// - Trong Controller: PartialView(), ComponentView() - Trả về component
        //	return Partial("_Message");
        //}
    }
}