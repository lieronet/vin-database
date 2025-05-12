
using Microsoft.Extensions.Options;
using vin_db.Models;
using vin_db.Repos;

namespace vin_db.Services
{
    public class VinQueueService : IVinQueueService
    {
        private readonly ILogger<VinQueueService> logger;
        private readonly IVinRepo vinRepo;
        private readonly IVinNpRepo vinNpRepo;
        private readonly IOptions<Configuration> config;

        public VinQueueService(ILogger<VinQueueService> logger, IVinRepo vinRepo, IVinNpRepo vinNpRepo, IOptions<Configuration> config)
        {
            //I give up
            this.logger = logger;
            this.vinRepo = vinRepo;
            this.vinNpRepo = vinNpRepo;
            this.config = config;
        }
        public Task Foo()
        {
            logger.LogInformation("We did it!");

            return Task.CompletedTask;
        }
    }
}
