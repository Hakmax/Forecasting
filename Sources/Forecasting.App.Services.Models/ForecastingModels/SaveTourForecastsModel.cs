using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services.Models.ForecastingModels
{
    public class SaveTourForecastsModel
    {
        public long TourId { get; set; }
        public List<TourGameForecast> Forecasts { get; set; }
    }
}
