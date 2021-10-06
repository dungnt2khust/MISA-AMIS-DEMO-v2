using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Interfaces.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region DECLARE
        protected IConfiguration _configuration;
        protected IDbConnection _dbConnection;
        protected string _connectionString = string.Empty;
        private string _className;

        #endregion

        #region CONSTRUCTOR
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MisaAmisConnection");
            _className = typeof(TEntity).Name.ToLower();
        }
        #endregion

        #region METHOD
        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public int Delete(Guid entityId)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM {_className} WHERE {_className}_id = @{_className}_id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_className}_id", entityId);
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }

        }

        /// <summary>
        /// Xóa nhiều 
        /// </summary>
        /// <param name="entityIds"> mảng chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(19/8/2021)
        /// ModifiedBy: NTDUNG(19/8/2021)
        public int DeleteMultiple(List<Guid> entityIds)
        {
            var parameters = new DynamicParameters();
            var paramName = new List<string>();

            for (int i = 0; i < entityIds.Count; i++)
            {
                var id = entityIds[i];
                // Đặt tên cho param
                paramName.Add($"@m_Id{i}");
                // Đặt giá trị cho param bằng id 
                parameters.Add($"@m_Id{i}", id);
            }
            // Join mảng để tạo ra câu truy vấn xoá nhiều
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM {_className} WHERE {_className}_id IN ({String.Join(", ", paramName.ToArray())})";
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        public IEnumerable<TEntity> GetAllEntities()
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} ORDER BY created_date DESC";
                var entities = _dbConnection.Query<TEntity>(sqlCommand);
                return entities;
               
            }

        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021) 
        public virtual TEntity GetEntityById(Guid entityId)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} WHERE {_className}_id = @{_className}_id";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_className}_id", entityId);
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: dynamicParameters);
                return entity;
            }
        }

        /// <summary>
        /// Lấy thông tin theo property
        /// </summary>
        /// <param name="propName">Tên property</param>
        /// <param name="propValue">Gía trị property</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(19/8/2021)
        /// ModifiedBy: NTDUNG(19/8/2021)
        public TEntity GetEntityByProperty(string propName, object propValue)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} WHERE {propName} = @{propName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{propName}", propValue);
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: dynamicParameters);
                return entity;
            }
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: NTDUNG(17/8/2021)
        /// ModifiedBy: NTDUNG(17/8/2021)
        public int Insert(TEntity entity)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(entity);
                    dynamicParameters.Add($"@{propName}", propValue);

                }
                var proceduce = $"func_insert_{_className}";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
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
        public int Update(TEntity entity, Guid entityId)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(entity);
                    var propId = $"{_className}_id";
                    if (propName == propId)
                    {
                        dynamicParameters.Add($"{propName}_update", entityId);
                    }
                    else
                    {
                        dynamicParameters.Add($"{propName}_update", propValue);
                    }
                }

                var proceduce = $"func_update_{_className}";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }
        
        /// <summary>
        /// Kiểm tra trùng lặp
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fieldName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool CheckDuplicate(TEntity entity, string fieldName, string mode)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                var sqlQuery = "";

                parameters.Add("@fieldValue", entity.GetType().GetProperty(fieldName).GetValue(entity, null));
                parameters.Add("@fieldName", fieldName);
                parameters.Add($"@{_className}_id", (Guid)(entity.GetType().GetProperty($"{_className}_id").GetValue(entity, null)));

                if (mode == "ADD")
                {
                    sqlQuery = $"SELECT * FROM {_className} WHERE @fieldName = @fieldValue";
                }
                else if (mode == "UPDATE")
                {
                    sqlQuery = $"SELECT * FROM {_className} WHERE @fieldName = @fieldValue AND {_className}_id != @{_className}_id";
                }

                // Lấy dữ liệu và phản hồi cho client
                var queryField = _dbConnection.Query<TEntity>(sqlQuery, param: parameters, commandType: CommandType.Text);

                return queryField.ToList().Count() >= 1;
            }
        }

        #endregion
    }
}
