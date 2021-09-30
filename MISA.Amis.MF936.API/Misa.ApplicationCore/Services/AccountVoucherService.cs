using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Enum;
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
        IAccountVoucherDetailRepository _accountVoucherDetailRepository;
        //readonly IBaseRepository<AccountVoucher> _baseRepository;
        #endregion

        #region Constructor
        public AccountVoucherService(IBaseRepository<AccountVoucher> baseRepository, IAccountVoucherRepository accounVoucherRepository, IAccountVoucherDetailRepository accountVoucherDetailRepository) : base(baseRepository)
        {
            _accounVoucherRepository = accounVoucherRepository;
            _baseRepository = baseRepository;
            _accountVoucherDetailRepository = accountVoucherDetailRepository;
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
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accounVoucherRepository.addAccountVoucher(data);
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
                var serviceResult = new ServiceResult();
                var accountVoucher = (AccountVoucher)data.GetType().GetProperty("in_inward").GetValue(data, null);
                // Thêm vào bảng chính
                _baseRepository.Update(accountVoucher, accountVoucherID);
                // Thêm vào hàng tiền
                var accountVoucherDetails = (List<AccountVoucherDetail>)data.GetType().GetProperty("in_inward_detail").GetValue(data, null);
                for (int i = 0; i < accountVoucherDetails.Count(); i++)
                {
                    var accountVoucherDetail = accountVoucherDetails[i];
                    var state = (int)accountVoucherDetail.GetType().GetProperty("state").GetValue(accountVoucherDetail, null);

                    switch (state) {
                        case (int)AccountVoucherDetailState.Add:
                            _accountVoucherDetailRepository.Insert(accountVoucherDetail);
                            break;
                        case (int)AccountVoucherDetailState.Update: 
                            _accountVoucherDetailRepository.Update(accountVoucherDetail, accountVoucherID);
                            break;
                        case (int)AccountVoucherDetailState.Delete:
                            _accountVoucherDetailRepository.Delete(accountVoucherDetail.accountvoucherdetail_id);
                            break;
                    }
                }
                serviceResult.Data = accountVoucherDetails.Count();
                //serviceResult.Data = _accounVoucherRepository.updateAccountVoucher(accountVoucherID,data);

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
