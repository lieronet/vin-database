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
}
