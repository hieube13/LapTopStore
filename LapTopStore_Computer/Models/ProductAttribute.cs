using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapTopStore_Computer.Models
{
    public class ProductAttribute
    {
        [Key]
        public int ProAttrID { get; set; }

        // Khóa ngoại cho Product
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        // Khóa ngoại cho Attribute
        public int AttrId { get; set; }
        [ForeignKey("AttrId")]
        public virtual Attribute Attribute { get; set; }
    }
}
