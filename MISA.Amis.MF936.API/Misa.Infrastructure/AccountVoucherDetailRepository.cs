using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class AccountVoucherDetailRepository : BaseRepository<AccountVoucherDetail>, IAccountVoucherDetailRepository
    {
        #region Constructor
        public AccountVoucherDetailRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
