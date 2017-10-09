using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Entities.Forecast
{
    public class Tour:Entity<long>, IWatchedEntity
    {
        public int TourNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public long TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<TourGameResult> TourGameResults { get; set; }
    }
}
