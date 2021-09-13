using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Repositoties;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class PossitionService : BaseService<Possition>, IPossitionService
    {
        #region Constructors

        public PossitionService(IPossitionRepository possitionRepository) : base(possitionRepository)
        {
        }

        #endregion

    }
}
