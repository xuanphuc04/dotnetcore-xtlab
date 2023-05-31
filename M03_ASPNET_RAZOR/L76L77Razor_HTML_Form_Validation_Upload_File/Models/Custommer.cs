using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using L76L77Razor_HTML_Form_Validation_Upload_File.Binders;
using L76L77Razor_HTML_Form_Validation_Upload_File.Validation;
using Microsoft.AspNetCore.Mvc;

namespace L76L77Razor_HTML_Form_Validation_Upload_File.Models
{
    public class Custommer
    {
        [DisplayName("Tên khách hàng")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} phải có độ dài từ {2} đến {1} ký tự")]
        [ModelBinder(BinderType = typeof(UserNameBinding))]
        public string CustommerName { set; get; }

        [Display(Name = "Email")]
		[Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [EmailAddress(ErrorMessage = "{0} không đúng định dạng")]
		public string Email { set; get; }

        [DisplayName("Năm sinh")]
		[Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Range(1970, 2022, ErrorMessage = "{0} phải nằm trong khoảng {1} đến {2}")]
        [SoChan]
		public int? YearOfBirth { set; get; }
    }
}
