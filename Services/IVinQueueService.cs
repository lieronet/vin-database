using vin_db.Domain;

namespace vin_db.Services
{
    public interface IVinQueueService
    {
        Task<IEnumerable<VinQueue>> GetQueue(Guid batch, int takeSize);
        Task InsertProcessedRecords(IEnumerable<VinDetail> records);
        Task<IEnumerable<VinDetail>> ProcessRecords(IEnumerable<VinQueue> records);
        Task RollbackFailedBatch(Guid batch);
        Task ClearReservedQueueRecords(Guid batch);
    }
}
