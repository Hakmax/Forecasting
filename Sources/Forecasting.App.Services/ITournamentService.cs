using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services
{
    public interface ITournamentService
    {
        Tournament Create(Tournament tournament);
        Tournament Update(Tournament tournament);
        bool Delete(Tournament tournament);

        IList<Tournament> GetList();

        Tournament GetWithDetails(long id);
        IList<Player> SavePlayers(long tournamentId, IList<Player> players);
        IList<TournamentTeam> SaveTournamentTeams(long tournamentId, IList<TournamentTeam> teams);
        Tour SaveTour(Tour tour);
        Tour SaveTourForecast(SaveTourForecastsModel tourForecastsModel);
        IList<Tour> FilterToursForecasts(ToursForecastsFilterRequest request);
    }
}
