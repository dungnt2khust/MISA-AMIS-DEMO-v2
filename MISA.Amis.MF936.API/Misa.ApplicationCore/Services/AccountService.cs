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
    public class AccountService : BaseService<Account>, IAccountService
    {
        #region Declare
        IAccountRepository _accountRepository;
        #endregion
        #region Constructor
        public AccountService(IBaseRepository<Account> baseRepository, IAccountRepository accountRepository) : base(baseRepository)
        {
            _accountRepository = accountRepository;
        }
        #endregion
        public ServiceResult getAccountPagingFilter(string searchData, int pageIndex, int pageSize, int type)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accountRepository.getAccountPagingFilter(searchData, pageIndex, pageSize, type);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
