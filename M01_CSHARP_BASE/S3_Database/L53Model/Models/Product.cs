

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L53Model
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ProductName", TypeName = "ntext")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        // Foreign key tham chieu den ban Category
        // -> Tu dong tao cot CategoryId (giong ten khoa chinh)
        //    va tao FK tham chieu den Category trong database
        // Chi dinh ten khoa ngoai neu khong muon mac dinh giong ten khoa chinh
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } // null default -> Reference navigation: Dieu huong tham chieu

        // Them thuoc tinh de truy xuat khoa ngoai voi LINQ
        public int CategoryId { get; set; }

        public void Print()
        {
            Console.WriteLine($"{ProductId} - {Name} - {Price}");
        }
    }
}

/*
 * * [Table("Product")] - Bang Product
 * * [Key] - Primary key
 * * [Required] - Bat buoc
 * * [StringLength(50)] - Nvarchar(50)
 * * [Column(TypeName = "ntext")] hoac [DataType(DataType.ntext)] - Chi dinh kieu du lieu ntext trong SQL
 *   -> Mac dinh la kieu du lieu cua thuoc tinh
 * * [Column("SanPham" ,TypeName = "ntext")] - Chi dinh ten cot va kieu du lieu
 *   -> Mac dinh ten bang la ten thuoc tinh
 *   
 * * [ForeignKey("CategoryId")] - Chi dinh ten khoa ngoai
 *   -> Mac dinh giong ten khoa chinh cua bang cha neu khong chi dinh
 *   
 *   -------
 * * Reference navigation -> Tao ra foreign key (moi quan he 1 - N)
 * * Collection navigation -> Khong tao ra foreign key, chi dung de
 *   truy xuat cac doi tuong con thuoc doi tuong cha
 *   
 * * Navigation khong tu dong nap du lieu khi truy xuat ma phai su dung:
 *   DBContext.Entry.Reference.Load - Reference navigation
 *   hoac DBContext.Entry.Collection.Load - Collection navigation
 */
