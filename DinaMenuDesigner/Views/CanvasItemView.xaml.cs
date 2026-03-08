using DinaMenuDesigner.Common;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour CanvasItemView.xaml
    /// </summary>
    public partial class CanvasItemView : UserControl
    {
        private Point? _dragStart;
        public CanvasItemView()
        {
            InitializeComponent();

            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseMove += OnMouseMove;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStart = e.GetPosition(Parent as UIElement);

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

            //var newPosition = e.GetPosition(this);
            var newPosition = e.GetPosition(Parent as UIElement);
            var offset = newPosition - _dragStart;

            var positionChangedEventArgs = new PositionChangedEventArgs()
            {
                RoutedEvent = PositionChangedEvent,
                DeltaX = offset.Value.X,
                DeltaY = offset.Value.Y,
            };
            RaiseEvent(positionChangedEventArgs);

            _dragStart = newPosition;
        }
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragStart = null;
            ReleaseMouseCapture();
        }


        public string ElementContent
        {
            get => (string)GetValue(ElementContentProperty);
            set => SetValue(ElementContentProperty, value);
        }
        public static readonly DependencyProperty ElementContentProperty =
            DependencyProperty.Register(nameof(ElementContent), typeof(string), typeof(CanvasItemView), new PropertyMetadata(string.Empty));

        public Color ElementColor
        {
            get => (Color)GetValue(ElementColorProperty);
            set => SetValue(ElementColorProperty, value);
        }
        public static readonly DependencyProperty ElementColorProperty =
            DependencyProperty.Register(nameof(ElementColor), typeof(Color), typeof(CanvasItemView), new PropertyMetadata(Colors.White));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(CanvasItemView), new PropertyMetadata(false));


        public event EventHandler<PositionChangedEventArgs> PositionChanged
        {
            add => AddHandler(PositionChangedEvent, value);
            remove => RemoveHandler(PositionChangedEvent, value);
        }
        public static readonly RoutedEvent PositionChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(PositionChanged), RoutingStrategy.Bubble, typeof(EventHandler<PositionChangedEventArgs>), typeof(CanvasItemView));



        public double PositionX
        {
            get => (double)GetValue(PositionXProperty);
            set => SetValue(PositionXProperty, value);
        }
        public static readonly DependencyProperty PositionXProperty =
            DependencyProperty.Register(nameof(PositionX), typeof(double), typeof(CanvasItemView), new PropertyMetadata(0.0));

        public double PositionY
        {
            get => (double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }
        public static readonly DependencyProperty PositionYProperty =
            DependencyProperty.Register(nameof(PositionY), typeof(double), typeof(CanvasItemView), new PropertyMetadata(0.0));


        public event EventHandler<SelectionRequestedEventArgs> SelectionRequested
        {
            add => AddHandler(SelectionRequestedEvent, value);
            remove => RemoveHandler(SelectionRequestedEvent, value);
        }
        public static readonly RoutedEvent SelectionRequestedEvent =
            EventManager.RegisterRoutedEvent(nameof(SelectionRequested), RoutingStrategy.Bubble, typeof(EventHandler<SelectionRequestedEventArgs>), typeof(CanvasItemView));

    }
}
