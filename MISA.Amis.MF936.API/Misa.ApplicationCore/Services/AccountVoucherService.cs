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
        IAccountVoucherRepository _accounVoucherRepository;
        IAccountVoucherDetailRepository _accountVoucherDetailRepository;
        IAccountObjectRepository _accountObjectRepository;
        ICommodityUnitRepository _commodityUnitRepository;
        //readonly IBaseRepository<AccountVoucher> _baseRepository;
        #endregion

        #region Constructor
        public AccountVoucherService(IBaseRepository<AccountVoucher> baseRepository, IAccountVoucherRepository accounVoucherRepository, IAccountVoucherDetailRepository accountVoucherDetailRepository, IAccountObjectRepository accountObjectRepository, ICommodityUnitRepository commodityUnitRepository) : base(baseRepository)
        {
            _accounVoucherRepository = accounVoucherRepository;
            _baseRepository = baseRepository;
            _accountVoucherDetailRepository = accountVoucherDetailRepository;
            _accountObjectRepository = accountObjectRepository;
            _commodityUnitRepository = commodityUnitRepository;
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
                var accountVoucher = (AccountVoucher)data.GetType().GetProperty("in_inward").GetValue(data, null);
                var accountObject = new AccountObject();
                accountObject.accountobject_id = (Guid)accountVoucher.accountobject_id;
                accountObject.employee_id = accountVoucher.employee_id;
                // Thêm vào bảng đối tượng
                _accountObjectRepository.Update(accountObject, accountObject.accountobject_id);
                // Thêm vào bảng chính
                accountVoucher.accountvoucher_id = Guid.NewGuid();
                _baseRepository.Insert(accountVoucher);
                // Thêm vào hàng tiền
                var accountVoucherDetails = (List<AccountVoucherDetail>)data.GetType().GetProperty("in_inward_detail").GetValue(data, null);
                var accountVoucherDetailIds = new List<Guid>();
                for (int i = 0; i < accountVoucherDetails.Count(); i++)
                {
                    var accountVoucherDetail = accountVoucherDetails[i];
                    // Bind id của master
                    accountVoucherDetail.accountvoucher_id = accountVoucher.accountvoucher_id;
                    // Tạo id mới cho detail
                    var newDetailId = Guid.NewGuid();
                    accountVoucherDetail.accountvoucherdetail_id = newDetailId;
                    accountVoucherDetailIds.Add(newDetailId);
                    // Tạo mới detail
                    _accountVoucherDetailRepository.Insert(accountVoucherDetail);
                        
                }
                serviceResult.Data = new
                {
                    newMasterId = accountVoucher.accountobject_id,
                    newDetailIds = accountVoucherDetailIds
                };
                //serviceResult.Data = _accounVoucherRepository.updateAccountVoucher(accountVoucherID,data);

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
                var accountObject = new AccountObject();
                accountObject.accountobject_id = (Guid)accountVoucher.accountobject_id;
                accountObject.employee_id = accountVoucher.employee_id;
                // Thêm vào bảng đối tượng
                _accountObjectRepository.Update(accountObject, accountObject.accountobject_id);
                // Thêm vào bảng chính
                _baseRepository.Update(accountVoucher, accountVoucherID);
                // Thêm vào hàng tiền
                var accountVoucherDetails = (List<AccountVoucherDetail>)data.GetType().GetProperty("in_inward_detail").GetValue(data, null);
                for (int i = 0; i < accountVoucherDetails.Count(); i++)
                {
                    var accountVoucherDetail = accountVoucherDetails[i];
                    // Sửa đơn vị tính dược chọn
                    var state = (int)accountVoucherDetail.GetType().GetProperty("state").GetValue(accountVoucherDetail, null);

                    switch (state) {
                        case (int)AccountVoucherDetailState.Add:
                            _accountVoucherDetailRepository.Insert(accountVoucherDetail);
                            break;
                        case (int)AccountVoucherDetailState.Update: 
                            _accountVoucherDetailRepository.Update(accountVoucherDetail, accountVoucherDetail.accountvoucherdetail_id);
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

        /// <summary>
        /// Lấy mã chứng từ mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(30/09/2021)
        public ServiceResult getNewVoucherCode()
        {
            try
            {
                var serviceResult = new ServiceResult();
                var voucher = _accounVoucherRepository.getNewVoucherCode();
                var currentVoucherCode = voucher.voucher_code;
                var numberString = Regex.Match(currentVoucherCode, @"\d+").Value;
                int numberCode = Int32.Parse(numberString);
                numberCode = numberCode + 1;
                numberString = numberCode.ToString();
                var numberStringLength = numberString.Length;
                var newVoucherCode = "BK";
                if (numberStringLength < 6)
                {
                    for (int i = 0; i < 6 - numberStringLength; ++i)
                    {
                        newVoucherCode = newVoucherCode + "0";
                    }
                }
                newVoucherCode = newVoucherCode + numberString;
                serviceResult.Data = newVoucherCode;
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
