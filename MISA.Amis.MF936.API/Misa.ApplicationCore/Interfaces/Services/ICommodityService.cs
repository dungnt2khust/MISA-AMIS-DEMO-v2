using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface ICommodityService : IBaseService<Commodity>
    {
        /// <summary>
        /// Lọc và phân trang hàng hóa
        /// </summary>
        /// <param name="searchData">chuỗi tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        ServiceResult GetCommodityFilterPaging(string searchData, int pageIndex, int pageSize);
        
        /// <summary>
        /// Thêm mới hàng hoá
        /// </summary>
        /// <param name="commodityData"></param>
        /// <returns></returns>
        ServiceResult InsertCommodity(CommodityData commodityData);
    }
}
