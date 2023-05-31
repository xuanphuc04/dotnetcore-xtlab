using L54FluentAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L53Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(100)] 
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        // *Khong bat buoc
        // Collect navigation: Dieu huong tap hop
        // Chi dung de truy xuat, khong them vao database
        // Lien ket 1 - N: Bang cha chua tap hop bang con
        // null default -> De truy xuat phai dung Entry.Collection.Load
        public List<Product> Products { get; set; }

        // Tạo mối quan hệ 1-1 với CategoryDetail
        public CategoryDetail CategoryDetail { get; set; }
    }
}
