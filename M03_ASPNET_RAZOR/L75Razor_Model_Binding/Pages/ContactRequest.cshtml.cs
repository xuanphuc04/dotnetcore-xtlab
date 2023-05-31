using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace L75Razor_Model_Binding.Pages
{
    public class ContactRequestModel : PageModel
    {
        //[BindProperty(SupportsGet = true)]
        [BindProperty]
        [DisplayName("Id")]
        public int UserId { get; set; }

        [BindProperty]
        [DisplayName("Họ tên")]
        public string UserName { get; set; }

        [BindProperty]
        [DisplayName("Email")]
        public string Email { get; set; }

        public void OnGet()
        {
        }
    }
}
