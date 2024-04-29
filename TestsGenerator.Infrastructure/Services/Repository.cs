using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Infrastructure.Database;

namespace TestsGenerator.Infrastructure.Services
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private readonly TestsDbContext _ctx;
        private readonly DbSet<T> _dbset;

        public Repository(TestsDbContext ctx)
        {
            _ctx = ctx;
            _dbset = ctx.Set<T>();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbset.AsQueryable();
        }

        public async Task DeleteAsync(T entity, CancellationToken ct)
        {
            _dbset.Remove(entity);

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(IEnumerable<T> entities, CancellationToken ct)
        {
            _dbset.RemoveRange(entities);

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task InsertAsync(T entity, CancellationToken ct)
        {
            await _dbset.AddAsync(entity, ct);

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task InsertAsync(IEnumerable<T> entities, CancellationToken ct)
        {
            await _dbset.AddRangeAsync(entities, ct);

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(T entity, CancellationToken ct)
        {
            _dbset.Update(entity);

            await _ctx.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(IEnumerable<T> entities, CancellationToken ct)
        {
            _dbset.UpdateRange(entities);

            await _ctx.SaveChangesAsync(ct);
        }
    }
}
