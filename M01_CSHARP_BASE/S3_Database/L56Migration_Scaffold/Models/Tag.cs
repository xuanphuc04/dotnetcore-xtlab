using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L56Migration
{
    public class Tag
    {
        //[Key]
        //[StringLength(20)]
        //public string TagId { set; get; }
        [Key]
        public int TagId { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { set; get; }
    }
}