using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Services;
using MISA.Amis.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.MF936.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : BaseController<Customer>
    {
        #region Fields

        private readonly ICustomerService _customerService;

        #endregion

        #region Constructors

        public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        #endregion

        #region Lấy mã khách hàng mới 

        /// <summary>
        /// Lấy mã khách hàng mới
        /// </summary>
        /// <returns> Mã code trả về và dữ liệu hoặc mã lỗi của request</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        [HttpGet("NewcustomerCode")]
        public IActionResult GetNewcustomerCode()
        {
            try
            {
                _serviceResult = _customerService.GetNewCode();

                return Ok(_serviceResult);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = ResourcesVN.MISA_Exception_Error_Msg,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        #endregion

        #region Phân trang và lọc dữ liệu khách hàng
        /// <summary>
        /// Lọc và phân trang theo tiêu chí tìm kiếm, theo phòng ban, vị trí
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterString"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns> Mã code trả về và dữ liệu hoặc mã lỗi của request</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        [HttpGet("filter")]
        public IActionResult GetCustomerFilter(int pageSize, int pageNumber,
                                                string filterString)
        {
            try
            {
                _serviceResult = _customerService.GetByFilter(pageSize, pageNumber, filterString);

                if (!_serviceResult.IsValid)
                {
                    _serviceResult.Msg = ResourcesVN.MISA_Exception_Error_Msg;
                    return Ok(_serviceResult);
                }

                // Trả dữ liệu về cho client
                return StatusCode(200, _serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = ResourcesVN.MISA_Exception_Error_Msg,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        #endregion

        #region Export dữ liệu khách hàng
        /// <summary>
        /// Export dữ liệu khách hàng ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        [HttpGet("export")]
        public IActionResult Export()
        {
            try
            {
                var stream = _customerService.Export();
                //string excelname = $"userlist-{DateTime.Now.ToString("yyyymmddhhmmssfff")}.xlsx";
                string excelname = "Danh_sach_nhan_vien.xlsx";
                //return file(stream, "application/octet-stre excelname);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelname);
            }
            catch (Exception ex)
            {

                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourcesVN.MISA_Exception_Error_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, response);
            }
        }

        #endregion
    }
}
