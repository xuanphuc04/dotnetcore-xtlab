using L70L71Razor_Partial_ViewComponent.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace L70L71Razor_Partial_ViewComponent.Pages.Shared.Components.ProductBox
{
    //[ViewComponent]
    public class ProductBox : ViewComponent
    {

        // Sử dụng kỹ thuật Dependency Injection
        ProductService productService;
        public ProductBox(ProductService _productService) {
            productService= _productService;
        }


        /* Để tạo ra View Component:
         * 1. Khai báo phương thức thực thi bằng 1 trong cách sau:
         *  - string/IViewComponentResult Invoke([object m])
         *  - async Task<string/IViewComponentResult> InvokeAsync([object m])
         *  
         *  Lưu ý: Khi thêm object vào tham số của Invoke thì khi sử dụng View Component
         *  cần thêm các tham số tương ứng @await Component.InvokeAsync("ProductBox", object m)
         *  
         * 2. Xác định lớp là 1 View Component bằng 1 trong các cách:
         *  - [ViewComponent] làm Attribute của lớp
         *  - Thêm hậu tố ViewComponent vào tên lớp
         *  - Kế thừa ViewComponent (nên dùng cách này)
         * 
         */

        //// Trả về string
        //public string Invoke()
        //{
        //    return "Nội dung của ProductBox1";
        //}

        // Trả về IViewComponentResult
        public IViewComponentResult Invoke(bool orderByAscending = true)
        {
            // Trả về View
            // Mặc định không truyền tham số thì mở file Default.cshtml
            //return View("Default1"); 

            // Truyền Model cho View kiểu string
            //return View<string>("Nội dung Model-string gửi cho View");

            var products = productService.Products;

            List<Product> sortProducts;
            if(orderByAscending == true)
            {
                sortProducts = products.OrderBy(p => p.Price).ToList();
            }
            else
            {
                sortProducts = products.OrderByDescending(p => p.Price).ToList();
            }

            

            return View<List<Product>>(sortProducts);
        }
    }
}
