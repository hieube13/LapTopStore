using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data.Product
{
    public class ProductDetailAndRelated
    {
        public int ResponseCode { get; set; }
        public ProductDetails? product { get; set; }
        public List<ProductDetails>? listRelated { get; set; }
    }
}
