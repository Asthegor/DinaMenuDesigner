using DinaMenuDesigner.Common;
using DinaMenuDesigner.Models;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour GroupedItemsView.xaml
    /// </summary>
    public partial class GroupedItemsView : UserControl
    {
        private Point? _dragStart;        // position souris au MouseDown dans le Canvas
        private Point _itemStartPosition; // position de l'item au MouseDown
        public GroupedItemsView()
        {
            InitializeComponent();

            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseMove += OnMouseMove;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //_dragStart = e.GetPosition(Parent as UIElement);
            _dragStart = e.GetPosition(ParentCanvas);
            _itemStartPosition = new Point(PositionX, PositionY);

            var selectionRequestedEventArgs = new SelectionRequestedEventArgs()
            {
                RoutedEvent = SelectionRequestedEvent,
                Selected = true,
            };
            RaiseEvent(selectionRequestedEventArgs);

            CaptureMouse();
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragStart == null)
                return;

            var current = e.GetPosition(ParentCanvas);

            var positionChangedEventArgs = new AbsolutePositionChangedEventArgs()
            {
                RoutedEvent = AbsolutePositionChangedEvent,
                AbsoluteX = _itemStartPosition.X + (current.X - _dragStart!.Value.X),
                AbsoluteY = _itemStartPosition.Y + (current.Y - _dragStart!.Value.Y),
            };
            RaiseEvent(positionChangedEventArgs);

            _dragStart = current;
        }
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragStart = null;
            ReleaseMouseCapture();
        }
        public event EventHandler<SelectionRequestedEventArgs> SelectionRequested
        {
            add => AddHandler(SelectionRequestedEvent, value);
            remove => RemoveHandler(SelectionRequestedEvent, value);
        }
        public static readonly RoutedEvent SelectionRequestedEvent =
            EventManager.RegisterRoutedEvent(nameof(SelectionRequested), RoutingStrategy.Bubble, typeof(EventHandler<SelectionRequestedEventArgs>), typeof(GroupedItemsView));


        public MenuDirection Direction
        {
            get => (MenuDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(nameof(Direction), typeof(MenuDirection), typeof(GroupedItemsView), new PropertyMetadata(MenuDirection.Vertical));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(GroupedItemsView), new PropertyMetadata(false));


        public event EventHandler<AbsolutePositionChangedEventArgs> AbsolutePositionChanged
        {
            add => AddHandler(AbsolutePositionChangedEvent, value);
            remove => RemoveHandler(AbsolutePositionChangedEvent, value);
        }
        public static readonly RoutedEvent AbsolutePositionChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(AbsolutePositionChanged), RoutingStrategy.Bubble, typeof(EventHandler<AbsolutePositionChangedEventArgs>), typeof(GroupedItemsView));

        public double PositionX
        {
            get => (double)GetValue(PositionXProperty);
            set => SetValue(PositionXProperty, value);
        }
        public static readonly DependencyProperty PositionXProperty =
            DependencyProperty.Register(nameof(PositionX), typeof(double), typeof(GroupedItemsView), new PropertyMetadata(0.0));

        public double PositionY
        {
            get => (double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }
        public static readonly DependencyProperty PositionYProperty =
            DependencyProperty.Register(nameof(PositionY), typeof(double), typeof(GroupedItemsView), new PropertyMetadata(0.0));



        public ObservableCollection<MenuItemModel> Items
        {
            get { return (ObservableCollection<MenuItemModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<MenuItemModel>), typeof(GroupedItemsView), new PropertyMetadata());

        public double SpacingX
        {
            get => (double)GetValue(SpacingXProperty);
            set => SetValue(SpacingXProperty, value);
        }
        public static readonly DependencyProperty SpacingXProperty =
            DependencyProperty.Register(nameof(SpacingX), typeof(double), typeof(GroupedItemsView), new PropertyMetadata(0.0));

        public double SpacingY
        {
            get => (double)GetValue(SpacingYProperty);
            set => SetValue(SpacingYProperty, value);
        }
        public static readonly DependencyProperty SpacingYProperty =
            DependencyProperty.Register(nameof(SpacingY), typeof(double), typeof(GroupedItemsView), new PropertyMetadata(0.0));

        public Canvas? ParentCanvas
        {
            get => (Canvas?)GetValue(ParentCanvasProperty);
            set => SetValue(ParentCanvasProperty, value);
        }
        public static readonly DependencyProperty ParentCanvasProperty =
            DependencyProperty.Register(nameof(ParentCanvas), typeof(Canvas), typeof(GroupedItemsView), new PropertyMetadata(null));
    }
}
