using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Employee:BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid employee_id { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Mã nhân viên")]
        [MisaUnique]
        [MisaExport("Mã nhân viên")]
        public string employee_code { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Tên nhân viên")]
        [MisaExport("Tên nhân viên")]
        public string employee_name { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public gender gender { get; set; }

        /// <summary>
        /// Thông tin giới tính
        /// </summary>
        [MisaNotMap]
        [MisaExport("Giới tính")]
        public string gender_name
        {
            get
            {
                switch (gender)
                {
                    case gender.Male:
                        return "Nam";
                    case gender.Female:
                        return "Nữ";
                    case gender.Other:
                        return "Khác";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [MisaExport("Ngày sinh")]
        public DateTime? date_of_birth { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Đơn vị")]
        public Guid department_id { get; set; }

        /// <summary>
        /// Căn cước công dân
        /// </summary>
        public string identity_number { get; set; }

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        public DateTime? identity_date { get; set; }


        /// <summary>
        /// Chức danh
        /// </summary>
        [MisaExport("Chức danh")]
        public string position_name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string phone_number { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string landline_number { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string identity_place { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }


        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// [MisaExport]
        [MisaExport("Tên đơn vị")]
        [MisaNotMap]
        public string department_name { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [MisaNotMap]
        public string  department_code { get; set; }

        #endregion
    }
}
