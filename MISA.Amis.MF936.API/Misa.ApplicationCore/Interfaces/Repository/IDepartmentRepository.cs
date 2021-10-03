using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Repository
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Lọc và phân trang dữ liệu kho
        /// </summary>
        /// <param name="warehouseFilter">giá trị tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// author: NTDUNG(27/8/2021)
        /// modifiedBy: NTDUNG(27/8/2021)
        object GetDepartmentFilterPaging(string warehouseFilter, int pageIndex, int pageSize);
    }
}
