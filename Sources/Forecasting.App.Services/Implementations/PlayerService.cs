using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forecasting.App.Services.Models.ForecastingModels;
using Forecasting.App.Data.Services;
using AutoMapper.QueryableExtensions;

namespace Forecasting.App.Services.Implementations
{
    internal class PlayerService : IPlayerService
    {
        private readonly Lazy<IPlayerDataService> _playerDataService;

        public PlayerService(Lazy<IPlayerDataService> playerDataService)
        {
            _playerDataService = playerDataService;
        }
        public IList<Player> GetList(long tournamentId)
        {
            var players = _playerDataService.Value.Query().Where(x => x.TournamentId == tournamentId).ProjectTo<Player>().ToList();
            return players;
        }
    }
}
