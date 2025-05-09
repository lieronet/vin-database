namespace vin_db.Models
{
    public class ParseResponse
    {
        public bool Valid { get; set; }
        public List<VinRecordDataModel> VinRecords { get; set; }
        public IEnumerable<ErrorRecord> Errors { get; set; }
    }
    public class ErrorRecord
    {
        public long LineNumber { get; set; }
        public string Vin { get; set; }
        public string Error { get; set; }  
    }
}
