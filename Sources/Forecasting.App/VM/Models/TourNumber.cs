using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.VM.Models
{
    public class TourNumber : GalaSoft.MvvmLight.ObservableObject
    {
        public int TourNumberValue { get; set; }
        public string TourNumberDisplay { get; set; }

        private bool _checked;
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                Set(() => Checked, ref _checked, value);
            }
        }
        public TourNumber(int tourNumber, string tourNumberDisplay = null, bool? checkedVal = null)
        {
            TourNumberValue = tourNumber;
            if (!string.IsNullOrWhiteSpace(tourNumberDisplay))
                TourNumberDisplay = tourNumberDisplay;
            else
                TourNumberDisplay = tourNumber.ToString();
            if (checkedVal.HasValue)
                Checked = checkedVal.Value;
        }
    }
}
