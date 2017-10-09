using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services
{
    public interface IDataService<TEntity, in TKey> where TEntity:Entities.Entity<TKey>
    {
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
        TEntity Get(TKey key,  params Expression<Func<TEntity, object>>[] includes);

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
