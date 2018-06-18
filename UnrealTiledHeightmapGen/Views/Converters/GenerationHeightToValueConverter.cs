using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using UnrealTiledHeightmapGen.Data;

namespace UnrealTiledHeightmapGen.Views.Converters
{
    public class GenerationHeightToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var generationHeight = value as GenerationHeightComboBoxItem;
                return System.Convert.ToInt32(generationHeight?.Level);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
        //    if (targetType == typeof(GenerationHeightComboBoxItem))
         //   {
                var cloneGenerationHeightComboBoxItem = new GenerationHeightComboBoxItem();
                cloneGenerationHeightComboBoxItem.Id = System.Convert.ToInt32(value);
                cloneGenerationHeightComboBoxItem.Level = cloneGenerationHeightComboBoxItem.Id.ToString();

                return cloneGenerationHeightComboBoxItem;
         //   }
         //   return null;
        }
    }
}
