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
    public class AccountVoucherService : BaseService<AccountVoucher>, IAccountVoucherService
    {
        #region Declare
        IAccountVoucherRepository _accounVoucherRepository;
        #endregion

        #region Constructor
        public AccountVoucherService(IBaseRepository<AccountVoucher> baseRepository, IAccountVoucherRepository accounVoucherRepository) : base(baseRepository)
        {
            _accounVoucherRepository = accounVoucherRepository;
        }

        public ServiceResult getAccountVoucherPagingFilter(string searchData, int? mentionState, string voucherType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accounVoucherRepository.getAccountVoucherPagingFilter(searchData, mentionState, voucherType, startDate, endDate, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        /// <summary>
        /// Ghi sổ nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(28/09/2021)
        public ServiceResult mentionMany(List<Guid> entityIds)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var rowEffects = _accounVoucherRepository.mentionMany(entityIds);
                serviceResult.Data = new
                {
                    rowEffects = rowEffects,
                    messages = Resources.ResourceVN.Success_Delete,
                };
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    
        /// <summary>
        /// Bỏ ghi nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(28/09/2021)
        public ServiceResult unMentionMany(List<Guid> entityIds)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var rowEffects = _accounVoucherRepository.unMentionMany(entityIds);
                serviceResult.Data = new
                {
                    rowEffects = rowEffects,
                    messages = Resources.ResourceVN.Success_Delete,
                };
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Ghi đề phương thức lấy thông tin phiếu nhập chi tiết
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(28/09/2021)
        public override ServiceResult GetEntityById(Guid entityId)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _accounVoucherRepository.getAccountVoucherDetail(entityId);
            return serviceResult;
        }

        /// <summary>
        /// Lấy chi tiết chứng từ theo Id
        /// </summary>
        /// <param name="accountVoucherID">ID chứng từ</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(24/9/2021)
        public ServiceResult getAccountVoucherDetail(Guid accountVoucherID)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accounVoucherRepository.getAccountVoucherDetail(accountVoucherID);
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
