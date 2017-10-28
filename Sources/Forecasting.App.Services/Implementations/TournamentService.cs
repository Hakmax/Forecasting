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
        private readonly Lazy<ITourDataService> _tourDataService;
        private readonly Lazy<ITourGameResultDataService> _tourGameResultDataService;
        private readonly Lazy<ITourGameForecastDataService> _tourForecastDataService;


        public TournamentService(Lazy<IUnitOfWork> unitOfWork, Lazy<ITournamentDataService> tournamentDataService,
            Lazy<IPlayerDataService> playerDataService, Lazy<ITournamentTeamDataService> tournamentTeamDataService,
            Lazy<ITourDataService> tourDataService, Lazy<ITourGameResultDataService> tourGameResultDataService,
            Lazy<ITourGameForecastDataService> tourForecastDataService)
        {
            _unitOfWork = unitOfWork;
            _tournamentDataService = tournamentDataService;
            _playerDataService = playerDataService;
            _tournamentTeamDataService = tournamentTeamDataService;
            _tourDataService = tourDataService;
            _tourGameResultDataService = tourGameResultDataService;
            _tourForecastDataService = tourForecastDataService;
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
            if (itemToDelete != null)
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
            if (dbItem != null)
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
            var item = _tournamentDataService.Value.Query().Where(x => x.Id == id).ProjectTo<Tournament>(x => x.Players, x => x.TournamentTeams, x => x.Tours).FirstOrDefault();
            return item;
        }

        public IList<Player> SavePlayers(long tournamentId, IList<Player> players)
        {
            List<Player> resultPlayers = null;
            var dbPlayers = _playerDataService.Value.Query().Where(x => x.TournamentId == tournamentId).ToList();
            var newPlayers = new List<Entities.Forecast.Player>();
            foreach (var item in dbPlayers.ToList())
            {
                var player = players.FirstOrDefault(x => x.Id == item.Id);
                if (player != null)
                {
                    Mapper.Map(player, item);
                }
                else
                    _playerDataService.Value.Delete(item);
            }

            resultPlayers= Mapper.Map<List<Player>>(dbPlayers);
            foreach (var item in players.Where(x => x.Id == 0))
            {
                var newPlayer = Mapper.Map<Entities.Forecast.Player>(item);
                newPlayer.TournamentId = tournamentId;
                _playerDataService.Value.Create(newPlayer);
                newPlayers.Add(newPlayer);
            }
            _unitOfWork.Value.Save();
            foreach (var item in newPlayers)
                resultPlayers.Add(Mapper.Map<Player>(item));

            return resultPlayers;
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

        public Tour SaveTour(Tour tour)
        {
            Tour resultTour = null;
            if (tour.Id == 0)
            {
                var newTour = Mapper.Map<Entities.Forecast.Tour>(tour);
                _tourDataService.Value.Create(newTour);
                _unitOfWork.Value.Save();
                resultTour = Mapper.Map<Tour>(newTour);
            }
            else
            {
                var dbTour = _tourDataService.Value.Get(tour.Id, x => x.TourGameResults);
                if (dbTour != null)
                {
                    dbTour.TourNumber = tour.TourNumber;
                    if (dbTour.TourGameResults.Count > 0)
                    {
                        foreach (var item in dbTour.TourGameResults.ToList())
                        {
                            var gameResFromUser = tour.TourGameResults.FirstOrDefault(x => x.Id == item.Id);
                            if (gameResFromUser != null)
                            {
                                Mapper.Map(gameResFromUser, item);
                                item.TourId = dbTour.Id;
                                _tourGameResultDataService.Value.Update(item);
                            }
                            else
                            {
                                _tourGameResultDataService.Value.Delete(item);
                            }
                        }
                    }
                    foreach (var item in tour.TourGameResults.Where(x => x.Id == 0))
                    {
                        item.TourId = tour.Id;
                        dbTour.TourGameResults.Add(Mapper.Map<Entities.Forecast.TourGameResult>(item));
                        //_tourGameResultDataService.Value.Create(Mapper.Map<Entities.Forecast.TourGameResult>(item));
                    }
                }
                _unitOfWork.Value.Save();
                resultTour = Mapper.Map<Tour>(dbTour);
            }

            return resultTour;
        }

        

        public Tour SaveTourForecast(SaveTourForecastsModel tourForecastsModel)
        {
            var gameResults = _tourGameResultDataService.Value.Query(x => x.TourGameForecasts).Where(x => x.TourId == tourForecastsModel.TourId).ToList();
            Tour result = null;
            if (gameResults.Any())
            {
                foreach (var item in gameResults)
                {
                    foreach (var gameResForecast in item.TourGameForecasts.ToList())
                    {
                        var userFCast = tourForecastsModel.Forecasts.FirstOrDefault(x => x.Id == gameResForecast.Id);
                        if (userFCast != null)
                        {
                            Mapper.Map(userFCast, gameResForecast);
                        }
                        else
                        {
                            _tourForecastDataService.Value.Delete(gameResForecast);
                        }
                    }
                    foreach (var newForecast in tourForecastsModel.Forecasts.Where(x => x.Id == 0 && x.TourGameResultId == item.Id))
                    {
                        _tourForecastDataService.Value.Create(Mapper.Map<Entities.Forecast.TourGameForecast>(newForecast));
                    }
                    if (result == null)
                        result = Mapper.Map<Tour>(gameResults[0].Tour);
                }
                _unitOfWork.Value.Save();
            }
            return result;
        }

        public IList<Tour> FilterToursForecasts(ToursForecastsFilterRequest request)
        {
            var toursQuery = _tourDataService.Value.Query().Where(x => x.TournamentId == request.TournamentId);
            if (request.TourNumbers?.Any() == true)
                toursQuery = toursQuery.Where(x => request.TourNumbers.Contains(x.TourNumber));
            var tours = toursQuery.ProjectTo<Tour>().ToList();

            return tours;
        }
    }
}
