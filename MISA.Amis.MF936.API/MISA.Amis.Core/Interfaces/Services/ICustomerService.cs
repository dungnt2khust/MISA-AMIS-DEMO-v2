using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        #region Phân trang và lọc dữ liệu khách hàng
        /// <summary>
        /// Lấy và lọc data
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterString"></param>
        /// <returns> Kết quả nghiệp vụ phân trang và lọc dữ liệu khách hàng</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        ServiceResult GetByFilter(int pageSize, int pageNumber, string filterString);
        #endregion

        #region Lấy mã khách hàng mới
        /// <summary>
        /// Lấy mã nv mới
        /// </summary>
        /// <returns> Kết quả nghiệp vụ lấy mã khách hàng mới</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        ServiceResult GetNewCode();
        #endregion

        #region Export dữ liệu khách hàng
        /// <summary>
        /// Export dữ liệu khách hàng ra file excel
        /// </summary>
        /// <returns> Memory Stream</returns>
        /// CreatedBy: NTDUNG (01/09/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        MemoryStream Export();
        #endregion
    }
}
