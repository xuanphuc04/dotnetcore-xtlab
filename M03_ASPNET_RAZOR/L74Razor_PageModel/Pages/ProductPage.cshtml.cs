using L74Razor_PageModel.Models;
using L74Razor_PageModel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L74Razor_PageModel.Pages
{
    public class ProductPageModel : PageModel
    {
		private readonly ILogger<ProductPageModel> logger;
		public readonly ProductsService productsService;
		public Product Product { set; get; }
		//private List<Product> Products { set; get; }
		public ProductPageModel(ILogger<ProductPageModel> _logger, ProductsService _productsService) {
			logger = _logger;
			logger.LogInformation("Create ProductPageModel...");
			productsService = _productsService;
		}

        /*
         * Các phương thức: OnGet, OnGetAbc, OnPost,... Http Request
         * Kiểu trả về: void hoặc IActionResult
         */

        public void OnGet(int? id)
        {
			// Nếu phương thức OnGet có tham số là "id"
			// thì tự động gán giá trong ViewData có key là "id"

			if (Request.RouteValues["id"] != null)
			{
				var id1 = Request.RouteValues["id"].ToString();
				ViewData["Title"] = $"Trang sản phẩm id:{id1} - {id}";
			}
			else
			{
				ViewData["Title"] = "Danh sách sản phẩm";
			}

			// Lấy dữ liệu Product
			if(id != null)
			{
				Product = productsService.FindProduct((int)id);
			}
			//Products = productsService.GetAllProducts();
		}

		// /product/{id:int?}?handler=lastproduct
		public IActionResult OnGetLastProduct()
		{
			ViewData["Title"] = "Sản phẩm cuối";
			Product = productsService.GetAllProducts().LastOrDefault();

			if(Product != null)
			{
				// Render trang cshtml
				return Page();
			}
			else
			{
				// Trả trực tiếp về content bỏ qua cshtml
				return Content("Không có sản phẩm nào");
			}
		}


		public IActionResult OnGetRemoveAll()
		{
			productsService.GetAllProducts().Clear();
			// Chuyển về trang ProductPage
			return RedirectToPage("ProductPage");
		}

		public IActionResult OnGetLoad()
		{
			productsService.LoadProducts();
			// Chuyển về trang ProductPage
			return RedirectToPage("ProductPage");
		}

		public IActionResult OnPostDelete(int? id)
		{
			if(id != null)
			{
				var product = productsService.FindProduct((int)id);
				if(product  != null)
				{
					productsService.GetAllProducts().Remove(product);
				}
			}
			
			// Chuyển về trang ProductPage với RouteValue id = ""
			return RedirectToPage("ProductPage", new {id = string.Empty});
		}
	}
}
