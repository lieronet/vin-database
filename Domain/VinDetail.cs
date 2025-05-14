using System.ComponentModel.DataAnnotations.Schema;
using vin_db.Helper_Functions;
using vin_db.Models;

namespace vin_db.Domain
{
    public class VinDetail
    {
        public VinDetail() : base() { }
        public VinDetail(DecodedVin vinData, VinQueue vinMetaData)
        {
            Vin = vinMetaData.Vin;
            DealerId = vinMetaData.DealerId;
            ModifiedDate = vinMetaData.ModifiedDate;

            TrackWidth = Parsers.ParseDecimal(vinData.TrackWidth);
            WheelBaseFrom = Parsers.ParseDecimal(vinData.WheelBaseShort);
            WheelBaseTo = Parsers.ParseDecimal(vinData.WheelBaseLong);
            Doors = Parsers.ParseInt(vinData.Doors);
            Windows = Parsers.ParseInt(vinData.Windows);
            BedLength = Parsers.ParseInt(vinData.BedLengthIN);
            NumberOfWheels = Parsers.ParseInt(vinData.Wheels);
            ModelYear = Parsers.ParseInt(vinData.ModelYear);

            Series = vinData.Series;
            BodyClass = vinData.BodyClass;
            WheelBaseType = vinData.WheelBaseType;
            CustomMotorcycleType = vinData.CustomMotorcycleType;
            MotorcycleChassisType = vinData.MotorcycleChassisType;
            Make = vinData.Make;
            Manufacturer = vinData.Manufacturer;
            Model = vinData.Model;
            VehicleType = vinData.VehicleType;
            ErrorCode = vinData.ErrorCode;
            ErrorText = vinData.ErrorText;

            CreatedDate = DateTime.UtcNow;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Vin { get; set; }
        public int DealerId { get; set; }
        public decimal? TrackWidth { get; set; }
        public decimal? WheelBaseFrom { get; set; }
        public decimal? WheelBaseTo { get; set; }
        public int? Doors {get;set;}
        public int? Windows {get;set;}
        public int? BedLength {get;set;}
        public int? NumberOfWheels{get;set;}
        public int? ModelYear {get;set;}
        public string? Series { get; set; }

        //lookup section
        public string? BodyClass {get;set;}
        public string? WheelBaseType {get;set;}
        public string? CustomMotorcycleType {get;set;}
        public string? MotorcycleChassisType {get;set;}
        public string? Make {get;set;}
        public string? Manufacturer {get;set;}
        public string? Model {get;set;}
        public string? VehicleType {get;set;}
        public string? ErrorCode {get;set;}
        public string? ErrorText { get; set; }

        //end lookups

        public DateTime ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
