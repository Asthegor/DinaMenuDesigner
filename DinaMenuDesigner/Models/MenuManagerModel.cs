using DinaMenuDesigner.Common;

using System.Collections.ObjectModel;

namespace DinaMenuDesigner.Models
{
    public class MenuManagerModel : ObservableObject
    {
        public MenuManagerModel()
        {
            Direction = MenuDirection.Vertical;
            SpacingX = 5;
            SpacingY = 5;
            ItemsPositionX = 100;
            ItemsPositionY = 300;
            CenterTitles = false;
            CenterItems = false;
            Titles = [];
            Items = [];
        }
        private MenuDirection _direction;
        private float _spacingX;
        private float _spacingY;
        private float _itemsPositionX;
        private float _itemsPositionY;
        private bool _centerTitles;
        private bool _centerItems;
        private ObservableCollection<TitleModel> _titles = [];
        private ObservableCollection<MenuItemModel> _items = [];

        public MenuDirection Direction { get => _direction; set => SetProperty(ref _direction, value); }
        public float SpacingX
        {
            get => _spacingX;
            set
            {
                if (value < 0)
                    value = 0;
                SetProperty(ref _spacingX, value);
            }
        }
        public float SpacingY
        {
            get => _spacingY;
            set
            {
                if (value < 0)
                    value = 0;
                SetProperty(ref _spacingY, value);
            }
        }
        public float ItemsPositionX { get => _itemsPositionX; set => SetProperty(ref _itemsPositionX, value); }
        public float ItemsPositionY { get => _itemsPositionY; set => SetProperty(ref _itemsPositionY, value); }
        public bool CenterTitles { get => _centerTitles; set => SetProperty(ref _centerTitles, value); }
        public bool CenterItems { get => _centerItems; set => SetProperty(ref _centerItems, value); }
        public ObservableCollection<TitleModel> Titles { get => _titles; set => SetProperty(ref _titles, value); }
        public ObservableCollection<MenuItemModel> Items { get => _items; set => SetProperty(ref _items, value); }

    }
}
