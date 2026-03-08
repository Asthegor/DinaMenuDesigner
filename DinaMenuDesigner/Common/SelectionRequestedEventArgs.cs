using System.Windows;

namespace DinaMenuDesigner.Common
{
    public class SelectionRequestedEventArgs : RoutedEventArgs
    {
        public bool Selected { get; set; }
    }
}
