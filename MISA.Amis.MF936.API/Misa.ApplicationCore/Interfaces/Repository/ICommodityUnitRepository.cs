using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Repository
{
    public interface ICommodityUnitRepository : IBaseRepository<CommodityUnit>
    {
        /// <summary>
        /// Cập nhật đơn vị tính đang được chọn của hàng hoá
        /// </summary>
        /// <param name="commodityId"></param>
        /// <param name="selectedUnitId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        int changeSelectedUnit(Guid commodityId, Guid selectedUnitId);

        /// <summary>
        /// Lấy đơn vị tính của hàng hoá
        /// </summary>
        /// <param name="commodityId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        List<Unit> GetUnits(Guid commodityId);
    }
}
