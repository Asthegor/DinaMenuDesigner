using DinaMenuDesigner.Models;

using System.Globalization;
using System.Windows.Data;

namespace DinaMenuDesigner.Common
{
    class MenuElementToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuElement = (MenuElementModel)value;
            return menuElement != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
