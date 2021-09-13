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
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Constructors

        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
        }

        #endregion

    }
}
