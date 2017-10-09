using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.Tests
{
    public static class DependencyConfig
    {
        public static IContainer Container { get; private set; }
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<App.Data.Services.DataServicesModule>();
            builder.RegisterModule<App.Services.ServicesModule>();
            Container = builder.Build();

            App.Services.Models.ModelsMappingConfig.Configure();
        }
    }
}
