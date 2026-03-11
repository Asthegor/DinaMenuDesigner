using DinaMenuDesigner.Commands;
using DinaMenuDesigner.Common;
using DinaMenuDesigner.Models;
using DinaMenuDesigner.Services;

using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace DinaMenuDesigner.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IFileDialogService _fileDialogService;
        private readonly IWindowService _windowService;

        public MainViewModel(IFileDialogService? fileDialogService = null, IWindowService? windowService = null)
        {
            _fileDialogService = fileDialogService ?? new FileDialogService();
            _windowService = windowService ?? new WindowService();

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

            SaveCommand = new RelayCommand(_ => Save());
            LoadCommand = new RelayCommand(_ => Load());

            GenerateCommand = new RelayCommand(_ => GenerateCode());
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

        public RelayCommand SaveCommand { get; }
        public RelayCommand LoadCommand { get; }

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true, Converters = { new ColorJsonConverter() } };
        private void Save()
        {
            var filename = _fileDialogService.GetSaveFilePath("Fichiers JSON (*.json)|*.json");

            if (filename == null)
                return;

            var jsonString = JsonSerializer.Serialize(MenuManager, _jsonOptions);
            File.WriteAllText(filename, jsonString);
        }
        private void Load()
        {
            var filename = _fileDialogService.GetOpenFilePath("Fichiers JSON (*.json)|*.json");
            
            if (filename == null)
                return;

            var jsonString = File.ReadAllText(filename);
            if (string.IsNullOrEmpty(jsonString))
                return; // On devrait peut-être afficher un message à l'utilisateur

            MenuManager = JsonSerializer.Deserialize<MenuManagerModel>(jsonString, _jsonOptions)!;
        }


        public RelayCommand GenerateCommand { get; }

        private void GenerateCode()
        {
            var generatedCode = GenerateCodeService.GenerateDeclaration(MenuManager);
            _windowService.ShowCodePreview(generatedCode);
        }

        public string GeneratedCode
        {
            get => _generatedCode;
            set => SetProperty(ref _generatedCode, value);
        }
        private string _generatedCode = string.Empty;

        private bool _isGroupSelected;
        public bool IsGroupSelected
        {
            get => _isGroupSelected;
            set => SetProperty(ref _isGroupSelected, value);
        }
    }
}
