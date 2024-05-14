using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;
using TestETL.Domain.Models;
using TestETL.Infrastructure.DTO;
using TestETL.Infrastructure.Interfaces;

namespace TestETL.Application.Services
{
    public class CsvService(ICsvRepository csvRepository)
    {
        public async Task AddDataToDb(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };

            List<CsvDTO> duplicates = new List<CsvDTO>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<CsvDTO>();
                var dTOs = records.ToList();

                duplicates = dTOs.GroupBy(x => new { x.PickupDateTime, x.DropoffDateTime, x.PassengerCount })
                      .Where(g => g.Count() > 1)
                      .SelectMany(g => g)
                      .ToList();

                string pathProject = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string pathToFolder = Path.Combine(pathProject, "Data");
                string pathToFileDuplicate = Path.Combine(pathToFolder, "dublicate.csv");
                await CreateDuplicatesCsvAsync(duplicates, pathToFileDuplicate);

                var withoutDuplicate = dTOs.DistinctBy(x => new { x.PickupDateTime, x.DropoffDateTime, x.PassengerCount }).ToList();

                int batchSize = 1000;
                for (int i = 0; i < withoutDuplicate.Count; i += batchSize)
                {
                    var batch = withoutDuplicate.Skip(i).Take(batchSize).ToList();
                    await csvRepository.AddBatchToDbAsync(batch);
                }
            }
        }

        private async Task CreateDuplicatesCsvAsync(List<CsvDTO> duplicates, string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                await csv.WriteRecordsAsync(duplicates);
            }
        }
    }
}
