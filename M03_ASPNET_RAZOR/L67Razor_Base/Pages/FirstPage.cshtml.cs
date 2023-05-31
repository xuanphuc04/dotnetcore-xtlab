using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L67Razor_Base.Pages
{
    public class FirstPageModel : PageModel
    {
        // Tự động gọi khi truy cập không có query trùng với handler nào
        public void OnGet()
        {
            Console.WriteLine("Test GET");
        }

        // Tự động gọi khi địa chỉ có query: ?handler=xyz
        public void OnGetXyz()
        {
            Console.WriteLine("OnGetXyz");
        }
    }
}
