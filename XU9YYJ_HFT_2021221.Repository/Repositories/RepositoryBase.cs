using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Data.DbContexts;
using XU9YYJ_HFT_2021221.Repository.Interfaces;

namespace XU9YYJ_HFT_2021221.Repository.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        protected XU9YYJ_DbContext Context;
        public RepositoryBase(XU9YYJ_DbContext context)
        {
            Context = context;
        }
        public TEntity Create(TEntity entity)
        {
            var result = Context.Add(entity);
            Context.SaveChanges();
            return result.Entity;
        }

        public void Delete(TKey id)
        {
            Context.Remove(Read(id));
            Context.SaveChanges();
        }

        public abstract TEntity Read(TKey id);
        

        public IQueryable<TEntity> ReadAll()
        {
            return Context.Set<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            var result = Context.Update(entity);
            Context.SaveChanges();
            return result.Entity;
        }
    }
}
