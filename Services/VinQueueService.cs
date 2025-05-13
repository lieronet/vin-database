
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

            var apiResults = new List<DecodedVin>();

            while (records.Any())
            {
                var data = await nhtsaRepository.GetVinData
                    (records.Select(r => r.Vin)
                    .Take(records.Count() > 50 ? 50 : records.Count()).ToList());

                apiResults.AddRange(data);            

                data.RemoveRange(0, data.Count > 50 ? 50 : data.Count());
            }

            return results;
        }

        public async Task RollbackFailedBatch(Guid batch)
        {
            await vinRepo.RollbackFailedBatch(batch);
        }
    }
}
