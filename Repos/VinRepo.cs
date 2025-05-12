using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using vin_db.Domain;
using vin_db.Models;

namespace vin_db.Repos
{
    public class VinRepo : IVinRepo
    {
        private readonly VinDbContext _context;
        public VinRepo(VinDbContext context)
        {
            _context = context;
        }

        public async Task InsertVinList(List<VinRecordDataModel> vinRecords)
        {
            _context.VinQueue.AddRange(vinRecords.Select(v => new VinQueue(v)));

            await _context.SaveChangesAsync();
        }
    }
}
