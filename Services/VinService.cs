using vin_db.Models;
using vin_db.Repos;

namespace vin_db.Services
{
    public class VinService : IVinService
    {
        private readonly IVinRepo vinRepo;
        private readonly IVinNpRepo vinNpRepo;

        public VinService(IVinRepo vinRepo, IVinNpRepo vinNpRepo)
        {
            this.vinRepo = vinRepo;
            this.vinNpRepo = vinNpRepo;
        }

        public async Task InsertVinList(List<VinRecordDataModel> vinRecords)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VinRecordDataModel>> Parse(string csvVinList)
        {
            throw new NotImplementedException();
        }

        public async Task SaveCsv(string csvVinList)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResponse> Validate(List<VinRecordDataModel> vinRecords)
        {
            var result = new ValidationResponse();

            foreach (var vinRecord in vinRecords)
            {

            }

            return result;
        }

        public async Task<bool> ValidateVin(string vin)
        {
            throw new NotImplementedException();
        }
    }
}
