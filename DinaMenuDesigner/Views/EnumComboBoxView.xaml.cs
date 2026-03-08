using System.Windows;
using System.Windows.Controls;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour EnumComboBoxView.xaml
    /// </summary>
    public partial class EnumComboBoxView : UserControl
    {
        public EnumComboBoxView()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(EnumComboBoxView), new PropertyMetadata(string.Empty));

        public Type EnumType
        {
            get => (Type)GetValue(EnumTypeProperty);
            set => SetValue(EnumTypeProperty, value);
        }
        public static readonly DependencyProperty EnumTypeProperty =
            DependencyProperty.Register(nameof(EnumType), typeof(Type), typeof(EnumComboBoxView), new PropertyMetadata(null, OnEnumTypeChanged));
        private static void OnEnumTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not Type enumType)
                return;
            var instance = (EnumComboBoxView)d;
            instance.EnumComboBox.ItemsSource = Enum.GetValues(enumType);
        }

        public object SelectedValue
        {
            get => (object)GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(nameof(SelectedValue), typeof(object), typeof(EnumComboBoxView), new PropertyMetadata(null));

    }
}
