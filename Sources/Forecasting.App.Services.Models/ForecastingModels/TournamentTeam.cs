using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class TournamentTeam:ModelWithName<long>
    {
        public long TournamentId { get; set; }
    }

    internal class TournamentTeamMappingProfile : Profile
    {
        public TournamentTeamMappingProfile()
        {
            CreateMap<Entities.Forecast.TournamentTeam, TournamentTeam>();
            CreateMap<TournamentTeam, Entities.Forecast.TournamentTeam>().ForMember(dst=>dst.TournamentId, opt=>opt.Ignore());
        }

    }

}
