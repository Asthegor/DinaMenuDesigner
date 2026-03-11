using DinaMenuDesigner.Commands;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour ListBoxItemView.xaml
    /// </summary>
    public partial class ListBoxItemView : UserControl
    {
        public ListBoxItemView()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(ListBoxItemView), new PropertyMetadata(string.Empty));
        public string ElementContent
        {
            get => (string)GetValue(ElementContentProperty);
            set => SetValue(ElementContentProperty, value);
        }
        public static readonly DependencyProperty ElementContentProperty =
            DependencyProperty.Register(nameof(ElementContent), typeof(string), typeof(ListBoxItemView), new PropertyMetadata(string.Empty));

        public ICommand UpCommand
        {
            get => (RelayCommand)GetValue(UpCommandProperty);
            set => SetValue(UpCommandProperty, value);
        }
        public static readonly DependencyProperty UpCommandProperty =
            DependencyProperty.Register(nameof(UpCommand), typeof(ICommand), typeof(ListBoxItemView), new PropertyMetadata(null));
        
        public ICommand DownCommand
        {
            get => (RelayCommand)GetValue(DownCommandProperty);
            set => SetValue(DownCommandProperty, value);
        }
        public static readonly DependencyProperty DownCommandProperty =
            DependencyProperty.Register(nameof(DownCommand), typeof(ICommand), typeof(ListBoxItemView), new PropertyMetadata(null));

        public ICommand RemoveCommand
        {
            get => (RelayCommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }
        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register(nameof(RemoveCommand), typeof(ICommand), typeof(ListBoxItemView), new PropertyMetadata(null));


    }
}
