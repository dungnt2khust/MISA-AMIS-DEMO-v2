using MISA.Amis.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    public class Employee : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired] 
        [MISAUnique]
        [MISADisplayName("Mã nhân viên")]
        [MISAExport("Mã nhân viên")]
        public string EmployeeCode { get; set; }

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
        /// Id Phòng ban
        /// </summary>
        [MISARequired]
        [MISADisplayName("Đơn vị")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [MISANotMap]
        [MISAExport("Đơn vị")]
        public string DepartmentName { get; set; }
 
        /// <summary>
        /// Tên vị trí / chức vụ
        /// </summary>
        [MISAExport("Chức danh")]
        public string PositionName { get; set; }
        
        /// <summary>
        /// Là khách hàng
        /// </summary> 
        //[MISAExport("Là khách hàng")]
        public int? IsCustomer { get; set; }
        /// <summary>
        /// Là nhà cung cấp
        /// </summary>
        //[MISAExport("Là nhà cung cấp")]
        public int? IsSupplier { get; set; }
        #endregion
    }
}
