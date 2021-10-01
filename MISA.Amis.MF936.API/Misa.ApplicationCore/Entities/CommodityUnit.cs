using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class CommodityUnit : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid commodityandunit_id { get; set; }

        /// <summary>
        /// Id hàng hóa
        /// </summary>
        public Guid commodity_id { get; set; }

        /// <summary>
        /// Id đơn vị tính
        /// </summary>
        public Guid unit_id { get; set; }

        /// <summary>
        /// Đơn vị tính chính
        /// </summary>
        public int is_main_unit { get; set; }

        /// <summary>
        /// Tỉ lệ so vơi đơn vị tính chính
        /// </summary>
        public decimal rate { get; set; }

        /// <summary>
        /// Tên hàng hóa
        /// </summary>
        [MisaNotMap]
        public string commodity_name { get; set; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        [MisaNotMap]
        public string unit_name { get; set; }

        /// <summary>
        /// Được chọn
        /// </summary>
        public int selected { get; set; }
    }
}
