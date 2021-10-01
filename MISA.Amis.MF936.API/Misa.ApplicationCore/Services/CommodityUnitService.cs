using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    public class CommodityUnitService : BaseService<CommodityUnit>, ICommodityUnitService
    {
        #region Declare
        ICommodityUnitRepository _commodityUnitRepository;
        #endregion

        #region Constructor
        public CommodityUnitService(IBaseRepository<CommodityUnit> baseRepository, ICommodityUnitRepository commodityUnitRepository) : base(baseRepository)
        {
            _commodityUnitRepository = commodityUnitRepository;
        }
        #endregion
        
        /// <summary>
        /// Thay đổi đơn vị tính được chọn
        /// </summary>
        /// <param name="commodityId"></param>
        /// <param name="selectedUnitId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        public ServiceResult changeSelectedUnit(Guid commodityId, Guid selectedUnitId)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _commodityUnitRepository.changeSelectedUnit(commodityId, selectedUnitId);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách đơn vị tính
        /// </summary>
        /// <param name="commodityId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        public ServiceResult GetUnits(Guid commodityId)
        {
            try
                {
                    var serviceResult = new ServiceResult();
                    serviceResult.Data = _commodityUnitRepository.GetUnits(commodityId);
                    return serviceResult;
                }
                catch (Exception)
                {

                    throw;
                }
        }
    }
}
