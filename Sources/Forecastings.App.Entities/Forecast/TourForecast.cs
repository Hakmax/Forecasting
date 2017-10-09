using Forecasting.App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Entities.Forecast
{
    public class TourForecast:Entity<long>, IWatchedEntity
    {
        public DateTime CreationDate { get; set; }
        public long TourGameResultId { get; set; }
        public virtual TourGameResult TourGameResult { get; set; }

        public long PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int Team1Points { get; set; }
        public int Team2Points { get; set; }

        public TourGameSummaryType TourGameSummaryType { get; set; }
    }
}
