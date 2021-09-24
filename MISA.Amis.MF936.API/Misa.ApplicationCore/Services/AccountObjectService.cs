using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    class AccountObjectService:BaseService<AccountObject>, IAccountObjectService
    {
        #region Constructor
        public AccountObjectService(IBaseRepository<AccountObject> baseRepository) : base(baseRepository)
        {
           
        }
        #endregion
    }
}
