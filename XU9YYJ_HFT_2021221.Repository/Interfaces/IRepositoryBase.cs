using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Repository.Interfaces
{
    public interface IRepositoryBase <TEntity, TKey>
    {
        IQueryable<TEntity> ReadAll();
        TEntity Read(TKey id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TKey id);
    }
}
