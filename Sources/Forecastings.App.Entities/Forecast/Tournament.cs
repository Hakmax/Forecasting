using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Entities.Forecast
{
    public class Tournament:EntityWithName<long>, IWatchedEntity
    {
        public DateTime CreationDate { get; set; }
        public virtual ICollection<TournamentTeam> TournamentTeams { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
