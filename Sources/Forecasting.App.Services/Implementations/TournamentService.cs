using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forecasting.App.Services.Models.ForecastingModels;
using AutoMapper;
using Forecasting.App.Data.Services;
using AutoMapper.QueryableExtensions;

namespace Forecasting.App.Services.Implementations
{
    internal class TournamentService : ITournamentService
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<ITournamentDataService> _tournamentDataService;
        private readonly Lazy<IPlayerDataService> _playerDataService;
        private readonly Lazy<ITournamentTeamDataService> _tournamentTeamDataService;


        public TournamentService(Lazy<IUnitOfWork> unitOfWork, Lazy< ITournamentDataService> tournamentDataService,
            Lazy<IPlayerDataService> playerDataService, Lazy<ITournamentTeamDataService> tournamentTeamDataService)
        {
            _unitOfWork = unitOfWork;
            _tournamentDataService = tournamentDataService;
            _playerDataService = playerDataService;
            _tournamentTeamDataService = tournamentTeamDataService;
        }


        public Tournament Create(Tournament tournament)
        {
            var newTournament = Mapper.Map<Entities.Forecast.Tournament>(tournament);
            _tournamentDataService.Value.Create(newTournament);
            _unitOfWork.Value.Save();
            return Mapper.Map<Tournament>(newTournament);
        }

        public bool Delete(Tournament tournament)
        {
            var result = false;
            var itemToDelete = _tournamentDataService.Value.Get(tournament.Id);
            if(itemToDelete!=null)
            {
                _tournamentDataService.Value.Delete(itemToDelete);
                _unitOfWork.Value.Save();
                result = true;
            }
            return result;
        }

        public Tournament Update(Tournament tournament)
        {
            var dbItem = _tournamentDataService.Value.Get(tournament.Id);
            if(dbItem!=null)
            {
                Mapper.Map(tournament, dbItem);
                _tournamentDataService.Value.Update(dbItem);
                _unitOfWork.Value.Save();
            }
            return tournament;
        }

        public IList<Tournament> GetList()
        {
            var items = _tournamentDataService.Value.Query().OrderByDescending(x => x.CreationDate).ProjectTo<Tournament>().ToList();
            return items;
        }

        public Tournament GetWithDetails(long id)
        {
            var item = _tournamentDataService.Value.Query().Where(x => x.Id == id).ProjectTo<Tournament>(x=>x.Players,x=>x.TournamentTeams,x=>x.Tours).FirstOrDefault();
            return item;
        }

        public IList<Player> SavePlayers(long tournamentId, IList<Player> players)
        {
            var dbPlayers = _playerDataService.Value.Query().Where(x => x.TournamentId == tournamentId).ToList();
            foreach(var item in dbPlayers.ToList())
            {
                var player = players.FirstOrDefault(x => x.Id == item.Id);
                if (player != null)
                {
                    Mapper.Map(player, item);
                }
                else
                    _playerDataService.Value.Delete(item);
            }

            foreach(var item in players.Where(x=>x.Id==0))
            {
                var newPlayer = Mapper.Map<Entities.Forecast.Player>(item);
                newPlayer.TournamentId = tournamentId;
                _playerDataService.Value.Create(newPlayer);
            }

            _unitOfWork.Value.Save();
            return null;
        }

        public IList<TournamentTeam> SaveTournamentTeams(long tournamentId, IList<TournamentTeam> teams)
        {
            //System.ComponentModel.DataAnnotations.Validator.
            var tournaments = _tournamentTeamDataService.Value.Query().Where(x => x.TournamentId == tournamentId).ToList();
            foreach (var item in tournaments.ToList())
            {
                var teamToSave = teams.FirstOrDefault(x => x.Id == item.Id);
                if (teamToSave != null)
                {
                    Mapper.Map(teamToSave, item);
                }
                else
                    _tournamentTeamDataService.Value.Delete(item);
            }

            foreach (var item in teams.Where(x => x.Id == 0))
            {
                var newDbTeam = Mapper.Map<Entities.Forecast.TournamentTeam>(item);
                newDbTeam.TournamentId = tournamentId;
                _tournamentTeamDataService.Value.Create(newDbTeam);
            }

            _unitOfWork.Value.Save();
            return null;
        }

        public IList<Tour> SaveTours()
        {
            return null;
        }
    }
}
