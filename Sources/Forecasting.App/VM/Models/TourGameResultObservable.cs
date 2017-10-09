using AutoMapper;
using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class TourGameResultObservable:ObservableModelWithId<long>
    {
        public long TournamentTeam1Id { get; set; }
        public long TournamentTeam2Id { get; set; }

        public int TournamentTeam1Points { get; set; }
        public int TournamentTeam2Points { get; set; }
    }

    internal class TourGameResultObservableMappingProfile:Profile
    {
        public TourGameResultObservableMappingProfile()
        {
            CreateMap<TourGameResult, TourGameResultObservable>();
            CreateMap<TourGameResultObservable, TourGameResult>();
        }
    }
}
