using vin_db.Models;

namespace vin_db.Repos
{
    public interface IVinRepo
    {
        Task InsertVinList(List<VinRecordDataModel> vinRecords);
    }
}
