using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration):base(configuration)
        {
        }
        #endregion

        #region Method

        /// <summary>
        /// Lọc và phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="employeeFilter">giá trị tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// createdBy: NTDUNG(27/8/2021)
        /// modifiedBy: NTDUNG(27/8/2021)
        public object GetEmployeeFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var employeeFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@employeefilter", employeeFilter);
                dynamicParameters.Add("@pageindex", pageIndex - 1);
                dynamicParameters.Add("@pagesize", pageSize);
                var sql = "create temp table filtertable as (select * from public.view_employee_department e where e.employee_code ILIKE concat('%', @employeefilter, '%') order by e.created_date desc);";
                sql += "select * from filtertable  ft limit @pagesize offset @pageindex;";
                sql += "select count(*) from filtertable as a; drop table filtertable";

                var response = _dbConnection.QueryMultiple(sql, param:dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var employees = response.Read<Employee>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    Employees = employees,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
                return result;
            }
        }

        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public string GetNewEmployeeCode()
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var proceduce = "func_get_new_employee_code";

                var newEmployeeCode = _dbConnection.Query<String>(proceduce, commandType: CommandType.StoredProcedure);
                var newCode = newEmployeeCode.ToList()[0];
                return newCode;

            }
        }

        /// <summary>
        /// Export thông tin nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public IEnumerable<Employee> ExportEmployee()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var proceduce = "Proc_ExportEmployee";
                var employees = _dbConnection.Query<Employee>(proceduce, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }

        /// <summary>
        /// Kiểm tra mã nhân viên có tồn tại không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        // CreatedBy: NTDUNG(27/8/2021)
        /// ModifiedBy: NTDUNG(27/8/2021)
        public int CheckEmployeeCodeExist(string employeeCode)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = "SELECT EXISTS(SELECT * from employee WHERE EmployeeCode= @EmployeeCode);";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeCode", employeeCode);
                var result = _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters);
                return result;
            }
        }
        #endregion

    }
}
