using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public class VinRepo : IVinRepo
    {
        private readonly VinDbContext _context;
        public VinRepo(VinDbContext context)
        {
            _context = context;
        }

        public async Task CleanAndReserveQueue(int takeSize, Guid batch)
        {
            //remove already processed VINs
            //this assumes a vin will only need to be processed once
            _context.Database.ExecuteSql(@$"
DELETE q
FROM VinQueue q 
LEFT JOIN VinDetail d ON q.Vin = d.Vin
WHERE d.Vin IS NOT NULL");

            //if by some accident a vin has been marked for processing for too long, delete it
            var oldRecords = _context.VinQueue
                .Where(x => x.InUseDate < DateTime.UtcNow.AddMinutes(-60));

            _context.VinQueue.RemoveRange(oldRecords);

            await _context.VinQueue
                .Where(x => x.InUseBy == null)
                .Take(takeSize)
                .ForEachAsync(x =>
                {
                    x.InUseBy = batch;
                    x.InUseDate = DateTime.UtcNow;
                });

            await _context.SaveChangesAsync();
        }

        public async Task ClearReservedQueueRecords(Guid batch)
        {
            var queueRecordsToRemove = _context.VinQueue.Where(v => v.InUseBy == batch);
            _context.VinQueue.RemoveRange(queueRecordsToRemove);

            await _context.SaveChangesAsync();
        }

        public async Task InsertProcessedRecords(IEnumerable<VinDetail> vinRecords)
        {
            foreach(var record in vinRecords)
            {
                _context.VinDetails.Add(record);
            }

            await _context.SaveChangesAsync(); 
        }

        public async Task InsertVinList(IEnumerable<VinRecordDataModel> vinRecords)
        {
            _context.VinQueue.AddRange(vinRecords.Select(v => new VinQueue(v)));

            await _context.SaveChangesAsync();
        }

        public async Task RollbackFailedBatch(Guid batch)
        {
            await _context.VinQueue
                .Where(x => x.InUseBy == batch)
                .ForEachAsync(x => { x.InUseBy = null; x.InUseDate = null; });

            await _context.SaveChangesAsync();
        }
    }
}
