using DinaMenuDesigner.Commands;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour SectionTitleView.xaml
    /// </summary>
    public partial class SectionTitleView : UserControl
    {
        public SectionTitleView()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(SectionTitleView), new PropertyMetadata(string.Empty));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(SectionTitleView), new PropertyMetadata(null));

    }
}
