using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public interface INhtsaRepository
    {
        public Task<List<DecodedVin>> GetVinData(List<string> vins);
    }
}
