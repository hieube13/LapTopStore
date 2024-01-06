using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LapTopStore_Computer.Models;
using Attribute = LapTopStore_Computer.Models.Attribute;

namespace LapTopStore_Computer.Data
{
    public class GetListAttribute
    {
        public List<Attribute> AttributeColor { get; set; }
        public List<Attribute> AttributeCPU { get; set; }
        public List<Attribute> AttributeRAM { get; set; }
        public List<Attribute> AttributeScreen { get; set; }
        public List<Attribute> AttributeDriver { get; set; }
        public List<Attribute> AttributeSystem { get; set; }
    }
}
