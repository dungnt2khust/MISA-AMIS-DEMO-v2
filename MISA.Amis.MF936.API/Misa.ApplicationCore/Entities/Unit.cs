using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Unit : BaseEntity
    {
        #region Property
        /// <summary>
        /// Id
        /// </summary>
        public Guid unit_id { get; set; }

        /// <summary>
        /// Mã đơn vị tính
        /// </summary>
        [MisaUnique]
        [MisaRequired]
        [MisaDisplayName("Mã đơn vị tính")]
        public string unit_code { get; set; }
        /// <summary>
        /// Mã đơn vị tính
        /// </summary> 
        [MisaRequired]
        public string unit_name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Tỷ lệ chuyển đổi
        /// </summary>
        [MisaNotMap]
        public decimal rate  { get; set; }

        /// <summary>
        /// Là đơn vị chính
        /// </summary>
        [MisaNotMap]
        public int is_main_unit { get; set; }

        /// <summary>
        /// Đang được chọn
        /// </summary>
        [MisaNotMap]
        public int selected { get; set; }

        #endregion
    }
}
