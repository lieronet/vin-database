namespace vin_db.Models
{
    public class ValidationResponse
    {
        public List<string> InvalidVins { get; set; }
        public string ResponseMessage {  get; set; }
    }
}
