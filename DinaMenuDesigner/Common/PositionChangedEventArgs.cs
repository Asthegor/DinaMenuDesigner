using System.Windows;

namespace DinaMenuDesigner.Common
{
    public class PositionChangedEventArgs : RoutedEventArgs
    {
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
    }
}
