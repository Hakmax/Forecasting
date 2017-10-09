using AutoMapper;
using Forecasting.App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class TourForecast:ModelWithId<long>
    {
        public long TourGameResultId { get; set; }
        public long PlayerId { get; set; }
        public int Team1Points { get; set; }
        public int Team2Points { get; set; }
        public TourGameSummaryType TourGameSummaryType { get; set; }

    }

    internal class TourForecastMappingProfile : Profile
    {
        public TourForecastMappingProfile()
        {
            CreateMap<Entities.Forecast.TourForecast, TourForecast>();
            CreateMap<TourForecast, Entities.Forecast.TourForecast>();
        }
    }

}
