using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Lọc và phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="employeeFilter">giá trị tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// author: NTDUNG(27/8/2021)
        /// modifiedBy: NTDUNG(27/8/2021)
        object GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize);

        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        string GetNewEmployeeCode();

        /// <summary>
        /// Export thông tin nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        IEnumerable<Employee> ExportEmployee();

        /// <summary>
        /// Kiểm tra mã nhân viên có trùng hay không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        int CheckEmployeeCodeExist(string employeeCode);

    }
}
