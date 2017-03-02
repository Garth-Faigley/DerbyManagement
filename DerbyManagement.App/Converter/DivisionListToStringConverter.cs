using DerbyManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace DerbyManagement.App.Converter
{
    class DivisionListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value.GetType() == typeof(List<Division>)))
                throw new ArgumentException("Value must be of type List<Division>");

            var output = new StringBuilder();

            foreach (Division division in (List<Division>)value)
            {
                if (output.Length > 0)
                    output.Append(",");
                output.Append(division.Name);
            }

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }




    }
}
