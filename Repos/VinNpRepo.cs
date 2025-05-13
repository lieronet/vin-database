using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public class VinNpRepo : IVinNpRepo
    {
        private readonly string _connectionString;

        public VinNpRepo(IOptions<Configuration> appConfig)
        {
            _connectionString = appConfig.Value.ConnectionString;
        }

        public async Task<IEnumerable<VinQueue>> GetQueuedRecords(Guid batch)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"
SELECT DISTINCT  Vin, DealerId, ModifiedDate
FROM    VinQueue
WHERE   InUseBy = @batch
";
                return await conn.QueryAsync<VinQueue>(sql, new { batch });
            }
        }

        public async Task<VinDetailModel> GetVinRecord(string vin)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM VinRecords WHERE Vin = @vin";

                return (await conn.QueryAsync<VinDetailModel>(sql, new { vin })).FirstOrDefault();
            }
        }

        public async Task<IEnumerable<VinRecordDataModel>> SearchVinRecords(int pageIndex, int pageSize, DateTime? modifiedAfter, int? dealerId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var sql = @"
SELECT  Vin, DealerId, ModifiedDate
FROM    VinRecords
WHERE   (@modifiedAfter IS NULL OR ModifiedDate > @modifiedAfter)
AND     (@dealerId IS NULL OR DealerId = @dealerId)
ORDER BY Id
OFFSET @pageIndex * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
";

                return await conn.QueryAsync<VinRecordDataModel>(sql, new { pageIndex, pageSize, modifiedAfter, dealerId });
            }
        }
    }
}
