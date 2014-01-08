using System;
using System.Windows.Data;
using Sesa.Desktop.Models;

namespace Sesa.Desktop.Common
{
    public class ExternalProductMaterialIsFloatValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as ExternalProductMaterial;
            if (item != null)
            {
                if ( item.Material != null && !item.Material.IsFloatValue)
                    return item.Value.ToString("n0");
                return item.Value;
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
