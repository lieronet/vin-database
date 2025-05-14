using RestSharp;
using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public interface INhtsaRepository
    {
        public Task<BatchVinDecodeModel> GetVinData(IEnumerable<string> vins);
    }
}
