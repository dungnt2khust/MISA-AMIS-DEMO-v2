using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class AccountVoucher:BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid accountvoucher_id { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Mã chứng từ")]
        [MisaUnique]
        [MisaExport("Mã chứng từ")]
        public string voucher_code { get; set; }

        /// <summary>
        /// Ngày hạch toán
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Ngày hạch toán")]
        [MisaExport("Ngày hạch toán")]
        public DateTime mathematics_date { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Ngày chứng từ")]
        [MisaExport("Ngày chứng từ")]
        public DateTime voucher_date { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Người giao / Người nhận
        /// </summary>
        public string contact_name { get; set; }

        /// <summary>
        /// Mã đối tượng
        /// </summary>
        public Guid? accountobject_id { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public string voucher_type { get; set; }

        /// <summary>
        /// Trạng thái ghi sổ
        /// </summary>
        public int is_mention { get; set; }
        
        /// <summary>
        /// Tông tiền
        /// </summary>
        public decimal total_price { get; set; }

        /// <summary>
        /// Kèm theo
        /// </summary>
        public string voucher_attach { get; set; }

        /// <summary>
        /// Mã đối tượng
        /// </summary>
        [MisaNotMap]
        public string account_object_code { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        [MisaNotMap]
        public string account_object_name { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        [MisaNotMap]
        public string contact_address { get; set; }

        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid? employee_id { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MisaNotMap]
        public string employee_code { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MisaNotMap]
        public string employee_name { get; set; }
        #endregion
    }
}
