using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Data
{
    public interface IRepository<TPersistent> where TPersistent : class
    {
        Task AddAsync(TPersistent entity, CancellationToken cancellationToken = default);
        Task AddAsync(IEnumerable<TPersistent> entities, CancellationToken cancellationToken = default);
        Task<long> CountAsync();
        Task<bool> AnyAsync(Expression<Func<TPersistent, bool>> filter);
        Task<TPersistent> FindAsync(long id, CancellationToken cancellationToken = default);
        Task<TPersistent> FindAsync(Expression<Func<TPersistent, bool>> predicate, params Expression<Func<TPersistent, object>>[] includeProperties);
        Task<TPersistent> GetFirstOrDefaultAsync();
        Task<IEnumerable<TPersistent>> GetAllAsync();
        Task<IEnumerable<TPersistent>> GetAllAsync(Expression<Func<TPersistent, bool>> filter, Func<IQueryable<TPersistent>, IOrderedQueryable<TPersistent>> orderBy = null, int? skip = default, int? take = default, params Expression<Func<TPersistent, object>>[] includeProperties);
        Task<IEnumerable<TPersistent>> GetAllAsync(List<Expression<Func<TPersistent, bool>>> filters, Func<IQueryable<TPersistent>, IOrderedQueryable<TPersistent>> orderBy = null, int? skip = default, int? take = default, params Expression<Func<TPersistent, object>>[] includeProperties);
        Task Delete(long id, CancellationToken cancellationToken = default);
        Task Delete(Expression<Func<TPersistent, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
