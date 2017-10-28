using Forecasting.App.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forecasting.App.VM.Models;
using System.Collections.ObjectModel;
using System.Threading;
using Forecasting.App.Services.Models.ForecastingModels;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
using Forecasting.App.VM.Models.Messages;

namespace Forecasting.App.VM
{
    public class MainVM : ViewModelBase
    {
        private readonly Lazy<ITournamentService> _tournamentService;
        private readonly Lazy<ITournamentTeamService> _tournamentTeamService;
        private readonly Lazy<IPlayerService> _playerService;


        private readonly TaskScheduler _currentTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private ObservableCollection<TournamentObservable> _tournaments;
        public ObservableCollection<TournamentObservable> Tournaments
        {
            get
            {
                return _tournaments;
            }
            set
            {
                Set(() => Tournaments, ref _tournaments, value);
            }
        }

        private TournamentObservable _selectedTournament;
        public TournamentObservable SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {

                TournamentObservable cpy = null;
                if (value != null)
                    cpy = (TournamentObservable)value.Clone();
                Set(() => SelectedTournament, ref _selectedTournament, cpy);
                if (SelectedTournament?.Id > 0)
                    LoadTournamentDetails();
            }
        }

        private ObservableCollection<TournamentTeamObservable> _teams;
        public ObservableCollection<TournamentTeamObservable> Teams
        {
            get
            {
                return _teams;
            }
            set
            {
                Set(() => Teams, ref _teams, value);
            }
        }

        private ObservableCollection<TourNumber> _tourNumbers;
        public ObservableCollection<TourNumber> TourNumbers
        {
            get
            {
                return _tourNumbers;
            }
            set
            {
                Set(() => TourNumbers, ref _tourNumbers, value);
            }
        }
        public RelayCommand CreateNewTournamentCommand { get; private set; }
        public RelayCommand SaveTournamentCommand { get; private set; }

        public RelayCommand CreateNewTeamCommand { get; private set; }
        public RelayCommand CreateNewPlayerCommand { get; private set; }

        public RelayCommand SaveTeamsCommand { get; private set; }
        public RelayCommand<TournamentTeamObservable> RemoveTeamCommand { get; private set; }

        public RelayCommand SavePlayersCommand { get; private set; }
        public RelayCommand<PlayerObservable> RemovePlayerCommand { get; private set; }
        public RelayCommand CancelPlayersChangesCommand { get; private set; }
        public RelayCommand CancelTeamsChangesCommand { get; private set; }
        public RelayCommand CreateTourCommand { get; private set; }
        public RelayCommand<TourObservable> RemoveTourCommand { get; private set; }
        public RelayCommand<TourObservable> AddGameResultCommand { get; private set; }
        public RelayCommand<Tuple<TourObservable, TourGameResultObservable>> RemoveGameResultCommand { get; private set; }
        public RelayCommand<TourObservable> SaveTourCommand { get; private set; }
        public RelayCommand<TourExtendedObservable> SaveForecastsTourCommand { get; private set; }
        public RelayCommand ApplyToursForecastsFilterCommand { get; private set; }

        public MainVM()
        {
            if (IsInDesignMode)
            {
                Tournaments = new ObservableCollection<TournamentObservable>();
                Tournaments.Add(new TournamentObservable { Name = "1" });
                Tournaments.Add(new TournamentObservable { Name = "2" });
                SelectedTournament = new TournamentObservable { Name = "RFPL 2016-2017" };
                SelectedTournament.Players = new ObservableCollection<PlayerObservable>();
                SelectedTournament.Players.Add(new PlayerObservable { Name = "player1" });
                SelectedTournament.Players.Add(new PlayerObservable { Name = "player2" });

                SelectedTournament.TournamentTeams = new ObservableCollection<TournamentTeamObservable>();
                SelectedTournament.TournamentTeams.Add(new TournamentTeamObservable { Name = "FC Zenit" });
                SelectedTournament.TournamentTeams.Add(new TournamentTeamObservable { Name = "CSKA" });

                SelectedTournament.Tours = new ObservableCollection<TourObservable>();
                SelectedTournament.Tours.Add(new TourObservable
                {
                    TourNumber = 1,
                    TourGameResults = new ObservableCollection<TourGameResultObservable>(new TourGameResultObservable[]
                    {
                        new TourGameResultObservable
                        {
                            TournamentTeam1Id=2,
                            TournamentTeam2Id=3,
                            TournamentTeam1Points=1,
                            TournamentTeam2Points=3
                        }
                    })
                });
                SelectedTournament.Tours.Add(new TourObservable
                {
                    TourNumber = 2
                });

                Teams = new ObservableCollection<TournamentTeamObservable>();
                Teams.Add(new TournamentTeamObservable
                {
                    Name = "123"
                });

                var tourForecasts = new ObservableCollection<TourExtendedObservable>();
                tourForecasts.Add(new TourExtendedObservable
                {
                    TourNumber = 1,
                    Id = 1,
                    TournamentId = 1
                });
                var t = tourForecasts[0];
                t.Players = new List<PlayerExtended>();
                var pl1 = new PlayerExtended
                {
                    Name = "User1",
                    Forecasts = new ObservableCollection<PlayerTourForecast>()
                };
                pl1.Forecasts.Add(new PlayerTourForecast { ForecastEnabled = true, TournamentTeam1Name = "Spartak", TournamentTeam2Name = "Zenit", TournamentTeam1Points = 1 });
                pl1.Forecasts.Add(new PlayerTourForecast { TournamentTeam1Name = "Dinamo", TournamentTeam2Name = "Amkar", TournamentTeam1Points = 3 });

                t.Players.Add(pl1);
                t.Players.Add(new PlayerExtended
                {
                    Name = "User2"
                });
                t.Players.Add(new PlayerExtended
                {
                    Name = "User3"
                });

                SelectedTournament.TourForecasts = tourForecasts;
            }
        }



