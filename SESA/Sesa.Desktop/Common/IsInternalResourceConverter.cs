using System;
using System.Windows.Data;

namespace Sesa.Desktop.Common
{
    public class IsInternalResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ResourceHelper.GetResource(((bool)value) ? "Internal" : "External");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
