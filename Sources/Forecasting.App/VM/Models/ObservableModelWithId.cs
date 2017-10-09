using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class ObservableModelWithId<TKey>:ObservableObject
    {
        public TKey Id { get; set; }
    }

    public class ObservableModelWithName<TKey> : ObservableModelWithId<TKey>
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(()=>Name, ref _name, value);
            }
        }
    }

}
