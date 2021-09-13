using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Repositoties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastucture.Repository
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        #region Contructor 
        public CustomerGroupRepository(IConfiguration _configuration) : base(_configuration)
        {
        }
        #endregion
    }
}
