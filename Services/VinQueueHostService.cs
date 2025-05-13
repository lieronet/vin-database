using Microsoft.Extensions.Options;
using vin_db.Models;
using vin_db.Repos;

namespace vin_db.Services
{
    public class VinQueueHostService : IHostedService
    {
        private readonly VinQueueConfiguration _queueConfig;
        private readonly IServiceProvider _services;
        private readonly ILogger<VinQueueHostService> _logger;
        private Timer? _timer = null;
        public VinQueueHostService(IServiceProvider services, IOptions<VinQueueConfiguration> configuration, ILogger<VinQueueHostService> logger)
        {
            _queueConfig = configuration.Value;
            _services = services;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("VinQueueHostService is starting.");

            //_timer = new Timer(ProcessQueue, null, 0, _queueConfig.IntervalMilliseconds);

            ProcessQueue(null);

            return Task.CompletedTask;
        }

        private async void ProcessQueue(object? state)
        {
            _logger.LogInformation("Attempting to do the thing");

            //very open to the possibility there's a better way here
            using var scope = _services.CreateScope();
            var queueProcessor = scope.ServiceProvider.GetRequiredService<IVinQueueService>();
            var batchName = Guid.NewGuid();

            try
            {
                var queueItems = await queueProcessor.GetQueue(batchName, _queueConfig.BatchSize);

                var processedVins = await queueProcessor.ProcessRecords(queueItems);
            }
            catch
            {

            }
            finally
            {
                await queueProcessor.RollbackFailedBatch(batchName);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
