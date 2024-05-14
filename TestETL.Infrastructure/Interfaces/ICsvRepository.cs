using TestETL.Domain.Models;
using TestETL.Infrastructure.DTO;

namespace TestETL.Infrastructure.Interfaces
{
    public interface ICsvRepository 
        : IRepository<Csv>
    {
        Task AddBatchToDbAsync(List<CsvDTO> batch);
    }
}
