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
    public class TournamentObservable:ObservableModelWithName<long>, ICloneable
    {
        private ObservableCollection<TournamentTeamObservable> _tournamentTeams;
        public ObservableCollection<TournamentTeamObservable> TournamentTeams
        {
            get
            {
                return _tournamentTeams;
            }
            set
            {
                Set(() => TournamentTeams, ref _tournamentTeams, value);
            }
        }

        private ObservableCollection<PlayerObservable> _players;
        public ObservableCollection<PlayerObservable> Players
        {
            get
            {
                return _players;
            }
            set
            {
                Set(() => Players, ref _players, value);
            }
        }

        private ObservableCollection<TourObservable> _tours;
        public ObservableCollection<TourObservable>Tours
        {
            get
            {
                return _tours;
            }
            set
            {
                Set(() => Tours, ref _tours, value);
            }
        }

        private ObservableCollection<TourExtendedObservable> _tourForecasts;

        public ObservableCollection<TourExtendedObservable> TourForecasts
        {
            get
            {
                return _tourForecasts;
            }
            set
            {
                Set(() => TourForecasts, ref _tourForecasts, value);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    internal class TournamentObservableMappingProfile:Profile
    {
        public TournamentObservableMappingProfile()
        {
            CreateMap<Tournament, TournamentObservable>();
            CreateMap<TournamentObservable, Tournament>();
        }
    }
}
