
using Microsoft.Extensions.Options;
using vin_db.Domain;
using vin_db.Models;
using vin_db.Repos;

namespace vin_db.Services
{
    public class VinQueueService : IVinQueueService
    {
        private readonly ILogger<VinQueueService> logger;
        private readonly IVinRepo vinRepo;
        private readonly IVinNpRepo vinNpRepo;
        private readonly INhtsaRepository nhtsaRepository;

        public VinQueueService(ILogger<VinQueueService> logger, IVinRepo vinRepo, IVinNpRepo vinNpRepo, INhtsaRepository nhtsaRepository)
        {
            //I give up
            this.logger = logger;
            this.vinRepo = vinRepo;
            this.vinNpRepo = vinNpRepo;
            this.nhtsaRepository = nhtsaRepository;
        }

        public async Task ClearReservedQueueRecords(Guid batch)
        {
            await vinRepo.ClearReservedQueueRecords(batch);
        }

        public async Task<IEnumerable<VinQueue>> GetQueue(Guid batch, int takeSize)
        {
            await vinRepo.CleanAndReserveQueue(takeSize, batch);

            return await vinNpRepo.GetQueuedRecords(batch);
        }

        public async Task InsertProcessedRecords(IEnumerable<VinDetail> records)
        {
            await vinRepo.InsertProcessedRecords(records);
        }

        public async Task<IEnumerable<VinDetail>> ProcessRecords(IEnumerable<VinQueue> records)
        {
            var results = new List<VinDetail>();

            var dict = records.ToDictionary(x=> x.Vin, x => x);

            var page = 0;

            while (records.Count() > page * 50)
            {
                var takeSize = records.Count() - (page * 50);
                if (takeSize > 50) takeSize = 50;

                var vins = records.Skip(page * 50)
                    .Take(takeSize)
                    .Select(r => r.Vin);

                var data = await nhtsaRepository.GetVinData(vins);

                //do some validation on the top-level results

                results.AddRange(data.Results.Select(x => new VinDetail(x, dict[x.VIN])));

                page++;
            }

            return results;
        }

        public async Task RollbackFailedBatch(Guid batch)
        {
            await vinRepo.RollbackFailedBatch(batch);
        }
    }
}
