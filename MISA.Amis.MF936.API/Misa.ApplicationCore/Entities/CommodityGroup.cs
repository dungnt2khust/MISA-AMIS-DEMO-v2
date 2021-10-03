using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class CommodityGroup : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid commoditygroup_id { get; set; }
        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        public string commodity_group_code { get; set; }
        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string commodity_group_name { get; set; }
        #endregion
    }
}
