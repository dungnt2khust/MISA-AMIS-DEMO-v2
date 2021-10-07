﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.API.Properties;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {

        #region Declare
        IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseEntityController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        [HttpGet]
        public IActionResult GetAllEntities()
        {
            try
            {
                var serviceResult = _baseService.GetAllEntities();
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
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        [HttpGet("{entityId}")]
        public IActionResult GetEntityById(Guid entityId)
        {
            try
            {
                var serviceResult = _baseService.GetEntityById(entityId);
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                return StatusCode(204);
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
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Dữ liệu được thêm</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        [HttpPost]
        public IActionResult Insert(TEntity entity)
        {
            try
            {
                //Trả về kết quả cho client
                var serviceResult = _baseService.Insert(entity);
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
        /// Chỉnh sửa theo Id
        /// </summary>
        /// <param name="entityId">Id</param>
        /// <param name="entity">Thông tin muốn thay đổi</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        [HttpPut("{entityId}")]
        public IActionResult Update(Guid entityId, TEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Update(entity, entityId);
                if (serviceResult.IsValid)
                {

                    return Ok(serviceResult);
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
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var serviceResult = _baseService.Delete(entityId);
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
        /// Xóa nhiều
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        [HttpDelete]
        public IActionResult DeleteMultiple([FromBody] List<Guid> entityIds)
        {
            try
            {
                var serviceResult = _baseService.DeleteMultiple(entityIds);
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
        /// Lấy mã mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(07/10/2021)
        [HttpGet("NewCode")] 
        public IActionResult GetNewCode()
        {
            try
            {
                var serviceResult = _baseService.GetNewCode();
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
        #endregion
    }
}
