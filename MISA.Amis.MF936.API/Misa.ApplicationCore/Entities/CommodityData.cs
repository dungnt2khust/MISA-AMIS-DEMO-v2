using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class CommodityData
    {
        public Commodity dataMaster { get; set; }

        public List<Unit> dataDetail { get; set; }
    }
}
