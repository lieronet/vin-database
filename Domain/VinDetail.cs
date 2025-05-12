using System.ComponentModel.DataAnnotations.Schema;
using vin_db.Models;

namespace vin_db.Domain
{
    public class VinDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Vin { get; set; }
        public int DealerId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
