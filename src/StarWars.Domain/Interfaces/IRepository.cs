using StarWars.Domain.Core;
using System;
using System.Threading.Tasks;

namespace StarWars.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        int SaveChanges();
    }
}
