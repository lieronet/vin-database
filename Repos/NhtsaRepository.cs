using System.Text.Json;
using RestSharp;
using vin_db.Models;

namespace vin_db.Repos
{
    public class NhtsaRepository : INhtsaRepository
    {
        private readonly ILogger<NhtsaRepository> _logger;
        public NhtsaRepository(ILogger<NhtsaRepository> logger)
        {
            _logger = logger;
        }
        public async Task<BatchVinDecodeModel> GetVinData(IEnumerable<string> vins)
        {
            var restClient = new RestClient("https://vpic.nhtsa.dot.gov/api/");

            restClient.AddDefaultHeader("Accept", "application/json");

            var request = new RestRequest("vehicles/DecodeVINValuesBatch/", Method.Post);
            
            request.AddBody($"data={string.Join(';', vins)}&format=json", ContentType.FormUrlEncoded);

            //var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVINValuesBatch/";
            //var content = new StringContent($"data={string.Join(';', vins)}&format=json");
            
            var response = await restClient.ExecuteAsync<BatchVinDecodeModel>(request);

            

            return response.Data;
        }
    }
}
