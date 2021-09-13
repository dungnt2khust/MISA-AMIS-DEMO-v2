using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    public class CustomerGroup : BaseEntity
    {
        /// <summary>
        /// Id nhóm khách hàng
        /// </summary>
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>

        public string CustomerGroupCode { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }
        
        /// <summary>
        /// Mô tả chi tiết
        /// </summary>
        public string Description { get; set; }
    }
}
