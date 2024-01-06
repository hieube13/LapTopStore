using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class ProductImageResponse
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
        public List<string> ImageNames { get; set; }

    }
}
