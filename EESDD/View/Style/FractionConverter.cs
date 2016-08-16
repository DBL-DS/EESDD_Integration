using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace EESDD.View.Style
{
    class FractionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
                return new Thickness(0, 0, 0, 0);

            double width = (double)values[0];
            double height = (double)values[1];
            double fraction = (double)values[2];

            double F_width = width / fraction;
            double F_height = height / fraction;

            string type = (string)values[3];

            switch (type)
            {
                case "Number":
                    return F_width < F_height ? F_width : F_height;
                case "DropShadow":
                    var shadow = new DropShadowEffect();
                    shadow.ShadowDepth = 0;
                    shadow.BlurRadius = F_width < F_height ? F_width : F_height;
                    return shadow;
                case "CornerRadius":
                    return new CornerRadius(F_width < F_height ? F_width : F_height);
                case "Thickness":
                    return new Thickness(F_width, F_height, F_width, F_height);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
