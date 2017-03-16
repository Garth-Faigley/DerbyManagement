using DerbyManagement.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace DerbyManagement.App.Converter
{
    class DivisionToBooleanConverter :  IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is Division))
                throw new ArgumentException("DivisionToBooleanConverter- First arg must be Division");
            if (!(values[1] is ObservableCollection<Division>))
                throw new ArgumentException("DivisionToBooleanConverter- Second arg must be RacerDivisions.");

            var division = (Division)values[0];
            var racerDivisions = (ObservableCollection<Division>)values[1];

            return racerDivisions.Contains(division);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
