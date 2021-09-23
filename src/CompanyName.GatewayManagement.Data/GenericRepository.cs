using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Data
{
    public sealed class GenericRepository<TPersistent> : IRepository<TPersistent> where TPersistent : class
    {
        private readonly GatewayDbContext _dbContext;

        public GenericRepository(GatewayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TPersistent entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TPersistent>().AddAsync(entity);
        }

        public async Task AddAsync(IEnumerable<TPersistent> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<TPersistent>().AddRange(entities);
            });
        }

        public async Task<long> CountAsync()
        {
            IQueryable<TPersistent> query = _dbContext.Set<TPersistent>();

            return await query.AsNoTracking().CountAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TPersistent, bool>> filter)
        {
            IQueryable<TPersistent> query = _dbContext.Set<TPersistent>();

            return await query.AsNoTracking().AnyAsync(filter);
        }

        public async Task<TPersistent> FindAsync(long id, CancellationToken cancellationToken = default)
        {
            return await Task.Run<TPersistent>(() => _dbContext.Set<TPersistent>().Find(id));
        }

        public async Task<TPersistent> FindAsync(Expression<Func<TPersistent, bool>> predicate, params Expression<Func<TPersistent, object>>[] includeProperties)
        {
            var query = _dbContext.Set<TPersistent>().Where(predicate);

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return await Task.Run<TPersistent>(() => query.FirstOrDefault());
        }

        public async Task<TPersistent> GetFirstOrDefaultAsync()
        {
            IQueryable<TPersistent> query = _dbContext.Set<TPersistent>();

            return await Task.Run<TPersistent>(() => query.FirstOrDefault());
        }

        public async Task<IEnumerable<TPersistent>> GetAllAsync()
        {
            IQueryable<TPersistent> query = _dbContext.Set<TPersistent>();

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TPersistent>> GetAllAsync(Expression<Func<TPersistent, bool>> filter, Func<IQueryable<TPersistent>, IOrderedQueryable<TPersistent>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<TPersistent, object>>[] includeProperties)
        {
            IQueryable<TPersistent> query = _dbContext.Set<TPersistent>();

            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TPersistent>> GetAllAsync(List<Expression<Func<TPersistent, bool>>> filters, Func<IQueryable<TPersistent>, IOrderedQueryable<TPersistent>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<TPersistent, object>>[] includeProperties)
        {
            IQueryable<TPersistent> query = _dbContext.Set<TPersistent>();

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task Delete(long id, CancellationToken cancellationToken = default)
        {
            var persistent = await Task.Run<TPersistent>(() => _dbContext.Set<TPersistent>().Find(id));
            await Task.Run(() =>
            {
                _dbContext.Remove(persistent);
            });
        }

        public async Task Delete(Expression<Func<TPersistent, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = _dbContext.Set<TPersistent>().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                await Task.Run(() =>
                {
                    _dbContext.Remove(entity);
                });
            }
        }


    }
}
