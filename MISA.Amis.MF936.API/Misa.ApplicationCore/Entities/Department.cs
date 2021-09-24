using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Department:BaseEntity
    {
        #region Property
        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string department_name { get; set; }

        #endregion
    }
}
