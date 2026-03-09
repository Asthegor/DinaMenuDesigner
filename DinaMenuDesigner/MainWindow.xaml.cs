using DinaMenuDesigner.Common;
using DinaMenuDesigner.Models;
using DinaMenuDesigner.ViewModels;
using DinaMenuDesigner.Views;

using System.Windows;

namespace DinaMenuDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            var item = (CanvasItemView)sender;
            var model = (MenuElementModel)item.DataContext;

            var transform = PreviewCanvas.TransformToAncestor(PreviewViewbox);
            var scale = transform.Transform(new Point(1, 0)).X;

            var deltaX = e.DeltaX / scale;
            var deltaY = e.DeltaY / scale;

            model.PositionX = (int)Math.Clamp(model.PositionX + deltaX, 0, 1920);
            model.PositionY = (int)Math.Clamp(model.PositionY + deltaY, 0, 1080);

        }

        private void OnSelectionRequested(object sender, SelectionRequestedEventArgs e)
        {
            var item = (CanvasItemView)sender;
            var model = (MenuElementModel)item.DataContext;

            var mainViewModel = (MainViewModel)DataContext;
            if (model is TitleModel titleModel)
                mainViewModel.SelectedTitle = titleModel;
            else if (model is MenuItemModel menuModel)
                mainViewModel.SelectedItem = menuModel;
        }
    }
}