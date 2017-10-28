using AutoMapper;
using Forecasting.App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class TourGameResult:ModelWithId<long>
    {
        public long TourId { get; set; }
        public long TournamentTeam1Id { get; set; }

        public long TournamentTeam2Id { get; set; }

        public int TournamentTeam1Points { get; set; }
        public int TournamentTeam2Points { get; set; }

        public TourGameSummaryType TourGameSummaryType { get; set; }
        public List<TourGameForecast> TourGameForecasts { get; set; }
    }

    internal class TourGameResultMappingProfile:Profile
    {
        public TourGameResultMappingProfile()
        {
            CreateMap<Entities.Forecast.TourGameResult, TourGameResult>();
            CreateMap<TourGameResult, Entities.Forecast.TourGameResult>();
        }
    }
}
