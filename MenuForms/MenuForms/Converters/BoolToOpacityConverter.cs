using System;
using System.Globalization;
using Xamarin.Forms;

namespace MenuForms.Converters
{
    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool))
            {
                if ((bool)value)
                {
                    return 0.8;
                }
                else
                    return 0;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

