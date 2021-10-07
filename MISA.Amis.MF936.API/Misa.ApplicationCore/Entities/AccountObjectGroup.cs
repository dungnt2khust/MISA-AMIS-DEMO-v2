using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class AccountObjectGroup : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid accountobjectgroup_id { get; set; }
        /// <summary>
        /// Mã nhóm đối tượng
        /// </summary>
        [MisaRequired]
        [MisaUnique]
        [MisaDisplayName("Mã nhóm khách hàng")]
        public string account_object_group_code { get; set; }
        /// <summary>
        /// Tên nhóm đối tượng
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Tên nhóm khách hàng")]
        public string account_object_group_name { get; set; }
        #endregion
    }
}
