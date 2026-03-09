using System.Windows;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour CodePreviewWindow.xaml
    /// </summary>
    public partial class CodePreviewWindow : Window
    {
        public CodePreviewWindow()
        {
            InitializeComponent();
        }

        public string Code { get; set; } = string.Empty;

        public void CopyCommand(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(GeneratedCodeTextbox.Text);
        }
        public void CloseCommand(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
