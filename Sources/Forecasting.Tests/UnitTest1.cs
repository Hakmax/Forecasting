using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forecasting.App.Data.Services;
using System.Data;
using System.Linq;
using Autofac;
using Forecasting.App.Entities.Forecast;
using Forecasting.App.Services;

namespace Forecasting.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            DependencyConfig.Configure();
        }

        [TestMethod]
        public void CreateTournament()
        {
            using (var scope = DependencyConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITournamentService>();
                var t = service.GetWithDetails(2);
                var result = service.Create(new App.Services.Models.ForecastingModels.Tournament { Name = $"РФПЛ 17-18 {DateTime.Now.ToShortTimeString()}" });
            }
        }


        [TestMethod]
        public void GetTournaments()
        {
            using (var scope = DependencyConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITournamentService>();
                var items = service.GetList();
            }
        }

        [TestMethod]
        public void UpdateTournament()
        {
            using (var scope = DependencyConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITournamentService>();
                var items = service.GetList();
                var tourn = items.First();
                tourn.Name = "new name";
                tourn = service.Update(tourn);

                var updatedItems = service.GetList();
            }
        }

        [TestMethod]
        public void DeleteTournament()
        {
            using (var scope = DependencyConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITournamentService>();
                var items = service.GetList();
                service.Delete(items.First());

                var afterDelete = service.GetList();
            }
        }

        [TestMethod]
        public void CreateTeam()
        {
            using (var scope = DependencyConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITournamentTeamService>();
                var newTeam= service.Create(new App.Services.Models.ForecastingModels.TournamentTeam
                {
                    Name = "FC Zenit",
                    TournamentId = 1
                });
            }
        }

        [TestMethod]
        public void GetTeams()
        {
            using (var scope = DependencyConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITournamentTeamService>();
                var teams = service.GetList();
            }
        }

    }
}
