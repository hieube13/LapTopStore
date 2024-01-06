﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Computer.Data
{
    public class APICreateProductResponse
    {
        public int ResponseCode { get; set; }
        public string Messenger { get; set; }
        public Dictionary<string, string>? ImageNames { get; set; }

    }
}
