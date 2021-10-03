﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class AccountObjectGroupRepository : BaseRepository<AccountObjectGroup>, IAccountObjectGroupRepository
    {
        #region Constructor
        public AccountObjectGroupRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Method

        /// <summary>
        /// Lọc và phân trang dữ liệu kho
        /// </summary>
        /// <param name="searchData">giá trị tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// author: NTDUNG(27/8/2021)
        /// modifiedBy: NTDUNG(27/8/2021)
        public object GetAccountObjectGroupFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var employeeFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@search_data", employeeFilter);
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);
                var sql = "select * from  public.func_get_accountobjectgroup_paging_filter(@search_data) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_accountobjectgroup_paging_filter(@search_data)) as filtertable;";

                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var AccountObjectGroups = response.Read<AccountObjectGroup>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    AccountObjectGroups = AccountObjectGroups,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
                return result;
            }
        }
        #endregion
    }
}
