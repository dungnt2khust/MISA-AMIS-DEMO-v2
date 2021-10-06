using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class CommodityData
    {
        /// <summary>
        /// Thông tin hàng hoá
        /// </summary>
        public Commodity dataMaster { get; set; }
        /// <summary>
        /// Thông tin các đơn vị tính
        /// </summary>
        public List<Unit> dataDetail { get; set; }
    }
}
