using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Entities.Forecast
{
    public class TournamentTeam:EntityWithName<long>, IWatchedEntity
    {
        public DateTime CreationDate { get; set; }

        public long TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
