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
    public class WarehouseService : BaseService<Warehouse>, IWarehouseService
    {
        #region Declare
        IWarehouseRepository _warehouseRepository;
        #endregion

        #region Constructor
        public WarehouseService(IBaseRepository<Warehouse> baseRepository, IWarehouseRepository warehouseRepository) : base(baseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult GetWarehouseFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _warehouseRepository.GetWarehouseFilterPaging(searchData, pageIndex, pageSize);
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
