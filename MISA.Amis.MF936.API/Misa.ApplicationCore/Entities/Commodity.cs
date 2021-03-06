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
        [MisaRequired]
        [MisaDisplayName("Tên hàng hoá")]
        public string commodity_name { get; set; }
        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public Guid? debit_account_id { get; set; }
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
        public Guid? credit_account_id { get; set; }
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
        /// Tài khoản kho
        /// </summary>
        public Guid? warehouse_account_id{ get; set; }

        /// <summary>
        /// Số tk nợ
        /// </summary>
        [MisaNotMap]
        public string warehouse_account_number { get; set; }

        /// <summary>
        /// Tên TK nợ
        /// </summary>
        [MisaNotMap]
        public string warehouse_account_name { get; set; }
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
        [MisaNotMap]
        public List<Guid> commoditygroup_id { get; set; }

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
        [MisaRequired]
        [MisaUnique]
        [MisaDisplayName("Mã hàng hoá")]
        public string commodity_code { get; set; }

        /// <summary>
        /// Đơn giá ngầm định
        /// </summary>
        public decimal debit_amount { get; set; } 
        /// <summary>
        /// Số lượng tồn
        /// </summary>

        public decimal quantity { get; set; }
        /// <summary>
        /// Đơn vị tính
        /// </summary>
        [MisaNotMap]
        public List<CommodityUnit> units { get; set; }
        /// <summary>
        /// Đơn vị tính chính
        /// </summary>
        [MisaNotMap]
        public Guid? unit_id { get; set; }
    }
}
