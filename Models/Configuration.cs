namespace vin_db.Models
{
    public class Configuration
    {
        public const string Section = "ApplicationConfiguration";
        public string FileDumpDirectory { get; set; }
        public string ConnectionString { get; set; }
        public string TestFile { get; set; }
        public string ApiBaseUrl { get; set; }
    }

    public class VinQueueConfiguration
    {
        public const string Section = "ApplicationConfiguration:VinQueueConfiguration";
        public int BatchSize { get; set; }
        public int IntervalMilliseconds { get; set; }  
    }
}
