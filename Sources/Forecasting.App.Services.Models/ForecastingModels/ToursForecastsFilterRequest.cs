using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class ToursForecastsFilterRequest
    {
        public long TournamentId { get; set; }
        public List<int> TourNumbers { get; set; }
    }
}
