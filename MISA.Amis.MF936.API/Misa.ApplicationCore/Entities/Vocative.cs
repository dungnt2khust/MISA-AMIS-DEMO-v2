using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Vocative : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid vocative_id { get; set; }
        /// <summary>
        /// Mã xưng hô
        /// </summary>
        public string vocative_code { get; set; }
        /// <summary>
        /// Tên xưng hô
        /// </summary>
        public string vocative_name { get; set; }
        #endregion
    }
}
