using vin_db.Models;

namespace vin_db.Services
{
    public interface IVinService
    {
        //public Task<ValidationResponse> Validate(List<VinRecordDataModel> vinRecords);
        public Task<ParseResponse> Parse(string csvVinList);
        public Task InsertVinList(List<VinRecordDataModel> vinRecords);
        public Task SaveCsv(string csvVinList);
        public bool ValidateVin(string vin);
        public Task<VinDetailModel> GetVinRecord(string vin);
        public Task<VinSearchResults> SearchVinRecords(int pageIndex, int pageSize, DateTime? modifiedAfter, int? dealerId);
    }
}
