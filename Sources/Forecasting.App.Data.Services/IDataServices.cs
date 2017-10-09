using Forecasting.App.Entities.Forecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services
{
    public interface IPlayerDataService:IDataService<Player, long>
    {
    }

    public interface ITourDataService : IDataService<Tour, long>
    {
    }

    public interface ITourForecastDataService : IDataService<TourForecast, long>
    {
    }
    public interface ITourGameResultDataService : IDataService<TourGameResult, long>
    {
    }
    public interface ITournamentDataService : IDataService<Tournament, long>
    {
    }
    public interface ITournamentTeamDataService : IDataService<TournamentTeam, long>
    {
    }
}
