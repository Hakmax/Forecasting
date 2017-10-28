using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class Tournament : ModelWithName<long>
    {
        public List<TournamentTeam> TournamentTeams { get; set; }
        public List<Player> Players { get; set; }
        public List<Tour> Tours { get; set; }
    }

    internal class TournamentMappingProfile : Profile
    {
        public TournamentMappingProfile()
        {
            CreateMap<Entities.Forecast.Tournament, Tournament>()
                .ForMember(dst => dst.TournamentTeams, opt => opt.ExplicitExpansion())
                .ForMember(dst => dst.Players, opt => opt.ExplicitExpansion())
                .ForMember(dst => dst.Tours, opt => opt.ExplicitExpansion());
            CreateMap<Tournament, Entities.Forecast.Tournament>().ForMember(dst=>dst.TournamentTeams, opt=>opt.Ignore())
                .ForMember(dst => dst.Players, opt => opt.Ignore());
        }
    }

}
