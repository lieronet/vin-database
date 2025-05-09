namespace vin_db.Models
{
    public class ValidationResponse
    {
        public List<string> InvalidVins { get; set; }
        public List<Tuple<string,string>> ResponseMessage {  get; set; }
    }
}
