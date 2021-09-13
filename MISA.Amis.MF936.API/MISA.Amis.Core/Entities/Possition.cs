using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    public class Possition : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Id vị trí/ chức vụ
        /// </summary>
        public Guid PossitionId { get; set; }

        /// <summary>
        /// Tên vị trí/ chức vụ
        /// </summary>
        public string PossitionName { get; set; }

        /// <summary>
        /// Mã vị trí/ chức vụ
        /// </summary>
        public string PossitionCode { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
