using Forecasting.App.VM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Forecasting.App.Converters
{
    public class GameResultTeamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] != null)
            {
                long teamId = (long)values[0];
                var teams = values[1] as ObservableCollection<TournamentTeamObservable>;
                if (teams != null)
                    return teams.FirstOrDefault(x => x.Id == teamId);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var ids = new List<object>();
                ids.Add((value as TournamentTeamObservable).Id);
                return ids.ToArray();
            }
            return null;
        }
    }
}
