using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.Repositoties
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        #region Phân trang và lọc dữ liệu khách hàng
        /// <summary>
        /// Phân trang khách hàng và lọc dữ liệu
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterString"></param>
        /// <returns> Trả về kết quả của filter</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        /// ModifiedBy: NTUDNG (01/09/2021)
        FilterResponse GetByFilter(int pageSize, int pageNumber, string filterString);

        #endregion

        #region Lấy mã khách hàng mới
        /// <summary>
        /// Lấy mã khách hàng mới
        /// </summary>
        /// <returns> Trả về chuỗi là mã khách hàng mới</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        string GetNewCode();

        #endregion

        #region Export dữ liệu khách hàng
        /// <summary>
        /// Export thông tin khách hàng ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG (01/09/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        IEnumerable<Employee> ExportCustomers();
        #endregion
    }
}
