using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleOAuthMail.ValueConverters
{
    public class BoolToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                return string.Empty;

            bool valueAsBool = (bool) value;

            if (valueAsBool)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
