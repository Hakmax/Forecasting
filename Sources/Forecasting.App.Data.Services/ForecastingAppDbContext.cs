using Forecasting.App.Data.Services.Conventions;
using Forecasting.App.Entities.Forecast;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services
{
    public class ForecastingAppDbContext : DbContext
    {
        public const string ConnectionStringName = "DbConnection";

        public IDbSet<Player> Players { get; set; }
        public IDbSet<Tour> Tours { get; set; }
        public IDbSet<Tournament> Tournaments { get; set; }
        public IDbSet<TourGameForecast> TourGameForecasts { get; set; }
        public IDbSet<TourGameResult> TourGameResults { get; set; }
        public IDbSet<TournamentTeam> TournamentTeams { get; set; }


        public ForecastingAppDbContext() : base(ConnectionStringName)
        {
            this.Database.Log = str =>
            {
             //   System.Diagnostics.Trace.WriteLine(str);
            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<ForecastingAppDbContext>(modelBuilder, true));

            //modelBuilder.Entity<Player>().Property(x => x.TournamentId).HasColumnAnnotation("XI_TournamentId", "XI_TournamentId");

            //modelBuilder.Conventions.AddBefore<System.Data.Entity.ModelConfiguration.Conventions.ForeignKeyIndexConvention>(new ForeignKeyNamingConvention());
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
