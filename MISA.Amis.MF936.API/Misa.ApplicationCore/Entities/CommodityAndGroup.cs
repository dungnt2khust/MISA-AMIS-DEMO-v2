using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class CommodityAndGroup
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid commodityandgroup_id { get; set; }
        
        /// <summary>
        /// Id hàng hoá
        /// </summary>
        public Guid commodity_id { get; set; }
    
        /// <summary>
        /// Id nhóm hàng hoá
        /// </summary>
        public Guid commoditygroup_id { get; set; }
    }
}
