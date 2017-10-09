using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Forecasting.App.VM.Models;

namespace Forecasting.App
{
    internal static class AppMappingConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(AddMappings);
        }


        public static void AddMappings(AutoMapper.IMapperConfigurationExpression config)
        {
            config.AddProfile<VM.Models.TournamentObservableMappingProfile>();
            config.AddProfile<TournamentTeamObservableMappingProfile>();
            config.AddProfile<PlayerObservableMappingProfile>();
            config.AddProfile<TourObservableMappingProfile>();
            config.AddProfile<TourGameResultObservableMappingProfile>();
            Services.Models.ModelsMappingConfig.AddMappings(config);
        }
    }
}
