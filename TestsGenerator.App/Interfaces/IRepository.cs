﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.App.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQueryable();

        Task InsertAsync(T entity, CancellationToken ct);
        Task InsertAsync(IEnumerable<T> entities, CancellationToken ct);
        Task UpdateAsync(T entity, CancellationToken ct);
        Task UpdateAsync(IEnumerable<T> entities, CancellationToken ct);
        Task DeleteAsync(T entity, CancellationToken ct);
        Task DeleteAsync(IEnumerable<T> entities, CancellationToken ct);
        void Attach<TEntity>(TEntity entity) where TEntity: class;
        void Attach<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
