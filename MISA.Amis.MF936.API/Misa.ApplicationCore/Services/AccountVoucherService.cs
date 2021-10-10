using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Enum;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    public class AccountVoucherService : BaseService<AccountVoucher>, IAccountVoucherService
    {
        #region Declare
        IAccountVoucherRepository _accountVoucherRepository;

        IBaseService<AccountVoucher> _accountVoucherService;
        IBaseService<AccountObject> _accountObjectService;
        IBaseService<AccountVoucherDetail> _accountVoucherDetailService;
        #endregion

        #region Constructor
        public AccountVoucherService(IBaseRepository<AccountVoucher> baseRepository, IAccountVoucherRepository accountVoucherRepository,
            IBaseService<AccountVoucher> accountVoucherService, IBaseService<AccountObject> accountObjectService, IBaseService<AccountVoucherDetail> accountVoucherDetailService) : base(baseRepository)
        
        {
            _baseRepository = baseRepository;
            _accountVoucherRepository = accountVoucherRepository;

            _accountVoucherService = accountVoucherService;
            _accountObjectService = accountObjectService;
            _accountVoucherDetailService = accountVoucherDetailService;
        }

        public ServiceResult getAccountVoucherPagingFilter(string searchData, int? mentionState, string voucherType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accountVoucherRepository.getAccountVoucherPagingFilter(searchData, mentionState, voucherType, startDate, endDate, pageIndex, pageSize);
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
                var rowEffects = _accountVoucherRepository.mentionMany(entityIds);
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
                var rowEffects = _accountVoucherRepository.unMentionMany(entityIds);
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
            serviceResult.Data = _accountVoucherRepository.getAccountVoucherDetail(entityId);
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
                serviceResult.Data = _accountVoucherRepository.getAccountVoucherDetail(accountVoucherID);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Thêm mới phiếu nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(30/09/2021)
        public ServiceResult addAccountVoucher(AccountVoucherData data)
        {
            try
            {
                // AccountVoucher
                var accountVoucher = (AccountVoucher)data.GetType().GetProperty("in_inward").GetValue(data, null);
 
                // AccountVoucherDetails
                var accountVoucherDetails = (List<AccountVoucherDetail>)data.GetType().GetProperty("in_inward_detail").GetValue(data, null);
                // Validate 
                var serviceResult = _accountVoucherService.ValidateData(accountVoucher, "ADD");

                if (!serviceResult.IsValid)
                {
                    return serviceResult;
                }

                //serviceResult = _accountObjectService.ValidateData(accountObject, "UPDATE");
                //if (!serviceResult.IsValid)
                //{
                //    return serviceResult;
                //}

                foreach (AccountVoucherDetail accountVoucherDetail in accountVoucherDetails)
                {
                    serviceResult = _accountVoucherDetailService.ValidateData(accountVoucherDetail, "ADD");
                    if (!serviceResult.IsValid)
                    {
                        return serviceResult;
                    }
                }

                serviceResult.IsValid = true;
                serviceResult.Data = _accountVoucherRepository.addAccountVoucher(accountVoucher, accountVoucherDetails); 
                serviceResult.Msg = Resources.ResourceVN.Success_Insert;

                return serviceResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Chỉnh sửa phiếu nhập
        /// </summary>
        /// <param name="accountVoucherID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(30/09/2021)
        public ServiceResult updateAccountVoucher(Guid accountVoucherID, AccountVoucherData data)
        {
            try
            {
                var accountVoucher = (AccountVoucher)data.GetType().GetProperty("in_inward").GetValue(data, null);
                
                var accountVoucherDetails = (List<AccountVoucherDetail>)data.GetType().GetProperty("in_inward_detail").GetValue(data, null);

                // Validate data
                var serviceResult = _accountVoucherService.ValidateData(accountVoucher, "UPDATE");

                if (!serviceResult.IsValid)
                {
                    return serviceResult;
                }

                //serviceResult = _accountObjectService.ValidateData(accountObject, "UPDATE");
                //if (!serviceResult.IsValid)
                //{
                //    return serviceResult;
                //}

                foreach (AccountVoucherDetail accountVoucherDetail in accountVoucherDetails)
                {
                    serviceResult = _accountVoucherDetailService.ValidateData(accountVoucherDetail, "ADD");
                    if (!serviceResult.IsValid)
                    {
                        return serviceResult;
                    }
                }


                serviceResult.IsValid = true;
                serviceResult.Data = _accountVoucherRepository.updateAccountVoucher(accountVoucher, accountVoucherDetails);
                serviceResult.Msg = Resources.ResourceVN.Success_Update;

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
