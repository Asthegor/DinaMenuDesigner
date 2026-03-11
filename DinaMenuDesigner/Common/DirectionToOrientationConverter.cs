using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DinaMenuDesigner.Common
{
    public class DirectionToOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var direction = (MenuDirection)value;
            return direction == MenuDirection.Horizontal ? Orientation.Horizontal : Orientation.Vertical;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
