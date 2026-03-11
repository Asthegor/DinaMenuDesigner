using DinaMenuDesigner.Common;

using System.Windows.Media;

namespace DinaMenuDesigner.Models
{
    public abstract class MenuElementModel : ObservableObject
    {
        public MenuElementModel(string placeholderFont = "fontMenu")
        {
            Id = Guid.NewGuid();
            Content = "Nouveau";
            Color = Colors.White;
            Visible = true;
            PlaceholderFont = placeholderFont;
        }

        private string _content = string.Empty;
        private float _positionX;
        private float _positionY;
        private float _width;
        private float _height;
        private Color _color;
        private int _zOrder;
        private bool _visible;
        private string _placeholderFont = string.Empty;
        private float _fontSize = 12f;

        public Guid Id { get; init; }
        public string Content
        {
            get => _content;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                SetProperty(ref _content, value);
            }
        }
        public float PositionX { get => _positionX; set => SetProperty(ref _positionX, value); }
        public float PositionY { get => _positionY; set => SetProperty(ref _positionY, value); }
        public float Width { get => _width; set => SetProperty(ref _width, value); }
        public float Height { get => _height; set => SetProperty(ref _height, value); }
        public virtual Color Color { get => _color; set => SetProperty(ref _color, value); }
        public int ZOrder { get => _zOrder; set => SetProperty(ref _zOrder, value); }
        public bool Visible { get => _visible; set => SetProperty(ref _visible, value); }
        public string PlaceholderFont { get => _placeholderFont; set => SetProperty(ref _placeholderFont, value); }
        public float FontSize { get => _fontSize; set => SetProperty(ref _fontSize, value); }

    }
}
