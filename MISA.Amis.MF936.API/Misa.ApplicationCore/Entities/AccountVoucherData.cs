using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class AccountVoucherData
    {
        /// <summary>
        /// Thông tin chính
        /// </summary>
        public AccountVoucher in_inward { get; set; }

        /// <summary>
        /// Các hàng tiền
        /// </summary>
        public List<AccountVoucherDetail> in_inward_detail { get; set; }
    }
}
