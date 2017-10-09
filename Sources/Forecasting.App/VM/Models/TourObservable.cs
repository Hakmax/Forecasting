using AutoMapper;
using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class TourObservable : ObservableModelWithId<long>
    {
        public int TourNumber { get; set; }

        private ObservableCollection<TourGameResultObservable> _tourGameResults;
        public ObservableCollection<TourGameResultObservable> TourGameResults
        {
            get
            {
                return _tourGameResults;
            }
            set
            {
                Set(() => TourGameResults, ref _tourGameResults, value);
            }
        }
    }

    internal class TourObservableMappingProfile : Profile
    {
        public TourObservableMappingProfile()
        {
            CreateMap<Tour, TournamentTeamObservable>();
            CreateMap<TournamentTeamObservable, Tour>();
        }
    }
}
