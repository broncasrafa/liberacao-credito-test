using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public abstract class AbstractRepository<TEntity> where TEntity : class
    {
        public abstract Task<TEntity> GetByIdAsync(int id);
        public abstract Task<IReadOnlyList<TEntity>> GetAllAsync();
        public abstract Task<int> AddAsync(TEntity entity);
        public abstract Task<int> UpdateAsync(TEntity entity);
        public abstract Task<int> DeleteAsync(int id);
    }
}
