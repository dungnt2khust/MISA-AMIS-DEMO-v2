using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Warehouse : BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid warehouse_id { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [MisaUnique]
        [MisaDisplayName("Mã kho")]
        public string warehouse_code { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [MisaUnique]
        public string warehouse_name { get; set; }

        /// <summary>
        /// Id tài khoản kho
        /// </summary>
        public Guid? warehouse_account_id { get; set; }

        /// <summary>
        /// Địa chỉ kho
        /// </summary>
        public string warehouse_address { get; set; }
    }

}
