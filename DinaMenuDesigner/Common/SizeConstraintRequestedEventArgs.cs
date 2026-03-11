using System.Windows;

namespace DinaMenuDesigner.Common
{
    public class SizeConstraintRequestedEventArgs : RoutedEventArgs
    {
        public double MinWidth { get; set; }
        public double MinHeight { get; set; }
    }
}
