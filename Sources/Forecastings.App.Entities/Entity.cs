using Forecasting.App.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Entities
{
    public class Entity<TKey>
    {
        public TKey Id { get; set; }
    }

    public class EntityWithName<TKey>:Entity<TKey>
    {
        [MaxLength(DbConstants.DefaultStringMaxLength)]
        public string Name { get; set; }
    }

    public interface IWatchedEntity
    {
        DateTime CreationDate { get; set; }
    }

}
