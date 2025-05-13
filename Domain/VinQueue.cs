using System.ComponentModel.DataAnnotations.Schema;
using vin_db.Models;

namespace vin_db.Domain
{
    public class VinQueue
    {
        public VinQueue(VinRecordDataModel model)
        {
            Vin = model.Vin;
            DealerId = model.DealerId;
            ModifiedDate = model.ModifiedDate;
        }
        public VinQueue() : base()
        {
        }   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Vin { get; set; }
        public int DealerId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? InUseBy { get; set; }
        public DateTime? InUseDate { get; set; }
    }
}
