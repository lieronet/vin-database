namespace vin_db.Models
{
    public class VinDetailModel
    {
        public string Vin { get; set; }
        public string DealerId { get; set; }
        public string ModifiedDate { get; set; }
        public decimal? TrackWidth { get; set; }
        public decimal? WheelBaseFrom { get; set; }
        public decimal? WheelBaseTo { get; set; }
        public int? Doors { get; set; }
        public int? Windows { get; set; }
        public int? BedLength { get; set; }
        public int? NumberOfWheels { get; set; }
        public int? ModelYear { get; set; }
        public string? Series { get; set; }
        public string? BodyClass { get; set; }
        public string? WheelBaseType { get; set; }
        public string? CustomMotorcycleType { get; set; }
        public string? MotorcycleChassisType { get; set; }
        public string? Make { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? VehicleType { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorText { get; set; }
    }
}
