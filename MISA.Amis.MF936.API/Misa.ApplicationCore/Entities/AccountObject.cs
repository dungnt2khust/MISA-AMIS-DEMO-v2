using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class AccountObject:BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid account_object_id { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Mã khách hàng")]
        [MisaUnique]
        [MisaExport("Mã khách hàng")]
        public string account_object_code { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Tên khách hàng")]
        [MisaExport("Tên khách hàng")]
        public string account_object_name { get; set; }

        /// <summary>
        /// Id nhóm khách hàng
        /// </summary>
        public Guid? account_object_group_id { get; set; }

        /// <summary>
        /// Id nhân viên bán hàng
        /// </summary>
        public Guid? employee_id { get; set; }


        /// <summary>
        /// Địa chỉ liên hệ
        /// </summary>
        public string contact_address { get; set; }

        #endregion
    }
}
