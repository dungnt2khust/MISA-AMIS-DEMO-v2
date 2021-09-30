using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface IAccountObjectService: IBaseService<AccountObject>
    {
        /// <summary>
        /// lọc và phân trang đối tượng theo mã và tên đối tượng
        /// </summary>
        /// <param name="searchData">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(24/9/2021)
        ServiceResult getAccountObjectPagingFilter(string searchData, int pageIndex, int pageSize);
    }
}
