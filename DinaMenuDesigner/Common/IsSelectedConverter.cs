using System.Globalization;
using System.Windows.Data;

namespace DinaMenuDesigner.Common
{
    class IsSelectedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] = SelectedElement
            // values[1] = élément courant du DataTemplate
            return ReferenceEquals(values[0], values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
