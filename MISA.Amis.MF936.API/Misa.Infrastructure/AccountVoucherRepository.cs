using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Enum;
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
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);

                var sql = "select * from  public.func_get_voucher_paging_filter(@search_data, @mention_state,@type_voucher, @start_date, @end_date) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_voucher_paging_filter(@search_data, @mention_state, @type_voucher,  @start_date, @end_date)) as filtertable;";
                sql += "select coalesce(sum(filtertable.total_price),0) as total_prices from (select * from  public.func_get_voucher_paging_filter(@search_data, @mention_state, @type_voucher,  @start_date, @end_date)) as filtertable;";
                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var vouchers = response.Read<AccountVoucher>();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPrices = response.Read<decimal>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    Vouchers = vouchers,
                    TotalPrices = totalPrices,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,

                };
                return result;
            }
        }

        /// <summary>
        /// Lấy chi tiết chứng từ theo Id
        /// </summary>
        /// <param name="accountVoucherID">ID chứng từ</param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(24/9/2021)
        public object getAccountVoucherDetail(Guid accountVoucherID)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("@accountvoucher_id", accountVoucherID);
                var sql = "select * from public.view_accountvoucher_accountobject av where av.accountvoucher_id = @accountvoucher_id;";
                sql += "select * from public.view_accountvoucherdetail_commodity ac where ac.accountvoucher_id = @accountvoucher_id;";

                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);

                var vouchers = response.Read<AccountVoucher>();
                var voucherDetails = response.Read<AccountVoucherDetail>();
                var inwardDetail = new List<object>();

                if (voucherDetails.Count() > 0)
                {
                    foreach (var item in voucherDetails)
                    {
                        var commodityId = item.commodity_id;
                        DynamicParameters dynamicParameters1 = new DynamicParameters();

                        dynamicParameters1.Add("@commodity_id", commodityId);
                        var sql2 = "select * from view_commodity_unit vcu2 where vcu2.commodity_id = @commodity_id";
                        var units = _dbConnection.Query<CommodityUnit>(sql2, param: dynamicParameters1, commandType: CommandType.Text);

                        item.units = (List<CommodityUnit>)units;
                        inwardDetail.Add(new
                        {
                            voucher_detail = item
                        });
                    }
                }

                var result = new
                {
                    Data = new
                    {
                        in_inward = vouchers,
                        in_inward_detail = inwardDetail,
                    }

                };
                return result;
            }
        }

        /// <summary>
        /// Ghi sổ nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(28/09/2021)
        public int mentionMany(List<Guid> entityIds)
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
            // Join mảng để tạo ra câu truy vấn ghi sổ nhiều
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"UPDATE accountvoucher SET is_mention = 1 WHERE accountvoucher_id IN ({String.Join(", ", paramName.ToArray())})";
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }
        }

        /// <summary>
        /// Bỏ ghi nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(28/09/2021)
        public int unMentionMany(List<Guid> entityIds)
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
            // Join mảng để tạo ra câu truy vấn ghi sổ nhiều
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = $"UPDATE accountvoucher SET is_mention = 0 WHERE accountvoucher_id IN ({String.Join(", ", paramName.ToArray())})";
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }
        }

        /// <summary>
        /// Thêm mới phiếu nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(30/09/2021)
        public object addAccountVoucher(AccountVoucher accountVoucher, List<AccountVoucherDetail> accountVoucherDetails)
        {
            // Khai báo 
            NpgsqlConnection npgsqlConnection = null;
            IDbTransaction transaction = null;
            Guid masterId = Guid.NewGuid();
            List<Guid> detailIds = new List<Guid>();
            try
            {
                // 1. Tạo kết nối, transaction
                npgsqlConnection = new NpgsqlConnection(_connectionString);
                npgsqlConnection.Open();
                transaction = npgsqlConnection.BeginTransaction();

                // 2. Id mới cho master 
                accountVoucher.accountvoucher_id = Guid.NewGuid();
                masterId = accountVoucher.accountvoucher_id;
                // 3. Thêm mới vào master
                DynamicParameters dynamicParametersAccountVoucher = new DynamicParameters();
                var propertiesAccountVoucher = accountVoucher.GetType().GetProperties();
                foreach (var property in propertiesAccountVoucher)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(accountVoucher);
                    dynamicParametersAccountVoucher.Add($"@{propName}", propValue);

                }
                var proceduceAccountVoucher = $"func_insert_accountvoucher";
                var rowEffectsAccountVoucher = npgsqlConnection.Execute(proceduceAccountVoucher, param: dynamicParametersAccountVoucher, commandType: CommandType.StoredProcedure);

                // 4. Thêm mới detail
                addDetail(accountVoucherDetails, npgsqlConnection, masterId, detailIds);
                // 5. Commit
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    return new
                    {
                    };
                }
                throw;
            }
            finally
            {
                if (npgsqlConnection != null) npgsqlConnection.Close();
            }
            return new
            {
                newMasterId = masterId,
                newDetailIds = detailIds
            };
        }
        /// <summary>
        /// Thêm mới detail
        /// </summary>
        /// <param name="accountVoucherDetails"></param>
        /// <param name="npgsqlConnection"></param>
        /// <param name="masterId"></param>
        /// <param name="detailIds"></param>
        /// CreatedBy: NTDUNG (10/10/2021)
        private static void addDetail(List<AccountVoucherDetail> accountVoucherDetails, NpgsqlConnection npgsqlConnection, Guid masterId, List<Guid> detailIds)
        {
            DynamicParameters dynamicParametersAccountVoucherDetail = new DynamicParameters();
            foreach (AccountVoucherDetail accountVoucherDetail in accountVoucherDetails)
            {
                var propertiesAccountVoucherDetail = accountVoucherDetail.GetType().GetProperties();
                // Tạo id detail
                accountVoucherDetail.accountvoucherdetail_id = Guid.NewGuid();
                accountVoucherDetail.accountvoucher_id = masterId;
                detailIds.Add(accountVoucherDetail.accountvoucherdetail_id);

                foreach (var property in propertiesAccountVoucherDetail)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(accountVoucherDetail);
                    dynamicParametersAccountVoucherDetail.Add($"@{propName}", propValue);

                }
                var proceduceAccountVoucherDetail = $"func_insert_accountvoucherdetail";
                var rowEffectsAccountVoucherDetail = npgsqlConnection.Execute(proceduceAccountVoucherDetail, param: dynamicParametersAccountVoucherDetail,
                    commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Chỉnh sửa phiếu nhập
        /// </summary>
        /// <param name="accountVoucherID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(30/09/2021)
        public object updateAccountVoucher(AccountVoucher accountVoucher, List<AccountVoucherDetail> accountVoucherDetails)
        {
            // Khai báo
            NpgsqlConnection npgsqlConnection = null;
            IDbTransaction transaction = null;
            Guid masterId = Guid.NewGuid();
            List<Guid> detailIds = new List<Guid>();
            try
            {
                // 1. Tạo kết nối, transaction
                npgsqlConnection = new NpgsqlConnection(_connectionString);
                npgsqlConnection.Open();
                transaction = npgsqlConnection.BeginTransaction();

                masterId = accountVoucher.accountvoucher_id;
                // 2. Cập nhật vào master
                DynamicParameters dynamicParametersAccountVoucher = new DynamicParameters();
                var propertiesAccountVoucher = accountVoucher.GetType().GetProperties();
                foreach (var property in propertiesAccountVoucher)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(accountVoucher);
                    dynamicParametersAccountVoucher.Add($"@{propName}_update", propValue);

                }
                var proceduceAccountVoucher = $"func_update_accountvoucher";
                var rowEffectsAccountVoucher = npgsqlConnection.Execute(proceduceAccountVoucher, param: dynamicParametersAccountVoucher, commandType: CommandType.StoredProcedure);


                // 3. Cập nhật detail
                UpdateDetail(accountVoucher, accountVoucherDetails, npgsqlConnection, detailIds);
                // 4. Commit
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    return new
                    {
                    };
                }
                throw;
            }
            finally
            {
                if (npgsqlConnection != null) npgsqlConnection.Close();
            }
            return new {
                newMasterId = masterId,
                newDetailIds = detailIds
            };
        }

        /// <summary>
        /// Cập nhật detail
        /// </summary>
        /// <param name="accountVoucher"></param>
        /// <param name="accountVoucherDetails"></param>
        /// <param name="npgsqlConnection"></param>
        /// <param name="detailIds"></param>
        /// CreatedBy: NTDUNG(10/10/2021)
        private static void UpdateDetail(AccountVoucher accountVoucher, List<AccountVoucherDetail> accountVoucherDetails, NpgsqlConnection npgsqlConnection, List<Guid> detailIds)
        {
            foreach (AccountVoucherDetail accountVoucherDetail in accountVoucherDetails)
            {
                var propertiesAccountVoucherDetail = accountVoucherDetail.GetType().GetProperties();
                var state = (int)accountVoucherDetail.GetType().GetProperty("state").GetValue(accountVoucherDetail, null);

                // Gán id master
                accountVoucherDetail.accountvoucher_id = accountVoucher.accountvoucher_id;

                switch (state)
                {
                    case (int)AccountVoucherDetailState.Add:
                        DynamicParameters insertParams = new DynamicParameters();
                        // Tạo id detail thêm mới
                        accountVoucherDetail.accountvoucherdetail_id = Guid.NewGuid();
                        detailIds.Add(accountVoucherDetail.accountvoucherdetail_id);

                        foreach (var property in propertiesAccountVoucherDetail)
                        {
                            if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                            var propName = property.Name;
                            var propValue = property.GetValue(accountVoucherDetail);
                            insertParams.Add($"@{propName}", propValue);

                        }
                        var proceduceInsert = $"func_insert_accountvoucherdetail";
                        var rowEffectsInsert = npgsqlConnection.Execute(proceduceInsert, param: insertParams,
                            commandType: CommandType.StoredProcedure);
                        break;
                    case (int)AccountVoucherDetailState.Update:
                        DynamicParameters updateParams = new DynamicParameters();
                        detailIds.Add(accountVoucherDetail.accountvoucherdetail_id);
                        // Chỉnh sửa
                        foreach (var property in propertiesAccountVoucherDetail)
                        {
                            if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                            var propName = property.Name;
                            var propValue = property.GetValue(accountVoucherDetail);
                            updateParams.Add($"@{propName}_update", propValue);

                        }
                        var procedureUpdate = $"func_update_accountvoucherdetail";
                        var rowEffectsUpdate = npgsqlConnection.Execute(procedureUpdate, param: updateParams,
                            commandType: CommandType.StoredProcedure);
                        break;
                    case (int)AccountVoucherDetailState.Delete:
                        // Xoá 
                        DynamicParameters deleteParams = new DynamicParameters();
                        deleteParams.Add("@accountvoucherdetail_id_delete", accountVoucherDetail.accountvoucherdetail_id);

                        var procedureDelete = $"func_delete_accountvoucherdetail";
                        var rowEffectsDelete = npgsqlConnection.Execute(procedureDelete, param: deleteParams,
                            commandType: CommandType.StoredProcedure);
                        break;
                }
            }
        }
    }
}
