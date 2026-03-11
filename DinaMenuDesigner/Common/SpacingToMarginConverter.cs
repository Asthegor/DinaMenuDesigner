using System.Globalization;
using System.Windows.Data;

namespace DinaMenuDesigner.Common
{
    public class SpacingToMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] = Direction
            // values[1] = X
            // values[2] = Y
            var direction = (MenuDirection)values[0];
            var spacingX = (double)values[1];
            var spacingY = (double)values[2];
            return direction == MenuDirection.Vertical ? $"0,0,0,{spacingY}" : $"0,0,{spacingX},0";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
