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
    public class CommodityUnitRepository : BaseRepository<CommodityUnit>, ICommodityUnitRepository
    {
        public CommodityUnitRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Cập nhật đơn vị tính được chọn
        /// </summary>
        /// <param name="commodityId"></param>
        /// <param name="selectedUnitId"></param>
        /// <returns></returns>
        /// CreatedBy: NTDUNG(01/10/2021)
        public int changeSelectedUnit(Guid commodityId, Guid selectedUnitId)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@commodity_id", commodityId);  
                dynamicParameters.Add("@selected_unit_id", selectedUnitId);
                var proceduce = $"func_update_selectedunit";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy danh sách đơn vị tính của hàng hoá
        /// </summary>
        /// <param name="commodityId"></param>
        /// <returns></returns>
        public List<Unit> GetUnits(Guid commodityId)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@commodity_id", commodityId);
                var sql = $"select * from public.view_commodity_unit cu where cu.commodity_id = @commodity_id;";
                var units  = _dbConnection.Query<Unit>(sql, param: dynamicParameters, commandType: CommandType.Text);
                return units.ToList();
            }
        }
    }
}
