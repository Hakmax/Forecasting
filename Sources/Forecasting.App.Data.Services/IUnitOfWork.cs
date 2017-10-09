using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services
{
    public interface IUnitOfWork:IExtendedUnitOfWork
    {
        bool AutoDetectChanges { get; set; }
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }

    public interface IExtendedUnitOfWork
    {
        void ExecuteSql(string sql);
    }
}
