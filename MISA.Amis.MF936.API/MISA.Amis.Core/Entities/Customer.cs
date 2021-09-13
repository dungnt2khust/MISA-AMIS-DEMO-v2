using MISA.Amis.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    public class Customer : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Id khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MISARequired]
        [MISAUnique]
        [MISADisplayName("Mã khách hàng")]
        [MISAExport("Mã khách hàng")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và tên đệm
        /// </summary>
        [MISANotMap]
        public string FirstName { get; set; }


        /// <summary>
        /// Tên
        /// </summary>
        [MISANotMap]
        public string LastName { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary> 
        [MISAExport("Họ và tên")]
        [MISADisplayName("Họ và tên")]
        [MISARequired]
        public string FullName { get; set; }


        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [MISAExport("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary> 
        [MISADisplayName("Số điện thoại")]
        [MISAExport("Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary> 
        [MISAExport("Số cố định")]
        [MISADisplayName("Số cố định")]
        public string LandlineNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary> 
        [MISAExport("Email")]
        public string Email { get; set; }


        /// <summary>
        /// Địa chỉ
        /// </summary> 
        [MISAExport("Địa chỉ")]
        public string Address { get; set; }

        /// <summary>
        /// Số cmnd/căn cước
        /// </summary>
        [MISAExport("Số CMND")]
        [MISADisplayName("Số CMND")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary> 
        [MISAExport("Tài khoản ngân hàng")]
        [MISADisplayName("Tài khoản ngân hàng")]
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MISAExport("Tên ngân hàng")]
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [MISAExport("Chi nhánh")]
        public string BankBranch { get; set; }

        /// <summary>
        /// Id nhóm khách hàng 
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [MISANotMap]
        [MISAExport("Tên nhóm khách hàng")]
        public string CustomerGroupName { get; set; }
        #endregion
    }
}
