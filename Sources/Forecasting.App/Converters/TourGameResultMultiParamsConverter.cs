using Forecasting.App.VM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Forecasting.App.Converters
{
    public class TourGameResultMultiParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                var tour = values[0] as TourObservable;
                var gameRes = values[1] as TourGameResultObservable;
                if (tour != null && gameRes != null)
                    return new Tuple<TourObservable, TourGameResultObservable>((TourObservable)values[0], (TourGameResultObservable)values[1]);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
