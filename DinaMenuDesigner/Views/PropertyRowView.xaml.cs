using System.Windows;
using System.Windows.Controls;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour PropertyRowView.xaml
    /// </summary>
    public partial class PropertyRowView : UserControl
    {
        public PropertyRowView()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(PropertyRowView), new PropertyMetadata(string.Empty));
        public object Value
        {
            get => (object)GetValue(ValueProperty); 
            set => SetValue(ValueProperty, value);
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(object), typeof(PropertyRowView), new PropertyMetadata(null));

    }
}
