using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{

    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region DECLARE
        protected IBaseRepository<TEntity> _baseRepository;
        string _className;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _className = typeof(TEntity).Name;
        }

        #endregion

        #region Method
        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult Delete(Guid entityId)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var rowEffects = _baseRepository.Delete(entityId);
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
        /// Xóa nhiều
        /// </summary>
        /// <param name="entityIds"> mảng chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult DeleteMultiple(List<Guid> entityIds)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var rowEffects = _baseRepository.DeleteMultiple(entityIds);
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
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult GetAllEntities()
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _baseRepository.GetAllEntities();
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public virtual ServiceResult GetEntityById(Guid entityId)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _baseRepository.GetEntityById(entityId);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public ServiceResult Insert(TEntity entity)
        {
            try
            {
                //Validate dữ liệu
                //Validate chung
                var serviceResult = ValidateData(entity, "ADD");
                if (!serviceResult.IsValid)
                {
                    return serviceResult;
                }
               //Thêm dữ liệu
                var rowEffects = _baseRepository.Insert(entity);
                serviceResult.Data = new
                {
                    rowEffects = rowEffects,
                    messages = Resources.ResourceVN.Success_Insert,
                };
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        public ServiceResult Update(TEntity entity, Guid entityId)
        {
            try
            {
                //Validate dữ liệu
                //Validate chung
                var serviceResult = ValidateData(entity, "UPDATE");
                if (!serviceResult.IsValid)
                {
                    return serviceResult;
                }
                //Thêm dữ liệu
                var rowEffects = _baseRepository.Update(entity, entityId);
                serviceResult.Data = new
                {
                    rowEffects = rowEffects,
                    messages = Resources.ResourceVN.Success_Update,
                };
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region Private Method
        /// <summary>
        /// Validate data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(07/10/2021)
        public ServiceResult ValidateData(TEntity entity, string mode)
        {
            var serviceResult = new ServiceResult();
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(entity);
                var propMisaDislayName = property.GetCustomAttributes(typeof(MisaDisplayName), true);

                // Trường bắt buộc
                if (property.IsDefined(typeof(MisaRequired), false))
                {
                    var fieldName = propMisaDislayName.Length > 0 ? (propMisaDislayName[0] as MisaDisplayName).FieldName : property.Name;
                    if (propValue == null || propValue.ToString() == "")
                    {
                        serviceResult.IsValid = false;
                        serviceResult.Msg = string.Format(Resources.ResourceVN.Exception_Required, fieldName);
                        return serviceResult;
                    }
                };
                // Kiểm tra trùng lặp
                if (property.IsDefined(typeof(MisaUnique), false))
                {
                    var fieldName = propMisaDislayName.Length > 0 ? (propMisaDislayName[0] as MisaDisplayName).FieldName : property.Name;

                    var duplicate = _baseRepository.CheckDuplicate(entity, propName, mode);
                    if (duplicate)
                    {
                        serviceResult.IsValid = false;
                        serviceResult.Msg = string.Format(Resources.ResourceVN.Exception_Duplication, fieldName);
                        return serviceResult;
                    }
                };
            }
            serviceResult.IsValid = true;
            return serviceResult;
        }

        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(07/10/2021)
        public ServiceResult GetNewCode()
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _baseRepository.GetNewCode();
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
