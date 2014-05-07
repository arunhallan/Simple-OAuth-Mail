using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace SimpleOAuthMail.ValueConverters
{
    public class ListStringValueConverter : IValueConverter
    {
        private const string Separator = ", ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof (string))
                return string.Empty;

            List<string> listOfStrings = value as List<string>;

            if (listOfStrings == null)
                return string.Empty;

            return string.Join(Separator, listOfStrings);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<string>();
        }
    }
}
