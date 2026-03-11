using DinaMenuDesigner.Common;
using DinaMenuDesigner.Models;
using DinaMenuDesigner.ViewModels;
using DinaMenuDesigner.Views;

using System.Diagnostics;
using System.Windows;

namespace DinaMenuDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _canvasScale = 1.0;

        public MainWindow()
        {
            InitializeComponent();
            PreviewViewbox.SizeChanged += (s, e) =>
            {
                var transform = PreviewCanvas.TransformToAncestor(PreviewViewbox);
                _canvasScale = transform.Transform(new Point(1, 0)).X;
            };
        }

        private void OnPositionChanged(object sender, AbsolutePositionChangedEventArgs e)
        {
            var item = (CanvasItemView)sender;
            var transform = PreviewCanvas.TransformToAncestor(PreviewViewbox);
            item.Scale = transform.Transform(new Point(1, 0)).X;
            var model = (MenuElementModel)item.DataContext;
            model.PositionX = (int)Math.Clamp(e.AbsoluteX, 0, 1920);
            model.PositionY = (int)Math.Clamp(e.AbsoluteY, 0, 1080);
        }

        private void OnSelectionRequested(object sender, SelectionRequestedEventArgs e)
        {
            var item = (CanvasItemView)sender;
            var model = (MenuElementModel)item.DataContext;

            var mainViewModel = (MainViewModel)DataContext;
            mainViewModel.IsGroupSelected = false;

            if (model is TitleModel titleModel)
                mainViewModel.SelectedTitle = titleModel;
            else if (model is MenuItemModel menuModel)
                mainViewModel.SelectedItem = menuModel;
        }
        private void OnGroupPositionChanged(object sender, AbsolutePositionChangedEventArgs e)
        {
            var item = (GroupedItemsView)sender;
            var model = (MainViewModel)DataContext;
            var menuManagerModel = model.MenuManager;

            var transform = PreviewCanvas.TransformToAncestor(PreviewViewbox);
            var scale = transform.Transform(new Point(1, 0)).X;

            var deltaX = e.AbsoluteX;// / scale;
            var deltaY = e.AbsoluteY;// / scale;

            menuManagerModel.ItemsPositionX = (float)Math.Clamp(menuManagerModel.ItemsPositionX + deltaX, 0, 1920);
            menuManagerModel.ItemsPositionY = (float)Math.Clamp(menuManagerModel.ItemsPositionY + deltaY, 0, 1080);
        }

        private void OnGroupSelectionRequested(object sender, SelectionRequestedEventArgs e)
        {
            var mainViewModel = (MainViewModel)DataContext;
            mainViewModel.IsGroupSelected = true;
        }

        private void OnSizeConstraintRequested(object sender, SizeConstraintRequestedEventArgs e)
        {
            var item = (CanvasItemView)sender;
            var model = (MenuElementModel)item.DataContext;
            if (model.Width < e.MinWidth)
                model.Width = (int)e.MinWidth;
            if (model.Height < e.MinHeight)
                model.Height = (int)e.MinHeight;
        }
        public double CanvasScale
        {
            get => _canvasScale;
            private set
            {
                _canvasScale = value;
                // pas besoin de INotifyPropertyChanged ici car on utilise ElementName binding
            }
        }
    }
}