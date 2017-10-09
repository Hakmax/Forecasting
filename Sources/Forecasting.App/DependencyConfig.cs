using Autofac;
using Forecasting.App.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App
{
    public static class DependencyConfig
    {
        public static IContainer Container { get; private set; }
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<Data.Services.DataServicesModule>();
            builder.RegisterModule<Services.ServicesModule>();
            builder.RegisterType<MainVM>().AsSelf();


            Container = builder.Build();
            AppMappingConfig.Configure();
        }
    }
}
