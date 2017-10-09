using Forecasting.App.Entities.Forecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services.Implementations
{
    internal class PlayerDataService : DataService<Player, long>, IPlayerDataService
    {
        public PlayerDataService(ForecastingAppDbContext context) : base(context)
        {
        }

        public override void Create(Player entity)
        {
            if (entity.CreationDate == DateTime.MinValue)
                entity.CreationDate = DateTime.Now;
            base.Create(entity);
        }
    }

    internal class TourDataService : DataService<Tour, long>, ITourDataService
    {
        public TourDataService(ForecastingAppDbContext context) : base(context)
        {
        }

        public override void Create(Tour entity)
        {
            if (entity.CreationDate == DateTime.MinValue)
                entity.CreationDate = DateTime.Now;
            base.Create(entity);
        }
    }
    internal class TourForecastDataService : DataService<TourForecast, long>, ITourForecastDataService
    {
        public TourForecastDataService(ForecastingAppDbContext context) : base(context)
        {
        }

        public override void Create(TourForecast entity)
        {
            if (entity.CreationDate == DateTime.MinValue)
                entity.CreationDate = DateTime.Now;
            base.Create(entity);
        }
    }
    internal class TourGameResultDataService : DataService<TourGameResult, long>, ITourGameResultDataService
    {
        public TourGameResultDataService(ForecastingAppDbContext context) : base(context)
        {
        }
    }
    internal class TournamentTeamDataService : DataService<TournamentTeam, long>, ITournamentTeamDataService
    {
        public TournamentTeamDataService(ForecastingAppDbContext context) : base(context)
        {
        }

        public override void Create(TournamentTeam entity)
        {
            if (entity.CreationDate == DateTime.MinValue)
                entity.CreationDate = DateTime.Now;
            base.Create(entity);
        }
    }

    internal class TournamentDataService : DataService<Tournament, long>, ITournamentDataService
    {
        public TournamentDataService(ForecastingAppDbContext context) : base(context)
        {
        }

        public override void Create(Tournament entity)
        {
            if (entity.CreationDate == DateTime.MinValue)
                entity.CreationDate = DateTime.Now;
            base.Create(entity);
        }
    }
}
