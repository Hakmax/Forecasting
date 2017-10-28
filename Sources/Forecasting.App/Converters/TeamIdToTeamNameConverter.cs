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
    public class TeamIdToTeamNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values?.Length==2)
            {
                long id = (long)values[0];
                var teams = (ObservableCollection<TournamentTeamObservable>)values[1];
                return teams.FirstOrDefault(x => x.Id == id)?.Name;
            }
            return null;
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
