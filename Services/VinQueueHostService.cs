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

            _timer = new Timer(ProcessQueue, null, 0, _queueConfig.IntervalMilliseconds);

            return Task.CompletedTask;
        }

        private void ProcessQueue(object? state)
        {
            _logger.LogInformation("Attempting to do the thing");

            using (var scope = _services.CreateScope())
            {
                var queueProcessor = scope.ServiceProvider.GetRequiredService<IVinQueueService>();

                _logger.LogInformation("created the thing");

                queueProcessor.Foo(); 
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
