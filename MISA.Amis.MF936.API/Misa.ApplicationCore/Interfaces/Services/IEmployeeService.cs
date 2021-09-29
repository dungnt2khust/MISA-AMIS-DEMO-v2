using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// Lọc và phân trang nhân viên
        /// </summary>
        /// <param name="searchData">chuỗi tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        ServiceResult GetEmployeeFilterPaging(string searchData, int pageIndex, int pageSize);

        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        ServiceResult GetNewEmployeeCode();

        /// <summary>
        /// Export dữ liệu nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        MemoryStream Export();

        /// <summary>
        /// Kiểm tra mã nhân viên có trùng không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        ServiceResult CheckEmployeeCodeExist(string employeeCode);
        
        
       
    }
}
