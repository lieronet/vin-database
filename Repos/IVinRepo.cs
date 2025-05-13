using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public interface IVinRepo
    {
        Task InsertVinList(IEnumerable<VinRecordDataModel> vinRecords);
        Task InsertProcessedRecords(IEnumerable<VinDetail> vinRecords);
        Task CleanAndReserveQueue(int takeSize, Guid batch);
        Task ClearReservedQueueRecords(Guid batch);
        Task RollbackFailedBatch(Guid batch);
    }
}
