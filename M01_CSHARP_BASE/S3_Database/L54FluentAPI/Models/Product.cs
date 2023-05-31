

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L53Model
{
    public class Product
    {
        public int ProductId { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Column("ProductName", TypeName = "ntext")]
        public string Name { get; set; }

        //[Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        public void Print()
        {
            Console.WriteLine($"{ProductId} - {Name} - {Price}");
        }
    }
}

