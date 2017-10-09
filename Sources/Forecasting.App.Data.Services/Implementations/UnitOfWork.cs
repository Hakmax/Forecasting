using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services.Implementations
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ForecastingAppDbContext _dbContext;

        public bool AutoDetectChanges
        {
            get
            {
                return _dbContext.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public UnitOfWork(ForecastingAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ExecuteSql(string sql)
        {
            _dbContext.Database.ExecuteSqlCommand(sql);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
