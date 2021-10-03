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
    public class CommodityGroupsController : BaseEntityController<CommodityGroup>
    {
        #region Declare
        ICommodityGroupService _commodityGroupService;
        #endregion
        #region Constructor
        public CommodityGroupsController(IBaseService<CommodityGroup> baseService, ICommodityGroupService commodityGroupService) : base(baseService)
        {
            _commodityGroupService = commodityGroupService;
        }
        #endregion

        /// <summary>
        /// Lọc và phân trang kho
        /// </summary>
        /// <param name="searchData">Dữ liệu lọc</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(20/8/2021)
        /// ModifiedBy: NTDUNG(20/8/2021)
        [HttpGet("Filter")]
        public IActionResult GetCommodityGroupFilterPaging([FromQuery] string searchData, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var serviceResult = _commodityGroupService.GetCommodityGroupFilterPaging(searchData, pageIndex, pageSize);
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
