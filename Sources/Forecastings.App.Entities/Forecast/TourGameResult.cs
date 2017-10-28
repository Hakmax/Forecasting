using Forecasting.App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Entities.Forecast
{
    public class TourGameResult:Entity<long>
    {
        public long TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public long TournamentTeam1Id { get; set; }
        public virtual TournamentTeam TournamentTeam1 { get; set; }

        public long TournamentTeam2Id { get; set; }
        public virtual TournamentTeam TournamentTeam2 { get; set; }

        public int TournamentTeam1Points { get; set; }
        public int TournamentTeam2Points { get; set; }

        public TourGameSummaryType TourGameSummaryType { get; set; }

        public virtual ICollection<TourGameForecast> TourGameForecasts { get; set; }

    }
}
