using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Repository
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        /// <summary>
        /// lọc và phân trang đối tượng
        /// </summary>
        /// <param name="searchData">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <param name="type">Kiểu</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(24/9/2021)
        object getAccountPagingFilter(string searchData, int pageIndex, int pageSize, int type);
    }
}
