using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Attributes;
using MISA.Amis.Core.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastucture.Repository
{
    public class BaseRepository<MISAEntity>
    {
        #region Fields        

        protected readonly string _connectionString;

        protected readonly IDbConnection _dbConnection;

        protected readonly string _entityName;

        protected IConfiguration configuration;

        #endregion

        #region Constructors

        public BaseRepository(IConfiguration _configuration)
        {
            configuration = _configuration;

            // Lấy thông tin truy cập db
            _connectionString = this.configuration.GetConnectionString("MISA.Amis");

            // Khởi tạo đối tượng kết nối db
            _dbConnection = new MySqlConnection(_connectionString);

            _entityName = typeof(MISAEntity).Name;
        }

        #endregion

        #region Các phương thức GET

        /// <summary>
        /// Lấy tất cả data
        /// </summary>
        /// <returns>Trả về danh sách dữ liệu</returns>
        /// CreatedBy: NTDUNG (27/08/2020)
        public virtual List<MISAEntity> Get()
        {
            // Lấy dữ liệu
            var storeName = $"Proc_Get{_entityName}s";
            return (List<MISAEntity>)_dbConnection.Query<MISAEntity>(storeName, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Dữ liệu của một entity</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        public virtual MISAEntity GetById(Guid entityId)
        {
            var storeName = $"Proc_Get{_entityName}ById";

            var parameters = new DynamicParameters();
            parameters.Add("@m_id", entityId);

            // Lấy dữ liệu và phản hồi cho client
            return _dbConnection.QueryFirstOrDefault<MISAEntity>(storeName, commandType: CommandType.StoredProcedure,param: parameters);
        }

        #endregion

        #region Thêm mới

        /// <summary>
        /// Thêm mới khách hagnf
        /// </summary>
        /// <param name="entity"> Data thêm mới</param>
        /// <returns>Số dòng bị ảnh hưởng trong database</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        public virtual int Add(MISAEntity entity)
        {
            var columnsName = new List<string>();
            var columnsParam = new List<string>();
            var parameters = new DynamicParameters();

            // Tạo id mới
            //entity.entityId = Guid.NewGuid();

            // Đọc từng property của object
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propMISANotMap = prop.GetCustomAttributes(typeof(MISANotMap), true);
                if (propMISANotMap.Length == 0)
                {
                    // Tên thuộc tính
                    var propName = prop.Name;

                    // Giá tri thuộc tính
                    var propValue = prop.GetValue(entity);
                    if (propName.Equals($"{_entityName}Id") && prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }

                    columnsName.Add(propName);
                    columnsParam.Add($"@{propName}");
                    parameters.Add($"@{propName}", propValue);
                }

            }

            var sqlQuery = $"INSERT INTO {_entityName} ({String.Join(", ", columnsName.ToArray())}) " +
                            $"VALUES({String.Join(", ", columnsParam.ToArray())})";

            return _dbConnection.Execute(sqlQuery, param: parameters);
        }

        #endregion

        #region Cập nhật

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="entity">Data</param>
        /// <param name="entityId">Id</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        public virtual int Update(MISAEntity entity, Guid entityId)
        {
            var queryLine = new List<string>();
            var parameters = new DynamicParameters();

            // Đọc từng property của object
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propMISANotMap = prop.GetCustomAttributes(typeof(MISANotMap), true);
                if (propMISANotMap.Length == 0)
                {
                    // Tên thuộc tính
                    var propName = prop.Name;

                    // Giá tri thuộc tính
                    var propValue = prop.GetValue(entity);
                    if (propName.Equals($"{_entityName}Id"))
                    {
                        propValue = entityId;
                    }

                    queryLine.Add($"{propName} = @{propName}");
                    parameters.Add($"@{propName}", propValue);
                }
            }

            parameters.Add("@oldEntityId", entityId);
            var sqlQuery = $"UPDATE {_entityName} SET {String.Join(", ", queryLine.ToArray())} " +
                            $"WHERE {_entityName}Id = @oldEntityId";

            return _dbConnection.Execute(sqlQuery, param: parameters);
        }
        #endregion

        #region Xóa

        /// <summary>
        /// Xóa nhiều khách hàng
        /// </summary>
        /// <param name="entityIds">List id của các entity cần xóa</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        public virtual int DeleteMany(List<Guid> entityIds)
        {
            var parameters = new DynamicParameters();
            var paramName = new List<string>();

            for (int i = 0; i < entityIds.Count; i++)
            {
                var id = entityIds[i];
                // Đặt tên cho param
                paramName.Add($"@m_Id{i}");
                // Đặt giá trị cho param bằng id 
                parameters.Add($"@m_Id{i}", id.ToString());
            }
               
            // Join mảng để tạo ra câu truy vấn xoá nhiều
            var sql = $"Delete from {_entityName} where {_entityName}Id In ({String.Join(", ", paramName.ToArray())})";

            return _dbConnection.Execute(sql, param: parameters);
        }


        /// <summary>
        /// Xóa một KH với id tương ứng
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns> Số dòng bị ảnh hương khi truy vấn</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        public virtual int DeleteOne(Guid entityId)
        {
            var sqlQuery = $"DELETE FROM {_entityName} WHERE {_entityName}Id = @entityId";
            var parameters = new DynamicParameters();

            parameters.Add("@entityId", entityId);

            return _dbConnection.Execute(sqlQuery, param: parameters);
        }

        #endregion

        #region Check trùng
        /// <summary>
        /// Kiểm tra trùng giá trị 
        /// </summary>
        /// <param name="entity"> Thông tin của entity</param>
        /// <param name="fieldName"> Trường thông tin cần kiểm tra</param>
        /// <param name="mode"> Các kiểu kiểm tra khách nhau (ADD, UPDATE)</param>
        /// <returns> Boolean: true - không trùng, false - trùng</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        /// ModifiedBy: NTDUNG (27/08/2021)
        public bool CheckDuplicate(MISAEntity entity, string fieldName, string mode)
        {

            // Query check trùng
            var parameters = new DynamicParameters();
            var sqlQuery = "";

            if (mode == "ADD")
            {
                sqlQuery = $"SELECT * FROM {_entityName} WHERE {fieldName} = @fieldValue";
                parameters.Add("@fieldValue", entity.GetType().GetProperty(fieldName).GetValue(entity, null));
            }
            else if (mode == "UPDATE")
            {
                sqlQuery = $"SELECT * FROM {_entityName} WHERE {fieldName} = @fieldValue AND {_entityName}Id != @entityId";
                parameters.Add("@fieldValue", entity.GetType().GetProperty(fieldName).GetValue(entity, null));
                parameters.Add("@entityId", entity.GetType().GetProperty($"{_entityName}Id").GetValue(entity, null));
            }

            // Lấy dữ liệu và phản hồi cho client
            var queryField = _dbConnection.Query<MISAEntity>(sqlQuery, param: parameters);
            return queryField.Count() < 1;
        }
        #endregion
    }
}
