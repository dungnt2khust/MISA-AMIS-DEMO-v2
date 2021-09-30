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
    public class AccountVoucherDetailService : BaseService<AccountVoucherDetail>, IAccountVoucherDetailService
    {
        #region Declare
        IAccountVoucherDetailRepository _accountVoucherDetailRepository;
        #endregion
        #region Constructor
        public AccountVoucherDetailService(IBaseRepository<AccountVoucherDetail> baseRepository, IAccountVoucherDetailRepository AccountVoucherDetailRepository) : base(baseRepository)
        {
            _accountVoucherDetailRepository = AccountVoucherDetailRepository;
        }
        #endregion
    }
}
