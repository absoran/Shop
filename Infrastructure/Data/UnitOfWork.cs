using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using System.Collections;


namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopDbContext dbContext;
        private Hashtable repos;
        public UnitOfWork(ShopDbContext context)
        {
            dbContext = context;
        }
        public async Task<int> Commit()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public IShopRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(repos == null)
            {
                repos = new Hashtable();
            }
            var type = typeof(TEntity).Name;

            if (!repos.ContainsKey(type))
            {
                var repositoryType = typeof(ShopRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), dbContext);
                repos.Add(type, repositoryInstance);
            }

            return (ShopRepository<TEntity>)repos[type];
        }
    }
}
