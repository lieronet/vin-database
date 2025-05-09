using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using vin_db.Domain;
using vin_db.Models;
using vin_db.Repos;

namespace vin_db.Services
{
    public class VinService : IVinService
    {
        private readonly IVinRepo _vinRepo;
        private readonly IVinNpRepo _vinNpRepo;
        private readonly Configuration _config;

        public VinService(IVinRepo vinRepo, IVinNpRepo vinNpRepo, IOptions<Configuration> config)
        {
            _vinRepo = vinRepo;
            _vinNpRepo = vinNpRepo;
            _config = config.Value;
        }

        public async Task<VinDetailModel> GetVinRecord(string vin)
        {
            return await _vinNpRepo.GetVinRecord(vin);
        }

        public async Task InsertVinList(List<VinRecordDataModel> vinRecords)
        {
            return await _vinRepo.InsertVinList(vinRecords);
        }

        public async Task<ParseResponse> Parse(string csvVinList)
        {
            var result = new ParseResponse
            {
                VinRecords = [],
                Valid = false
            };

            var extraColumnDetected = false;
            using var parser = new TextFieldParser(csvVinList);

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                var line = parser.LineNumber;
                var fields = parser.ReadFields();

                if (fields == null) continue;

                if(fields.Length < 3)
                {
                    result.Errors.Append(new ErrorRecord 
                    { 
                        LineNumber= line,
                        Error = "Missing column. Skipping record."
                    });

                }

                if (fields.Length > 3 && !extraColumnDetected)
                {
                    extraColumnDetected = true;
                    result.Errors.Append(new ErrorRecord 
                    { 
                        Error = "Detected extra column in dataset. Ignoring.",
                        LineNumber = 0
                    });
                }
                //headers
                if (line == 0 && !int.TryParse(fields[0], out var _))
                {
                    if (fields[0].ToLower() != "dealerid" 
                        || fields[1].ToLower() != "vin" 
                        || fields[2].ToLower() != "modifieddate")
                    {
                        //this isn't great
                        result.Errors.Append(new ErrorRecord
                        {
                            LineNumber = 0,
                            Error = "Invalid data format. Expecting three columns: dealerId, vin, modifiedDate, in that order"
                        });

                        return result;
                    }
                }

                //just making sure the VIN is VIN-shaped
                //doing full validation on the VIN seems to require an API hit somewhere
                //so we're going to roll that into the step that hits the government's API

                if (!await ValidateVin(fields[1]))
                {
                    result.Errors.Append(new ErrorRecord
                    {
                        LineNumber = line,
                        Vin = fields[1],
                        Error = "Supplied VIN insufficiently VIN-shaped."
                    });

                    continue;
                }

                if (!int.TryParse(fields[0], out var dealerId))
                {
                    result.Errors.Append(new ErrorRecord
                    {
                        LineNumber = line,
                        Vin = fields[1],
                        Error = "DealerId invalid. Expecting an integer value."
                    });

                    continue;
                }

                if(dealerId > 99999)
                {
                    result.Errors.Append(new ErrorRecord
                    {
                        LineNumber = line,
                        Vin = fields[1],
                        Error = "DealerId invalid. Expecting a maximum of five digits."
                    });

                    continue;
                }

                if (!DateTime.TryParse(fields[2], out var modifiedDate))
                {
                    result.Errors.Append(new ErrorRecord
                    {
                        LineNumber = line,
                        Vin = fields[1],
                        Error = ""
                    });
                    continue;
                }

                result.VinRecords.Add(new VinRecordDataModel
                {
                    DealerId = dealerId,
                    Vin = fields[1],
                    ModifiedDate = modifiedDate
                });
            }

            return result;
        }

        public async Task SaveCsv(string csvVinList)
        {
            var filename = $"{DateTime.Now:dd} - {DateTime.Now:HH-mm-ss}-{DateTime.Now.Millisecond}.csv";
            var path = Path.Combine(_config.FileDumpDirectory, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var fs = new FileStream(Path.Combine(path, filename), FileMode.CreateNew);
            using var w = new BinaryWriter(fs);

            w.Write(csvVinList);
        }

        public async Task<VinSearchResults> SearchVinRecords(int pageIndex, int pageSize, DateTime? modifiedAfter, int? dealerId)
        {
            var vinRecords = await _vinNpRepo.SearchVinRecords(pageIndex, pageSize, modifiedAfter, dealerId);
            return new VinSearchResults(_config.ApiBaseUrl, pageSize, pageIndex, vinRecords, modifiedAfter, dealerId);
        }

        public async Task<bool> ValidateVin(string vin) => !Regex.Match(vin, "[A-HJ-NPR-Z0-9]{17}").Success;
    }
}
