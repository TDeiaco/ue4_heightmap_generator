using System;
using System.Globalization;
using System.Windows.Data;

namespace UnrealTiledHeightmapGen
{
    public class NumericTextBoxMask : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var numeric = 0;
            if(int.TryParse(value?.ToString(), out numeric))
            {
                return numeric;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var numeric = 0;
            if (string.IsNullOrEmpty(value?.ToString()))
                return 0;
            if (int.TryParse(value?.ToString(), out numeric))
            {
                if (numeric < System.Convert.ToInt32(parameter?.ToString()))
                {
                    return numeric;
                }
            }
            return 0;
        }
    }
}
