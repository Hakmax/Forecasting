﻿using Forecasting.App.Services.Models.ForecastingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Services
{
    public interface IPlayerService
    {
        IList<Player> GetList(long tournamentId);
    }
}
