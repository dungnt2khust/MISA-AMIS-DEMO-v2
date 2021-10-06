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
        public Guid accountobject_id { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MisaDisplayName("Mã khách hàng")]
        [MisaUnique]
        [MisaExport("Mã khách hàng")]
        public string account_object_code { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [MisaDisplayName("Tên khách hàng")]
        [MisaExport("Tên khách hàng")]
        public string account_object_name { get; set; }

        /// <summary>
        /// Id nhóm khách hàng
        /// </summary>
        public Guid? accountobjectgroup_id { get; set; }

        /// <summary>
        /// Id nhân viên bán hàng
        /// </summary>
        public Guid? employee_id { get; set; }


        /// <summary>
        /// Địa chỉ liên hệ
        /// </summary>
        public string contact_address { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? identity_date { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string identity_number { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string identity_place { get; set; }
        /// <summary>
        /// Là nhà cung cấp
        /// </summary>
        public int? is_supplier { get; set; }
        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string landline_number { get; set; }
        /// <summary>
        /// Đại diện theo PL
        /// </summary>
        public string legal_representation { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string phone_number { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string tax_code { get; set; }
        /// <summary>
        /// Id xưng hô
        /// </summary>
        public Guid? vocative_id { get; set; }
        /// <summary>
        /// Mã nhân viên bán hàng
        /// </summary>
        [MisaNotMap]
        public string employee_code { get; set; }

        /// <summary>
        /// Tên nhân viên bán hàng
        /// </summary>
        [MisaNotMap]
        public string employee_name { get; set; }
        /// <summary>
        /// Xưng hô
        /// </summary>
        [MisaNotMap]
        public string vocative_name { get; set; }

        #endregion
    }
}
