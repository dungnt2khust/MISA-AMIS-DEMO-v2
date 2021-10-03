using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    public class VocativeService : BaseService<Vocative>, IVocativeService
    {
        #region Declare
        IVocativeRepository _vocativeRepository;
        #endregion

        #region Constructor
        public VocativeService(IBaseRepository<Vocative> baseRepository, IVocativeRepository vocativeRepository) : base(baseRepository)
        {
            _vocativeRepository = vocativeRepository;
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
        public ServiceResult GetVocativeFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _vocativeRepository.GetVocativeFilterPaging(searchData, pageIndex, pageSize);
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
