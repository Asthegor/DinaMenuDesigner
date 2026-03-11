using System.Windows;

namespace DinaMenuDesigner.Common
{
    public class AbsolutePositionChangedEventArgs : RoutedEventArgs
    {
        public double AbsoluteX { get; set; }
        public double AbsoluteY { get; set; }
    }
}
