using DinaMenuDesigner.Views;

using System.Windows;

namespace DinaMenuDesigner.Services
{
    public class WindowService : IWindowService
    {
        private CodePreviewWindow? _codePreviewWindow;

        public void ShowCodePreview(string code)
        {
            _codePreviewWindow ??= new CodePreviewWindow();
            _codePreviewWindow.Owner = Application.Current.MainWindow;
            _codePreviewWindow.Code = code;
            _codePreviewWindow.Show();
            _codePreviewWindow.Activate();
        }
    }
}
