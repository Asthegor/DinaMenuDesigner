using DinaMenuDesigner.Common;

using System.Windows.Media;

namespace DinaMenuDesigner.Models
{
    public class MenuItemModel : MenuElementModel
    {
        public MenuItemModel() : base("fontMenu")
        {
            DisableColor = Colors.DarkGray;
            IsEnabled = true;
            HorizontalAlignment = DinaHorizontalAlignment.Left;
            VerticalAlignment = DinaVerticalAlignment.Top;
            SelectionMethodName = null;
            DeselectionMethodName = null;
            ActivationMethodName = null;
        }
        private Color _disableColor;
        private bool _isEnabled;
        private DinaHorizontalAlignment _horizontalAlignment;
        private DinaVerticalAlignment _verticalAlignment;
        private string? _selectionMethodName;
        private string? _deselectionMethodName;
        private string? _activationMethodName;

        public Color DisableColor { get => _disableColor; set => SetProperty(ref _disableColor, value); }
        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }
        public DinaHorizontalAlignment HorizontalAlignment { get => _horizontalAlignment; set => SetProperty(ref _horizontalAlignment, value); }
        public DinaVerticalAlignment VerticalAlignment { get => _verticalAlignment; set => SetProperty(ref _verticalAlignment, value); }
        public string? SelectionMethodName { get => _selectionMethodName; set => SetProperty(ref _selectionMethodName, value); }
        public string? DeselectionMethodName {  get => _deselectionMethodName; set => SetProperty(ref _deselectionMethodName, value); }
        public string? ActivationMethodName {  get => _activationMethodName; set => SetProperty(ref _activationMethodName, value); }
    }
}
