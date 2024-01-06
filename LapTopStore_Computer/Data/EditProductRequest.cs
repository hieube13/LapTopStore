using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class EditProductRequest
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public float ProductDiscount { get; set; }
        public int CategoryId { get; set; }
        public int ProductInStock { get; set; }
        public bool HomeFlag { get; set; }
        public bool BestSeller { get; set; }
        public string PrImage1 { get; set; }
        public string PrImage2 { get; set; }
        public string PrImage3 { get; set; }
        public string PrImage4 { get; set; }
        public DateTime? ProductCreated { get; set; }
        public int ImageChange1 { get; set; }
        public int ImageChange2 { get; set; }
        public int ImageChange3 { get; set; }
        public int ImageChange4 { get; set; }
        public string? Sign { get; set; }
        public int ColorCategory { get; set; }
        public int CpuCategory { get; set; }
        public int RamCategory { get; set; }
        public int HddCategory { get; set; }
        public int ScreenCategory { get; set; }
        public int OsCategory { get; set; }
    }
}
