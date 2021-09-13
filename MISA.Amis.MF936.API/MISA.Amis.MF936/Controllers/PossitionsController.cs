using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.MF936.Controllers
{
    [Route("api/v1/possitions")]
    [ApiController]
    public class PossitionsController : BaseController<Possition>
    {
        #region Constructors
        public PossitionsController(IPossitionService possitionService) : base(possitionService)
        {

        }
        #endregion

    }
}
