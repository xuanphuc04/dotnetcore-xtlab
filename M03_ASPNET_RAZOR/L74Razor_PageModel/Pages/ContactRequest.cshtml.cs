using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L74Razor_PageModel.Pages
{
	public class ContactRequestModel : PageModel
	{
		private readonly ILogger<ContactRequestModel> _logger;
		public ContactRequestModel(ILogger<ContactRequestModel> logger)
		{
			_logger = logger;
			_logger.LogInformation("Init Contact...");
		}

		public string UserId { get; set; } = "Contact: CONTACT12345";

		public double Tong(double a, double b)
		{
			return a + b;
		}

	}
}
