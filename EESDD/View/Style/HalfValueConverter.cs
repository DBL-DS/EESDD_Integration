using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EESDD.View.Style
{
    public class HalfValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            double parentLength = (double)values[0];
            double length = (double)values[1];

            return (parentLength - length) / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
	        object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
