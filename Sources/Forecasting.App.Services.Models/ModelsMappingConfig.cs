using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models
{
    public class ModelsMappingConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(AddMappings);
        }

        public static void AddMappings(AutoMapper.IMapperConfigurationExpression config)
        {
            config.AddProfile<PlayerMappingProfile>();
            config.AddProfile<TourMappingProfile>();
            config.AddProfile<TourForecastMappingProfile>();
            config.AddProfile<TourGameResultMappingProfile>();
            config.AddProfile<TournamentMappingProfile>();
            config.AddProfile<TournamentTeamMappingProfile>();
        }
    }
}
