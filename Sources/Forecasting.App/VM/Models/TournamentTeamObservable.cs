using AutoMapper;
using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class TournamentTeamObservable:ObservableModelWithName<long>
    {
    }

    internal class TournamentTeamObservableMappingProfile:Profile
    {
        public TournamentTeamObservableMappingProfile()
        {
            CreateMap<TournamentTeam, TournamentTeamObservable>();
            CreateMap<TournamentTeamObservable, TournamentTeam>();
        }
    }

}
