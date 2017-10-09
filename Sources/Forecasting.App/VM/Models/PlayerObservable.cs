using AutoMapper;
using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class PlayerObservable:ObservableModelWithName<long>
    {
    }

    internal class PlayerObservableMappingProfile:Profile
    {
        public PlayerObservableMappingProfile()
        {
            CreateMap<Player, PlayerObservable>();
            CreateMap<PlayerObservable, Player>();
        }
    }
}
