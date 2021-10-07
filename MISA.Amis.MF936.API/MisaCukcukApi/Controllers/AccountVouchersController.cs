using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.API.Properties;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.API.Controllers
{
    public class AccountVouchersController : BaseEntityController<AccountVoucher>
    {
        #region Declare
        IAccountVoucherService _accountVoucherService;
        #endregion

        #region Constructor
        public AccountVouchersController(IBaseService<AccountVoucher> baseService, IAccountVoucherService accountVoucherService) : base(baseService)
        {
            _accountVoucherService = accountVoucherService;
        }
        #endregion

        /// <summary>
        /// Lấy thông tin phân trang và lọc dữ liệu phiếu nhập
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="mentionState"></param>
        /// <param name="voucherType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpGet("Filter")]
        public IActionResult GetAccountVoucherPagingFilter([FromQuery] string searchData, [FromQuery] int? mentionState, [FromQuery] string voucherType, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var serviceResult = _accountVoucherService.getAccountVoucherPagingFilter(searchData, mentionState, voucherType, startDate, endDate, pageIndex, pageSize);
                return Ok(serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Ghi sổ hàng loạt
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns> 
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpPut("mention")]
        public IActionResult MentionMany([FromBody] List<Guid> entityIds)
        {
            try
            {
                var serviceResult = _accountVoucherService.mentionMany(entityIds);
                //4.Trả về kết quả cho client
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }
       
        /// <summary>
        /// Bỏ ghi hàng loạt
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns> 
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpPut("unmention")]
        public IActionResult UnMentionMany([FromBody] List<Guid> entityIds)
        {
            try
            {
                var serviceResult = _accountVoucherService.unMentionMany(entityIds);
                //4.Trả về kết quả cho client
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết phiếu nhập
        /// </summary>
        /// <param name="accountVoucherID"></param>
        /// <returns></returns> 
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpGet("Detail/{accountVoucherID}")]
        public IActionResult getAccountVoucherDetail(Guid accountVoucherID)
        {
            try
            {
                var serviceResult = _accountVoucherService.getAccountVoucherDetail(accountVoucherID);
                return Ok(serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }
   
        /// <summary>
        /// Thêm mới phiếu nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns> 
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpPost("add")]
        public IActionResult AddAccountVoucher([FromBody] AccountVoucherData data)
        {
            try
            {
                //Trả về kết quả cho client
                var serviceResult = _accountVoucherService.addAccountVoucher(data);
                if (serviceResult.IsValid)
                {
                    return StatusCode(201, serviceResult);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Chỉnh sửa phiếu nhập
        /// </summary>
        /// <param name="accountVoucherID"></param>
        /// <param name="data"></param>
        /// <returns></returns> 
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpPut("update/{accountVoucherID}")]
        public IActionResult UpdateAccountVoucher(Guid accountVoucherID, [FromBody] AccountVoucherData data)
        {
            try
            {
                //Trả về kết quả cho client
                var serviceResult = _accountVoucherService.updateAccountVoucher(accountVoucherID, data);
                if (serviceResult.IsValid)
                {
                    return StatusCode(201, serviceResult);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        } 
    }
}
