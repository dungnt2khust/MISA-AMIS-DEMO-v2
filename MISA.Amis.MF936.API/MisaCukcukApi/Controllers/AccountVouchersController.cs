using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using Misa.API.Properties;
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



    }
}
