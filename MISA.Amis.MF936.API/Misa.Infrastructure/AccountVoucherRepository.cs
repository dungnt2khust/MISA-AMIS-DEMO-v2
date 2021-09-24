using Dapper;
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
    public class AccountVoucherRepository : BaseRepository<AccountVoucher>, IAccountVoucherRepository
    {
        #region Constructor
        public AccountVoucherRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        /// <summary>
        /// lọc và phân trang chứng từ
        /// </summary>
        /// <param name="searchData">Từ khóa tìm kiếm</param>
        /// <param name="mentionState">Trạng thái ghi sổ</param>
        /// <param name="voucherType">loại chứng từ</param>
        /// <param name="startDate">ngày lọc đầu</param>
        /// <param name="endDate">ngày lọc cuối</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(24/9/2021)
        public object getAccountVoucherPagingFilter(string searchData, int? mentionState, string voucherType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var voucherFilter = searchData == null ? string.Empty : searchData;
                
                var typeVoucher = voucherType == null ? string.Empty : voucherType;
                dynamicParameters.Add("@search_data", voucherFilter);
                dynamicParameters.Add("@mention_state", mentionState);
                dynamicParameters.Add("@type_voucher", typeVoucher);
                dynamicParameters.Add("@start_date", startDate);
                dynamicParameters.Add("@end_date", endDate);
                dynamicParameters.Add("@page_index", pageIndex);
                dynamicParameters.Add("@page_size", pageSize);

                var sql = "select * from  public.func_get_voucher_paging_filter(@search_data, @mention_state,@type_voucher, @start_date, @end_date,  @page_index, @page_size);";
                sql += "select count(*) from (select * from  public.func_get_voucher_paging_filter(@search_data, @mention_state, @type_voucher,  @start_date, @end_date, @page_index, @page_size)) as filtertable;";
                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var vouchers = response.Read<AccountVoucher>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    Vouchers = vouchers,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
                return result;
            }
        }
        

    }
}
