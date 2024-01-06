using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class CreateProductAttribute
    {
        public int ProductID { get; set; }
        public List<int> AttrIDList { get; set; }
    }
}
