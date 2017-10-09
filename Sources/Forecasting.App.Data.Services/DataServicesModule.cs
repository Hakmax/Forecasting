using Autofac;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services
{
    public class DataServicesModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsImplementedInterfaces().InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.RegisterType<ForecastingAppDbContext>().AsSelf().InstancePerLifetimeScope()
                .PropertiesAutowired();
            base.Load(builder);
        }
    }
}
