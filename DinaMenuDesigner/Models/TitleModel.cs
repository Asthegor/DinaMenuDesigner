using System.Windows.Media;

namespace DinaMenuDesigner.Models
{
    public class TitleModel : MenuElementModel
    {
        public TitleModel() : base("fontTitle")
        {
            HasShadow = false;
            ShadowColor = Colors.Black;
            ShadowOffsetX = 0;
            ShadowOffsetY = 0;
        }

        private bool _hasShadow;
        private Color _shadowColor;
        private float _shadowOffsetX;
        private float _shadowOffsetY;

        public bool HasShadow {  get => _hasShadow; set => SetProperty(ref _hasShadow, value); }
        public Color ShadowColor { get => _shadowColor; set => SetProperty(ref _shadowColor, value); }
        public float ShadowOffsetX { get => _shadowOffsetX; set => SetProperty(ref _shadowOffsetX, value); }
        public float ShadowOffsetY { get => _shadowOffsetY; set => SetProperty(ref _shadowOffsetY, value); }

    }
}
