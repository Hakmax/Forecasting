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

        public MainVM()
        {
            if (IsInDesignMode)
            {
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

            Tournaments = new ObservableCollection<TournamentObservable>();

            /*Tournaments.Add(new TournamentObservable
            {
                Name = "123"
            });*/
            LoadTournaments();
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
                TourGameResults = new ObservableCollection<TourGameResultObservable>(new List<Models.TourGameResultObservable>(
                    new TourGameResultObservable[]
                    {
                        new TourGameResultObservable
                        {
                            TournamentTeam1Id=1,
                            TournamentTeam2Id=2,
                            TournamentTeam1Points=1,
                            TournamentTeam2Points=2
                        }
                    }
                    ))
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
            _tournamentService.Value.SavePlayers(SelectedTournament.Id, Mapper.Map<IList<Player>>(SelectedTournament.Players));
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
                Teams = Mapper.Map<ObservableCollection<TournamentTeamObservable>>(res.Result.TournamentTeams);
            });
        }
    }
}
