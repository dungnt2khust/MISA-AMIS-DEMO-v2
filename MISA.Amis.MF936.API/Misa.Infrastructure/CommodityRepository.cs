using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Attributes;
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
    public class CommodityRepository : BaseRepository<Commodity>, ICommodityRepository
    {
        #region Constructor
        public CommodityRepository(IConfiguration configuration) : base(configuration)
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
        /// author: NTDUNG(27/8/2021)
        /// modifiedBy: NTDUNG(27/8/2021)
        public object GetCommodityFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var commodityFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@search_data", commodityFilter);
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);
                var sql = "select * from  public.func_get_commodity_paging_filter(@search_data) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_commodity_paging_filter(@search_data)) as filtertable;";

                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);
                
                // Dữ liệu hàng hoá
                var commodities = response.Read<Commodity>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                // Lấy đơn vị tính cho hàng hoá
 
                var parameters = new DynamicParameters();

                for (int i = 0; i < commodities.Count; i++)
                {
                    var id = commodities[i].commodity_id;
                    parameters.Add("@commodity_id", id);
                    var sqlCommand = $"select * from public.view_commodity_unit cu where cu.commodity_id = @commodity_id;";
                    var units = _dbConnection.Query<CommodityUnit>(sqlCommand, param: parameters, commandType: CommandType.Text);
                    commodities[i].units = (List<CommodityUnit>)units;
                }
               
                var result = new
                {
                    Commoditys = commodities,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
                return result;
            }

        }
        
        /// <summary>
        /// Thêm mới hàng hoá
        /// </summary>
        /// <param name="commodityData"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(04/10/2021)
        public int InsertCommodity(CommodityData commodityData)
        {
            NpgsqlConnection npgsqlConnection = null;
            IDbTransaction transaction = null;
            try
            {
                // Tạo kết nối, transaction
                npgsqlConnection = new NpgsqlConnection(_connectionString);
                npgsqlConnection.Open();
                transaction = npgsqlConnection.BeginTransaction();

                /// Lấy dữ liệu bảng master
                var commodity = (Commodity)commodityData.dataMaster;
                /// Đặt đơn vị tính chính
                var mainUnit = new Unit();
                mainUnit.unit_id = (Guid)commodity.unit_id;
                mainUnit.is_main_unit = 1;
                /// Danh sách đơn vị tính 
                var units = (List<Unit>)commodityData.dataDetail;
                units.Add(mainUnit);
                /// Id master
                commodity.commodity_id = Guid.NewGuid();
                /// Thêm bảng master
                DynamicParameters dynamicParametersMaster = new DynamicParameters();
                var properties = commodity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(commodity);
                    dynamicParametersMaster.Add($"@{propName}", propValue);

                }
                var proceduce = $"func_insert_commodity_master";
                var rowEffects = npgsqlConnection.Execute(proceduce, param: dynamicParametersMaster, commandType: CommandType.StoredProcedure);
                /// Thêm bảng detail
                DynamicParameters dynamicParametersDetail = new DynamicParameters();
               
                foreach (Unit unit in units)
                {
                    dynamicParametersDetail.Add("@commodity_id", commodity.commodity_id);
                    /// Id đơn vị tính
                    dynamicParametersDetail.Add("@unit_id", unit.unit_id);
                    /// Tỷ lệ chuyển đổi
                    if (unit.is_main_unit == 1)
                    {
                        unit.rate = 1;
                        unit.selected = 1;
                    }
                    dynamicParametersDetail.Add("@rate", 1/unit.rate);
                    /// Là đơn vị tính chính
                    dynamicParametersDetail.Add("@is_main_unit", unit.is_main_unit);
                    /// Được chọn
                    dynamicParametersDetail.Add("@selected", unit.selected);

                    var proceduceDetail = $"func_insert_commodity_detail";
                    var rowEffectsDetail = npgsqlConnection.Execute(proceduceDetail, param: dynamicParametersDetail, commandType: CommandType.StoredProcedure);
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    return -1;
                }
                throw; 
            }
            finally
            {
                if (npgsqlConnection != null) npgsqlConnection.Close();
            }

            return 1;
        }

        #endregion
    }
}
