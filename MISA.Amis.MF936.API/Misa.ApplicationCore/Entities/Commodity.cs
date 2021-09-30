using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Commodity : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid commodity_id { get; set; }
        /// <summary>
        /// Tên hàng hóa
        /// </summary>
        public string commodity_name { get; set; }
        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public Guid debit_account { get; set; }
        /// <summary>
        /// Số tk nợ
        /// </summary>
        [MisaNotMap]
        public string debit_account_number { get; set; }

        /// <summary>
        /// Tên TK nợ
        /// </summary>
        [MisaNotMap]
        public string debit_account_name { get; set; }
        /// <summary>
        /// Tài khoản có
        /// </summary>
        public Guid credit_account { get; set; }
        /// <summary>
        /// Số tk nợ
        /// </summary>
        [MisaNotMap]
        public string credit_account_number { get; set; }

        /// <summary>
        /// Tên TK nợ
        /// </summary>
        [MisaNotMap]
        public string credit_account_name { get; set; }
        /// <summary>
        /// ID kho
        /// </summary>
        public Guid warehouse_id { get; set; }
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
        /// Id nhóm hàng hóa
        /// </summary>
        public Guid commoditygroup_id { get; set; }

        /// <summary>
        /// Tên nhóm hàng hóa
        /// </summary>
        [MisaNotMap]
        public string commodity_group_name { get; set; }
        /// <summary>
        /// Mã nhóm hàng hóa
        /// </summary>
        [MisaNotMap]
        public string commodity_group_code { get; set; }
        /// <summary>
        /// Mã hàng
        /// </summary>
        public string commodity_code { get; set; }

        /// <summary>
        /// Đơn giá ngầm định
        /// </summary>
        public decimal debit_amount { get; set; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        public List<Unit> units { get; set; }
    }
}
