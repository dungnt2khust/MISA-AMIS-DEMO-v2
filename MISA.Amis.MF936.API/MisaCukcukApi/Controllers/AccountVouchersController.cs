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
    
        [HttpPost("add")]
        public IActionResult AddAccountVoucher([FromBody] AccountVoucherData data)
        {
            try
            {
                //Trả về kết quả cho client
                var serviceResult = _accountVoucherService.addAccountVoucher(data);
                if (serviceResult.IsValid)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult.Data);
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

        [HttpPut("update/{accountVoucherID}")]
        public IActionResult UpdateAccountVoucher(Guid accountVoucherID, [FromBody] AccountVoucherData data )
        {
            try
            {
                //Trả về kết quả cho client
                var serviceResult = _accountVoucherService.updateAccountVoucher(accountVoucherID, data);
                if (serviceResult.IsValid)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult.Data);
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

        [HttpGet("NewVoucherCode")]
        public IActionResult getNewVoucherCode()
        {
            try
            {
                var serviceResult = _accountVoucherService.getNewVoucherCode();
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

    }
}
