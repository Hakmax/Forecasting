using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class Player:ModelWithName<long>
    {
        public long TournamentId { get; set; }
    }

    internal class PlayerMappingProfile:Profile
    {
        public PlayerMappingProfile()
        {
            CreateMap<Entities.Forecast.Player, Player>();
            CreateMap<Player, Entities.Forecast.Player>();
        }
    }
}
