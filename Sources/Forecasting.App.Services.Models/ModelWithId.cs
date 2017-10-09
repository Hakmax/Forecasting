using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models
{
    public class ModelWithId<TKey>
    {
        public TKey Id { get; set; }
    }

    public class ModelWithName<TKey> : ModelWithId<TKey>
    {
        public string Name { get; set; }
    }

}
