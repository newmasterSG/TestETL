using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TestETL.Domain.Models;
using TestETL.Infrastructure.DTO;
using TestETL.Infrastructure.Interfaces;

namespace TestETL.Infrastructure.Repository
{
    public class CsvRepository(EtldbContext etldbContext) : ICsvRepository
    {

        public async Task AddAsync(Csv Entity, CancellationToken cancellationToken = default)
        {
            await etldbContext.Csvs.AddAsync(Entity, cancellationToken);
        }

        public async Task AddBatchToDbAsync(List<CsvDTO> batch)
        {
            var withoutDuplicate = batch.DistinctBy(x => new { x.PickupDateTime, x.DropoffDateTime, x.PassengerCount }).ToList();

            var cvs = withoutDuplicate.Select(x => new Csv
            {
                DolocationId = x.DOLocationID ?? 0,
                FareAmount = x.FareAmount,
                PassengerCount = x.PassengerCount ?? 0,
                TpepDropoffDatetime = DateTime.TryParseExact(x.DropoffDateTime, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dropoffDateTime)
                    ? DateTime.SpecifyKind(dropoffDateTime, DateTimeKind.Utc)
                    : DateTime.MinValue,
                TpepPickupDatetime = DateTime.TryParseExact(x.PickupDateTime, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime pickupDateTime)
                    ? DateTime.SpecifyKind(pickupDateTime, DateTimeKind.Utc)
                    : DateTime.MinValue,
                TripDistance = (decimal?)x.TripDistance,
                PulocationId = x.PULocationID ?? 0,
                StoreAndFwdFlag = x.StoreAndForwardFlag.Contains('N') ? "No" : "Yes",
                TipAmount = x.TipAmount
            }).ToList();

            etldbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                await etldbContext.Csvs.AddRangeAsync(cvs);

                await etldbContext.SaveChangesAsync();
            }
            finally
            {
                etldbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public async Task AddListAsync(ICollection<Csv> entities, CancellationToken cancellationToken = default)
        {
            await etldbContext.Csvs.AddRangeAsync(entities, cancellationToken);
            await etldbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await etldbContext.Csvs.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<Csv> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await etldbContext.Csvs.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Csv>> GetListAsync(int pageSize = 10, int pageNumber = 1, CancellationToken cancellationToken = default)
        {
            return await etldbContext.Csvs
                .Skip(pageNumber)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Csv entity, CancellationToken cancellationToken = default)
        {
            await etldbContext.Csvs
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(str =>
                     str
                        .SetProperty(x => x.TpepPickupDatetime, entity.TpepPickupDatetime)
                        .SetProperty(x => x.TpepDropoffDatetime, entity.TpepDropoffDatetime),
                cancellationToken);
        }
    }
}
