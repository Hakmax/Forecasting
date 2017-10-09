using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services.Implementations
{
    abstract class DataService<TEntity, TKey> : IDataService<TEntity, TKey> where TEntity : Entities.Entity<TKey>
    {
        protected ForecastingAppDbContext Context { get; private set; }

        protected IDbSet<TEntity> DataSet { get { return Context.Set<TEntity>(); } }

        public DataService(ForecastingAppDbContext context)
        {
            Context = context;
        }

        public TEntity Get(TKey key, params Expression<Func<TEntity, object>>[] includes)
        {
            return Query(includes).FirstOrDefault(x =>(object) x.Id == (object) key);
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            return includes.Aggregate(DataSet.AsQueryable(), (q, include) => q.Include(include));
        }

        public virtual void Create(TEntity entity)
        {
            DataSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
        }

        public virtual void Delete(TEntity entity)
        {
            DataSet.Remove(entity);
        }
    }
}
