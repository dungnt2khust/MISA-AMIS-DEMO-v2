using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Account : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid account_id { get; set; }
    
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public int account_number { get; set; }
        
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string account_name { get; set; }
    }
}
