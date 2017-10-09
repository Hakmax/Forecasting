using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class Tour:ModelWithId<long>
    {
        public int TourNumber { get; set; }
        public long TournamentId { get; set; }

        public List<TourGameResult> TourGameResults { get; set; }
    }

    internal class TourMappingProfile : Profile
    {
        public TourMappingProfile()
        {
            CreateMap<Entities.Forecast.Tour, Tour>();
            CreateMap<Tour, Entities.Forecast.Tour>();
        }
    }
}