        public MainVM(Lazy<ITournamentService> tournamentService, Lazy<ITournamentTeamService> tournamentTeamService,
             Lazy<IPlayerService> playerService)
        {
            _tournamentService = tournamentService;
            _tournamentTeamService = tournamentTeamService;
            _playerService = playerService;

            CreateNewTournamentCommand = new RelayCommand(CreateNewTournamentCommand_Handler);
            SaveTournamentCommand = new RelayCommand(SaveCurrentTournamentCommand_Handler);
            CreateNewTeamCommand = new RelayCommand(CreateNewTeamCommand_Handler);
            CreateNewPlayerCommand = new RelayCommand(CreateNewPlayerCommand_Handler);
            SaveTeamsCommand = new RelayCommand(SaveTeamsCommand_Handler);
            RemoveTeamCommand = new RelayCommand<TournamentTeamObservable>(RemoveTeamCommand_Handler);
            SavePlayersCommand = new RelayCommand(SavePlayersCommand_Handler);
            RemovePlayerCommand = new RelayCommand<PlayerObservable>(RemovePlayerCommand_Handler);
            CancelPlayersChangesCommand = new RelayCommand(CancelPlayersChangesCommand_Handler);
            CancelTeamsChangesCommand = new RelayCommand(CancelTeamsChangesCommand_Handler);
            CreateTourCommand = new RelayCommand(CreateTourCommand_Handler);
            RemoveTourCommand = new RelayCommand<TourObservable>(RemoveTourCommand_Handler);
            AddGameResultCommand = new RelayCommand<TourObservable>(AddGameResultCommand_Handler);
            RemoveGameResultCommand = new RelayCommand<Tuple<TourObservable, TourGameResultObservable>>(RemoveGameResultCommand_Handler);
            SaveTourCommand = new RelayCommand<TourObservable>(SaveTourCommand_Handler);
            SaveForecastsTourCommand = new RelayCommand<TourExtendedObservable>(SaveForecastsTourCommand_Handler);
            ApplyToursForecastsFilterCommand = new RelayCommand(ApplyToursForecastsFilterCommand_Handler);


            Tournaments = new ObservableCollection<TournamentObservable>();
            //TourNumbers = new ObservableCollection<TourNumber>();
            /*Tournaments.Add(new TournamentObservable
            {
                Name = "123"
            });*/

            MessengerInstance.Register<TourFilterChangedMessage>(this, ToursFilterChanged_Handler);
            LoadTournaments();
        }

        private void ApplyToursForecastsFilterCommand_Handler()
        {
            var req = new ToursForecastsFilterRequest();
            req.TournamentId = SelectedTournament.Id;
            req.TourNumbers = TourNumbers.Where(x => x.TourNumberValue != Constants.AllToursSelector && x.Checked).Select(x=>x.TourNumberValue).ToList();
            var toursByFilter= _tournamentService.Value.FilterToursForecasts(req);
            var tourForecasts = new ObservableCollection<TourExtendedObservable>();
            foreach (var item in toursByFilter)
            {
                var tourExt = GetTourExtended(item, SelectedTournament.Players, SelectedTournament.TournamentTeams);
                if (tourExt != null)
                    tourForecasts.Add(tourExt);
            }
            SelectedTournament.TourForecasts = tourForecasts;
        }

        private void ToursFilterChanged_Handler(TourFilterChangedMessage message)
        {
            var checkedItems = TourNumbers.Where(x => x.Checked).ToList();
            if (checkedItems.Count > 0)
            {
                if (checkedItems.Count > 1)
                {
                    var allItemChecked = checkedItems.FirstOrDefault(x => x.TourNumberValue == Constants.AllToursSelector);
                    if (allItemChecked != null)
                        allItemChecked.Checked = false;
                }
            }
            else
            {
                TourNumbers.First(x => x.TourNumberValue == Constants.AllToursSelector).Checked = true;
            }
        }

