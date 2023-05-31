using L76L77Razor_HTML_Form_Validation_Upload_File.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace L76L77Razor_HTML_Form_Validation_Upload_File.Pages
{
    public class ContactModel : PageModel
    {
        // Biến môi trường giúp lấy gốc wwwroot
        private readonly IWebHostEnvironment _env;
        public ContactModel(IWebHostEnvironment env) 
        {
            _env = env;
        }

        // CustommerInfo chỉ được khởi tạo khi truy cập
        // với phương thức POST nếu muốn truy cập được với
        // phương thức GET (mà không bị lỗi: null)
        // thì thêm attribute [BindProperty(SupportsGet = true)]
        // hoặc kiểm tra model trong cshtml nếu khác null thì mới render
        [BindProperty]
        public Custommer CustommerInfo { set; get; }

        public string notification;

        // Upload file
        // 1. Thêm property
        // 2. Thêm enctype="multipart/form-data" vào form
        // 3. Thêm control upload
        // 4. Inject biến môi trường từ IWebHostEnvironment để lấy
        //    đường dẫn wwwroot
        // 5. Tạo file stream và lưu file
        //[BindProperty] // (Không cần thêm vẫn bind value được?!)
        //[DataType(DataType.Upload)] // (Không cần thêm vẫn chạy được ?!)
        [DisplayName("Upload file")]
        //[FileExtensions(Extensions = "png,jpg")] //Chỉ sử dụng được với string, cần phải custom lại!
        public IFormFile? FileUpload { set; get; }

        // Upload nhiều files
        [BindProperty]
        [DisplayName("Upload nhiều file")]
        public IFormFile[]? FilesUpload { set; get; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if(ModelState.IsValid)
            {
                if(FileUpload != null)
                {
                    var filePath = Path.Combine(_env.WebRootPath, "upload", FileUpload.FileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    FileUpload.CopyTo(fileStream);
                }

				Console.WriteLine("Files: " + FilesUpload);
				if(FilesUpload != null && FilesUpload.Length > 0)
                {
					foreach (var file in FilesUpload)
					{
						var path = Path.Combine(_env.WebRootPath, "upload", file.FileName);
						using var stream = new FileStream(path, FileMode.Create);
						file.CopyTo(stream);
					}
				}

			} else
            {
                notification = "Dữ liệu không phù hợp";
            }
            
            
        }
    }
}
