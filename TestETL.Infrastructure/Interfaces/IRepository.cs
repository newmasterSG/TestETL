using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestETL.Infrastructure.Interfaces
{
    public interface IRepository<TEntityDTO>
        where TEntityDTO : class
    {
        Task AddAsync(TEntityDTO Entity, CancellationToken cancellationToken = default);
        Task<TEntityDTO> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<List<TEntityDTO>> GetListAsync(int pageSize = 10, int pageNumber = 1, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntityDTO entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task AddListAsync(ICollection<TEntityDTO> entities, CancellationToken cancellationToken = default);
    }
}
