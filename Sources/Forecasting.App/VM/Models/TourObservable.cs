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

    public class TourObservableBase : ObservableModelWithId<long>
    {
        
        public int TourNumber { get; set; }
        public long TournamentId { get; set; }
    }

    public class TourObservable : TourObservableBase
    {

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

    public class TourExtendedObservable : TourObservableBase
    {
        public List<PlayerExtended> Players { get; set; }
    }

    public class PlayerExtended : ObservableModelWithName<long>
    {
        private ObservableCollection<PlayerTourForecast> _forecasts;
        public ObservableCollection<PlayerTourForecast> Forecasts
        {
            get
            {
                return _forecasts;
            }
            set
            {
                Set(() => Forecasts, ref _forecasts, value);
            }
        }

    }

    internal class TourObservableMappingProfile : Profile
    {
        public TourObservableMappingProfile()
        {
            CreateMap<Tour, TourObservable>();
            CreateMap<Tour, TourExtendedObservable>();
            CreateMap<TourObservable, Tour>();
        }
    }
}