        private void SaveForecastsTourCommand_Handler(TourExtendedObservable tourForecast)
        {
            var saveForecastsModel = new SaveTourForecastsModel();
            saveForecastsModel.TourId = tourForecast.Id;
            saveForecastsModel.Forecasts = new List<TourGameForecast>();
            foreach (var item in tourForecast.Players)
            {
                foreach (var f in item.Forecasts)
                {
                    if (f.ForecastEnabled && f.TournamentTeam1Points.HasValue && f.TournamentTeam2Points.HasValue)
                    {
                        var newFCast = Mapper.Map<TourGameForecast>(f, opts => opts.Items.Add("PlayerId", item.Id));
                        saveForecastsModel.Forecasts.Add(newFCast);
                    }
                }
            }
            var tour = _tournamentService.Value.SaveTourForecast(saveForecastsModel);
            if (tour != null)
            {
                var index = SelectedTournament.TourForecasts.IndexOf(tourForecast);
                if (index > -1)
                {
                    var tourExt = GetTourExtended(tour, SelectedTournament.Players, SelectedTournament.TournamentTeams);
                    SelectedTournament.TourForecasts[index] = tourExt;
                }
            }
        }

        private void SaveTourCommand_Handler(TourObservable tour)
        {
            var result = _tournamentService.Value.SaveTour(Mapper.Map<Tour>(tour));
            if (result != null)
            {
                var tourIndex = SelectedTournament.Tours.IndexOf(tour);
                if (tourIndex > -1)
                    SelectedTournament.Tours[tourIndex] = Mapper.Map<TourObservable>(result);
            }
        }

        private void RemoveGameResultCommand_Handler(Tuple<TourObservable, TourGameResultObservable> parameters)
        {
            var tour = parameters.Item1;
            tour.TourGameResults.Remove(parameters.Item2);
        }


        private void AddGameResultCommand_Handler(TourObservable tour)
        {
            tour.TourGameResults.Add(new TourGameResultObservable());
        }

        private void RemoveTourCommand_Handler(TourObservable tour)
        {
            SelectedTournament.Tours.Remove(tour);

        }

        private void CreateTourCommand_Handler()
        {
            if (SelectedTournament.Tours == null)
                SelectedTournament.Tours = new ObservableCollection<TourObservable>();
            SelectedTournament.Tours.Add(new TourObservable
            {
                TourNumber = SelectedTournament.Tours.Count + 1,
                TourGameResults = new ObservableCollection<TourGameResultObservable>(),
                TournamentId = SelectedTournament.Id
            });
        }

        private void CancelTeamsChangesCommand_Handler()
        {
            SelectedTournament.TournamentTeams = Mapper.Map<ObservableCollection<TournamentTeamObservable>>(_tournamentTeamService.Value.GetList(SelectedTournament.Id));
        }

        private void CancelPlayersChangesCommand_Handler()
        {
            SelectedTournament.Players = Mapper.Map<ObservableCollection<PlayerObservable>>(_playerService.Value.GetList(SelectedTournament.Id));
        }

        private void RemovePlayerCommand_Handler(PlayerObservable player)
        {
            SelectedTournament.Players.Remove(player);
        }

        private void SavePlayersCommand_Handler()
        {
            var toSave = Mapper.Map<IList<Player>>(SelectedTournament.Players);
            foreach (var item in toSave)
                item.TournamentId = SelectedTournament.Id;
            var players = _tournamentService.Value.SavePlayers(SelectedTournament.Id, toSave);
            SelectedTournament.Players = Mapper.Map<ObservableCollection<PlayerObservable>>(players);
        }

        private void RemoveTeamCommand_Handler(TournamentTeamObservable team)
        {
            SelectedTournament.TournamentTeams.Remove(team);
        }

        private void SaveTeamsCommand_Handler()
        {
            _tournamentService.Value.SaveTournamentTeams(SelectedTournament.Id, Mapper.Map<List<TournamentTeam>>(SelectedTournament.TournamentTeams));
        }

        private void CreateNewPlayerCommand_Handler()
        {
            if (SelectedTournament.Players == null)
                SelectedTournament.Players = new ObservableCollection<PlayerObservable>();
            SelectedTournament.Players.Add(new PlayerObservable
            {
                Name = "Новый участник"
            });
        }

        private void CreateNewTeamCommand_Handler()
        {
            if (SelectedTournament.TournamentTeams == null)
                SelectedTournament.TournamentTeams = new ObservableCollection<TournamentTeamObservable>();
            SelectedTournament.TournamentTeams.Add(new TournamentTeamObservable
            {
                Name = "Новая команда"
            });
        }

