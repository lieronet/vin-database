namespace vin_db.Models
{
    public class VinSearchResults
    {
        private readonly string _baseUrl;
        private readonly int _pageSize;
        private readonly int _pageIndex;
        private readonly DateTime? _modifiedAfter;
        private readonly int? _dealerId;

        private string dealerQuery => _dealerId.HasValue ? $"&dealerId={_dealerId}" : string.Empty;
        private string modifiedAfterQuery => _modifiedAfter.HasValue ? $"&modifiedAfter={_modifiedAfter.Value:yyyy-MM-dd}" : string.Empty;

        public VinSearchResults(string apiBaseUrl, int pageSize, int pageIndex, IEnumerable<VinRecordDataModel> vinRecords, DateTime? modifiedAfter, int? dealerId)
        {
            _baseUrl = apiBaseUrl;
            _pageSize = pageSize;
            _pageIndex = pageIndex;
            _modifiedAfter = modifiedAfter;
            _dealerId = dealerId;

            VinRecords = vinRecords;
        }
        public IEnumerable<VinRecordDataModel> VinRecords { get; }
        public string PreviousPageUrl => _pageIndex == 0 ? string.Empty : $"{_baseUrl}/vin?pageSize={_pageSize}&pageIndex={_pageIndex-1}{dealerQuery}{modifiedAfterQuery}";
        public string NextPageUrl => VinRecords.Count() != _pageSize ? string.Empty : $"{_baseUrl}/vin?pageSize={_pageSize}&pageIndex={_pageIndex + 1}{dealerQuery}{modifiedAfterQuery}";
    }
}
