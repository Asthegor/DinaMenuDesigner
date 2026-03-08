using DinaMenuDesigner.Commands;
using DinaMenuDesigner.Common;
using DinaMenuDesigner.Models;

using System.Windows.Input;

namespace DinaMenuDesigner.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            MenuManager = new MenuManagerModel();
            SelectedTitle = null;
            SelectedItem = null;

            AddTitleCommand = new RelayCommand(_ => AddTitle());
            RemoveTitleCommand = new RelayCommand(param => RemoveTitle(param));
            MoveTitleUpCommand = new RelayCommand(param => MoveTitleUp(param), param => param is TitleModel title && MenuManager.Titles.IndexOf(title) > 0);
            MoveTitleDownCommand = new RelayCommand(param => MoveTitleDown(param), param => param is TitleModel title && MenuManager.Titles.IndexOf(title) < MenuManager.Titles.Count - 1);

            AddItemCommand = new RelayCommand(_ => AddItem());
            RemoveItemCommand = new RelayCommand(param => RemoveItem(param));
            MoveItemUpCommand = new RelayCommand(param => MoveItemUp(param), param => param is MenuItemModel item && MenuManager.Items.IndexOf(item) > 0);
            MoveItemDownCommand = new RelayCommand(param => MoveItemDown(param), param => param is MenuItemModel item && MenuManager.Items.IndexOf(item) < MenuManager.Items.Count - 1);
        }

        private MenuManagerModel _menuManager = new MenuManagerModel();
        private TitleModel? _selectedTitle;
        private MenuItemModel? _selectedItem;

        public MenuManagerModel MenuManager
        {
            get => _menuManager;
            set => SetProperty(ref _menuManager, value);
        }
        public MenuElementModel? SelectedElement 
        {
            get
            {
                if (_selectedTitle != null)
                    return _selectedTitle;
                return _selectedItem;
            }
        }

        public TitleModel? SelectedTitle
        {
            get => _selectedTitle;
            set
            {
                SetProperty(ref _selectedTitle, value);
                OnPropertyChanged(nameof(SelectedElement));
                if (value != null)
                    SelectedItem = null;
            }
        }
        public MenuItemModel? SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(SelectedElement));
                if (value != null)
                    SelectedTitle = null;
            }
        }

        public RelayCommand AddTitleCommand { get; }
        public RelayCommand RemoveTitleCommand { get; }
        public RelayCommand MoveTitleUpCommand { get; }
        public RelayCommand MoveTitleDownCommand { get; }

        public RelayCommand AddItemCommand { get; }
        public RelayCommand RemoveItemCommand { get; }
        public RelayCommand MoveItemUpCommand { get; }
        public RelayCommand MoveItemDownCommand { get; }

        private void AddTitle()
        {
            var title = new TitleModel
            {
                Content = $"Title {MenuManager.Titles.Count}"
            };
            MenuManager.Titles.Add(title);
            SelectedTitle = title;
        }
        private void RemoveTitle(object? param)
        {
            if (param is not TitleModel title)
                return;
            MenuManager.Titles.Remove(title);
            SelectedTitle = null;
            CommandManager.InvalidateRequerySuggested();
        }
        private void MoveTitleUp(object? param)
        {
            if (param is not TitleModel title)
                return;
            SelectedTitle = title;
            var index = MenuManager.Titles.IndexOf(title);
            MenuManager.Titles.Move(index, index - 1);
        }
        private void MoveTitleDown(object? param)
        {
            if (param is not TitleModel title)
                return;
            SelectedTitle = title;
            var index = MenuManager.Titles.IndexOf(title);
            MenuManager.Titles.Move(index, index + 1);
        }

        private void AddItem()
        {
            var item = new MenuItemModel()
            {
                Content = $"Item {MenuManager.Items.Count}"
            };
            MenuManager.Items.Add(item);
            SelectedItem = item;
        }
        private void RemoveItem(object? param)
        {
            if (param is not MenuItemModel item)
                return;
            MenuManager.Items.Remove(item);
            SelectedItem = null;
            CommandManager.InvalidateRequerySuggested();
        }
        private void MoveItemUp(object? param)
        {
            if (param is not MenuItemModel item)
                return;
            SelectedItem = item;
            var index = MenuManager.Items.IndexOf(item);
            MenuManager.Items.Move(index, index - 1);
        }
        private void MoveItemDown(object? param)
        {
            if (param is not MenuItemModel item)
                return;
            SelectedItem = item;
            var index = MenuManager.Items.IndexOf(item);
            MenuManager.Items.Move(index, index + 1);
        }

    }
}
