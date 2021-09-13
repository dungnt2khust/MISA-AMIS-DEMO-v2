using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Repositoties;
using MISA.Amis.Core.Responses;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastucture.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Contructor 
        public EmployeeRepository(IConfiguration _configuration) : base(_configuration)
        {
        }
        #endregion

        #region Phân trang và lọc dữ liệu nhân viên

        /// <summary>
        /// Lọc dữ liệu theo chuỗi tìm kiếm hoặc nhóm nv, kết hợp phân trang
        /// </summary>
        /// <param name="pageSize">         số bản ghi 1 trang</param>
        /// <param name="pageNumber">       chỉ số trang</param>
        /// <param name="filterString">     chuỗi tìm kiếm</param>
        /// <returns> Kết quả lọc dữ liệu</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        public FilterResponse GetByFilter(int pageSize, int pageNumber, string filterString)
        {
            if (filterString == null)
            {
                filterString = "NV";
            } 

            var storeFilter = "Proc_GetFilterEmployees";
            var storeCount = "Proc_GetCountEmployees";
            var parameters = new DynamicParameters();

            parameters.Add("@m_PageSize", pageSize);
            parameters.Add("@m_PageStart", (pageNumber - 1) * pageSize);
            parameters.Add("@m_FilterString", filterString);


            // Thực hiện truy vấn lấy dữ liệu
            var employees = _dbConnection.Query<object>(storeFilter, commandType: CommandType.StoredProcedure, param: parameters);

            if (employees == null)
            {
                return new FilterResponse
                {
                    TotalRecord = 0,
                    TotalPage = 0,
                    Data = null
                };
            }
            
            var totalRecord = _dbConnection.QueryFirstOrDefault<int>(storeCount, commandType: CommandType.StoredProcedure, param: parameters);
            var totalPage = (int)(totalRecord / pageSize) + ((totalRecord % pageSize != 0) ? 1 : 0);

            return new FilterResponse
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = (List<object>)employees
            };
        }
        #endregion

        #region Lấy mã nhân viên mới
        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns> Mã nhân viên mới</returns>
        /// CreatedBy: NTDUNG (27/08/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        public string GetNewCode()
        {
            string storeName = "Proc_GetNewEmployeeCode";
            var newCode = _dbConnection.QueryFirstOrDefault<string>(storeName, commandType: CommandType.StoredProcedure);

            return newCode;
        }

        #endregion

        #region Export dữ liệu nhân viên
        /// <summary>
        /// Export thông tin nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG (01/09/2021)
        /// ModifiedBy: NTDUNG (01/09/2021)
        public IEnumerable<Employee> ExportEmployees()
        {
            var proceduce = "Proc_ExportEmployees";
            var employees = _dbConnection.Query<Employee>(proceduce, commandType: CommandType.StoredProcedure);
            return employees;
        }

        #endregion
    }
}
