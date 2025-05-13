
using vin_db.Models;
using vin_db.Repos;
using vin_db.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyModel;
using Serilog.Extensions.Logging;
using Serilog.Settings.Configuration;

namespace vin_db
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //make sure the file dump directory exists
            var appConfig = new Configuration();

            builder.Configuration.GetSection(Configuration.Section).Bind(appConfig);

            if (!Directory.Exists(appConfig.FileDumpDirectory)) { 
                Directory.CreateDirectory(appConfig.FileDumpDirectory);
            }

            builder.Services.AddDbContext<VinDbContext>(options =>
              options.UseSqlServer(appConfig.ConnectionString));

            // Add services to the container.

            builder.Services.AddSingleton<ILoggerProvider>(sp =>
            {
                var functionDependencyContext = DependencyContext.Load(typeof(Program).Assembly);

                var hostConfig = sp.GetRequiredService<IConfiguration>();
                var options = new ConfigurationReaderOptions(functionDependencyContext) { SectionName = "Serilog" };
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(hostConfig, options)
                    .CreateLogger();

                return new SerilogLoggerProvider(logger, dispose: true);
            });

            builder.Services.Configure<Configuration>(builder.Configuration.GetSection(Configuration.Section));
            builder.Services.Configure<VinQueueConfiguration>(builder.Configuration.GetSection(VinQueueConfiguration.Section));

            builder.Services.AddScoped<IVinQueueService, VinQueueService>();
            builder.Services.AddScoped<IVinService, VinService>();

            builder.Services.AddScoped<IVinNpRepo, VinNpRepo>();
            builder.Services.AddScoped<IVinRepo, VinRepo>();
            builder.Services.AddScoped<INhtsaRepository, NhtsaRepository>();

            builder.Services.AddHostedService<VinQueueHostService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<VinDbContext>();
                context.Database.EnsureCreated();
                //DbInitializer.Initialize(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
