using Autofac;
using Forecasting.App.VM;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App
{
    public class VMLocator
    {
        public VMLocator()
        {

        }

        public MainVM MainVM
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic)
                    return new MainVM();
                else
                    return DependencyConfig.Container.Resolve<MainVM>();
            }
        }
    }
}
