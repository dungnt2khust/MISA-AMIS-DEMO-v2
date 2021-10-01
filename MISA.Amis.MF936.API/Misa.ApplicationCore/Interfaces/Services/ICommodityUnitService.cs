using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface ICommodityUnitService : IBaseService<CommodityUnit>
    {
        /// <summary>
        /// Đổi đơn vị tính của hàng hoá
        /// </summary>
        /// <param name="commodityId"></param>
        /// <param name="selectedUnitId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        ServiceResult changeSelectedUnit(Guid commodityId, Guid selectedUnitId);

        /// <summary>
        /// Lấy các đơn vị tính của hàng hoá
        /// </summary>
        /// <param name="commodityId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        ServiceResult GetUnits(Guid commodityId);
    }
}
