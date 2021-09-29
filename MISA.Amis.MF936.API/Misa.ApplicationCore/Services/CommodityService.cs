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
    public class CommodityService : BaseService<Commodity>, ICommodityService
    {
        #region Declare
        ICommodityRepository _commodityRepository;
        #endregion

        #region Constructor
        public CommodityService(IBaseRepository<Commodity> baseRepository, ICommodityRepository commodityRepository) : base(baseRepository)
        {
            _commodityRepository = commodityRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search_data"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult GetCommodityFilterPaging(string search_data, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _commodityRepository.GetCommodityFilterPaging(search_data, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
