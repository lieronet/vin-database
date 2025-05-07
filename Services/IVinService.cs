using vin_db.Models;

namespace vin_db.Services
{
    public interface IVinService
    {
        public Task<ValidationResponse> Validate(List<VinRecordDataModel> vinRecords);
        public Task<List<VinRecordDataModel>> Parse(string csvVinList);
        public Task InsertVinList(List<VinRecordDataModel> vinRecords);
        public Task SaveCsv(string csvVinList);
        public Task<bool> ValidateVin(string vin);
    }
}
