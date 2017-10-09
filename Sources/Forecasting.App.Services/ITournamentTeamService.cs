using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services
{
    public interface ITournamentTeamService
    {
        TournamentTeam Create(TournamentTeam team);
        TournamentTeam Update(TournamentTeam team);
        bool Delete(int id);

        IList<TournamentTeam> GetList(long tournamentId);
    }
}