        private void CreateNewTournamentCommand_Handler()
        {
            SelectedTournament = new TournamentObservable
            {
                Name = "Новый турнир"
            };
        }

        private void SaveCurrentTournamentCommand_Handler()
        {
            var tournToSave = Mapper.Map<Tournament>(SelectedTournament);
            if (tournToSave.Id == 0)
            {
                _tournamentService.Value.Create(tournToSave);
            }
            else
            {
                var res = _tournamentService.Value.Update(tournToSave);
                if (res != null)
                {
                    var observableTourn = Mapper.Map<TournamentObservable>(res);
                    var listItem = Tournaments.FirstOrDefault(x => x.Id == res.Id);
                    if (listItem != null)
                    {
                        Tournaments[Tournaments.IndexOf(listItem)] = observableTourn;
                        SelectedTournament = observableTourn;
                        //LoadTournamentDetails();
                        RaisePropertyChanged(() => Tournaments);
                    }
                }
            }
        }

        public void LoadTournaments()
        {
            Task.Factory.StartNew(() =>
            {
                var tournamentsList = _tournamentService.Value.GetList();
                return tournamentsList;
            }, CancellationToken.None, TaskCreationOptions.None, _currentTaskScheduler).ContinueWith(res =>
            {
                Tournaments = Mapper.Map<ObservableCollection<TournamentObservable>>(res.Result);
            });
        }

        public void LoadTournamentDetails()
        {
            Task.Factory.StartNew(() =>
            {
                var details = _tournamentService.Value.GetWithDetails(SelectedTournament.Id);
                return details;
            }, CancellationToken.None, TaskCreationOptions.None, _currentTaskScheduler).ContinueWith(res =>
            {
                SelectedTournament.Name = res.Result.Name;
                SelectedTournament.Players = Mapper.Map<ObservableCollection<PlayerObservable>>(res.Result.Players);
                SelectedTournament.TournamentTeams = Mapper.Map<ObservableCollection<TournamentTeamObservable>>(res.Result.TournamentTeams);
                SelectedTournament.Tours = Mapper.Map<ObservableCollection<TourObservable>>(res.Result.Tours);
                Teams = Mapper.Map<ObservableCollection<TournamentTeamObservable>>(res.Result.TournamentTeams);
                var tourForecasts = new ObservableCollection<TourExtendedObservable>();
                foreach (var item in res.Result.Tours)
                {
                    var tourExt = GetTourExtended(item, SelectedTournament.Players, SelectedTournament.TournamentTeams);
                    if (tourExt != null)
                        tourForecasts.Add(tourExt);
                }
                SelectedTournament.TourForecasts = tourForecasts;
                InitTourNumbers();
            });
        }

        private void InitTourNumbers()
        {
            var nums = new List<TourNumber>(new[] { new TourNumber(Constants.AllToursSelector, "Все", true) });
            foreach (var item in SelectedTournament.Tours)
                nums.Add(new TourNumber(item.TourNumber));
            TourNumbers = new ObservableCollection<TourNumber>(nums);
        }

        private TourExtendedObservable GetTourExtended(Tour tour, IList<PlayerObservable> players, IList<TournamentTeamObservable> teams)
        {
            TourExtendedObservable result = null;
            if (tour.TourGameResults.Any())
            {
                result = Mapper.Map<TourExtendedObservable>(tour);
                result.Players = new List<PlayerExtended>();
                foreach (var player in players)
                {
                    var newPl = new PlayerExtended();
                    newPl.Name = player.Name;
                    newPl.Id = player.Id;
                    newPl.Forecasts = new ObservableCollection<PlayerTourForecast>();
                    foreach (var gr in tour.TourGameResults)
                    {
                        var forecast = Mapper.Map<PlayerTourForecast>(gr);

                        forecast.TournamentTeam1Name = teams.FirstOrDefault(x => x.Id == gr.TournamentTeam1Id)?.Name;
                        forecast.TournamentTeam2Name = teams.FirstOrDefault(x => x.Id == gr.TournamentTeam2Id)?.Name;
                        if (gr.TourGameForecasts.Any())
                        {
                            var userForecast = gr.TourGameForecasts.FirstOrDefault(x => x.PlayerId == newPl.Id);
                            if (userForecast != null)
                            {
                                forecast.Id = userForecast.Id;
                                forecast.ForecastEnabled = true;
                                forecast.TournamentTeam1Points = userForecast.Team1Points;
                                forecast.TournamentTeam2Points = userForecast.Team2Points;
                            }
                        }
                        newPl.Forecasts.Add(forecast);
                    }
                    result.Players.Add(newPl);
                }
            }
            return result;
        }
    }
}
