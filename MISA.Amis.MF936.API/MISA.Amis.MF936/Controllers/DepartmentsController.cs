using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.MF936.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        #region Constructors
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {

        }
        #endregion
    }
}
