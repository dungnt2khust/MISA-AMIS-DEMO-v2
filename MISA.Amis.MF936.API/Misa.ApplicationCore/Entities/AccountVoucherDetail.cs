using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class AccountVoucherDetail : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid accountvoucherdetail_id { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Mã hàng")]
        [MisaUnique]
        [MisaExport("Mã hàng")]
        public Guid commodity_id { get; set; }

        /// <summary>
        /// Id chứng từ
        /// </summary>
        public Guid? accountvoucher_id { get; set; }

        /// <summary>
        /// Id kho
        /// </summary>
        public Guid warehouse_id { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal debit_amount { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public Guid debit_account_id { get; set; }

        /// <summary>
        /// Tài khoản có
        /// </summary>
        public Guid credit_account_id { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string lot_number { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime expiry { get; set; }

        /// <summary>
        /// Thành tiền
        /// </summary>
        public decimal total_price { get; set; }

        /// <summary>
        /// Tên hàng hóa
        /// </summary>
        [MisaNotMap]
        public string commodity_name { get; set; }

        /// <summary>
        /// Mã hàng hóa
        /// </summary>
        [MisaNotMap]
        public string commodity_code { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [MisaNotMap]
        public string warehouse_code { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [MisaNotMap]
        public string warehouse_name { get; set; }

        /// <summary>
        /// Số Tk nợ
        /// </summary>
        [MisaNotMap]
        public string debit_account_number { get; set; }

        /// <summary>
        /// Tên TK nợ
        /// </summary>
        [MisaNotMap]
        public string debit_account_name { get; set; }

        /// <summary>
        /// Số TK có
        /// </summary>
        [MisaNotMap]
        public string credit_account_number { get; set; }

        /// <summary>
        /// Tên TK có
        /// </summary>
        [MisaNotMap]
        public string credit_account_name { get; set; } 
    }
}
