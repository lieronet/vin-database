using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public interface IVinNpRepo
    {
        public Task<VinDetailModel> GetVinRecord(string vin);
        public Task<IEnumerable<VinRecordDataModel>> SearchVinRecords(int pageIndex, int pageSize, DateTime? modifiedAfter, int? dealerId);
        public Task<IEnumerable<VinQueue>> GetQueuedRecords(Guid batch);
    }
}
