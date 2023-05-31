using L75Razor_Model_Binding.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System;

namespace L75Razor_Model_Binding.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int? id, Product product)
        {
            Console.WriteLine($"ID normal: {id}");
            Console.WriteLine($"Product ID: {product.Id}");
            Console.WriteLine($"Product Name: {product.Name}");
        }
    }
}

//## Model Binding: Liên kết dữ liệu
//   Dữ liệu gửi đến: theo dạng Key, Value

//## Nguồn gửi dữ liệu đến:
//   - Form HTML(post): HttpRequest.Form["key"]
//   - Query(url, form html - get): HttpRequest.Query["key"]
//   - Header(Request): HttpRequest.Headers["key"]
//   - Route Data: HttpRequest.RouteValues["key"]
//   - Upload
//   - Body(JSON)

//## Đọc dữ liệu gửi đến: HttpRequest (trong Controller, PageModel, View)

//## Đọc dữ liệu trực tiếp (theo key): VD: this.Request.Query["id"]
//   nhược điểm là dữ liệu trả về phải convert sang kiểu dữ liệu mong muốn
//   một cách thủ công

//## Đọc dữ liệu bằng Binding:
//   => Trong ASP.NET không đọc dữ liệu một cách trực tiếp (đọc theo key)
//   mà sẽ sử dụng kỹ thuật Binding (liên kết dữ liệu): tự động đọc dữ liệu
//   và convert sang kiểu dữ liệu mong muốn
//
//   ** Có hai dạng Binding dữ liệu:
//   * Parameter Binding:
//	  - Trong các handler: OnGet(int? id), ...
//		+ Tự động tìm trong tất cả các nguồn dữ liệu xem có key nào là id
//        thì lấy giá trị của id ra
//
//      + Vd: đường dẫn: /product/{id:int?}?id = 2
//        nếu có RouteValues, đọc RouteValues (bỏ qua Query, id lấy gtri của RouteValues)
//        còn nếu không có sẽ đọc đến Query (id lấy giá trị của Query)
//
//      + Để chỉ rõ lấy giá trị Binding từ nguồn dữ liệu nào thì thêm Property
//        vào trước các tham số trong Handler:
//        [FromQuery]
//		  [FromRoute]
//		  [FromForm]
//		  [FromHeader]
//		  [FromBody]
//        => Chú ý: Nếu chỉ thêm [FromQuery] thì tên key phải trùng với tên
//        tham số trong Handler, nếu muốn tên key khác mà vẫn lấy được
//        giá trị vào tham số id thì sử dụng ([FromQuery("sanpham")] int? id)
//        lúc này truy cập: /product?sanpham=1 thì id = 1
//
//		+ Nếu tham số là 1 đối tượng phức tạp
//		  (VD:OnGet(int? id, Product product)
//		  thì sẽ tự động khởi tạo product và tìm trong các nguồn dữ liệu
//		  có key nào trùng với tên thuộc tính của đối tượng thì gán giá trị
//		  cho thuộc tính đó (nếu có tất cả các thuộc tính phù hợp thì cũng
//		  gán tất cả)
//		  VD: /product/3?id=1&name=iPhone
//		  với OnGet([FromRoute]int? id, [FromQuery]Product product)
//		  hoặc /product/3?product.id=1&product.name=iPhone
//		  với OnGet(int? id, Product product)
//		  thì id = 3, product.Id = 1, product.Name = iPhone
//		  
//
//
//		  => Nếu muốn chỉ gán thuộc tính id và name thì sử dụng:
//		  [Bind("id", "name")] vào trước tham số trong handler
//
//	 * Property Binding:
//	  - Thêm property [BindProperty] vào trường dữ liệu cần Binding
//	    tự động đọc trong các nguồn dữ liệu nếu có key phù hợp
//	  - Mặc định chỉ thực hiện Binding trên phương thức POST (submit form)
//	    để hỗ trợ cả phương thức GET thì thêm property [BindProperty(SupportsGet = true)]
