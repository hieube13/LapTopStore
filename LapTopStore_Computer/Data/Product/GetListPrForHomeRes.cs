using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data.Product
{
    public class GetListPrForHomeRes
    {
        public List<ProductWithCategory>? listLenovo { get; set; }
        public List<ProductWithCategory>? listAcer { get; set; }
        public List<ProductWithCategory>? listMac { get; set; }
        public List<ProductWithCategory>? listHP { get; set; }
        public List<ProductWithCategory>? listDELL { get; set; }
        public List<ProductWithCategory>? listMSI { get; set; }
        public List<ProductWithCategory>? listASUS { get; set; }
        public List<ProductWithCategory>? topBestSeller { get; set; }
    }
}
