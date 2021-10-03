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
    public class AccountObjectGroupService : BaseService<AccountObjectGroup>, IAccountObjectGroupService
    {
        #region Declare
        IAccountObjectGroupRepository _accountObjectGroupRepository;
        #endregion

        #region Constructor
        public AccountObjectGroupService(IBaseRepository<AccountObjectGroup> baseRepository, IAccountObjectGroupRepository accountObjectGroupRepository) : base(baseRepository)
        {
            _accountObjectGroupRepository = accountObjectGroupRepository;
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
        public ServiceResult GetAccountObjectGroupFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accountObjectGroupRepository.GetAccountObjectGroupFilterPaging(searchData, pageIndex, pageSize);
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
