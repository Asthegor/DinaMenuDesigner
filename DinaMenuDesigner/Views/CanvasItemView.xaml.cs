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
        private Point _itemStartPosition;
        static CanvasItemView()
        {
            FontSizeProperty.OverrideMetadata(typeof(CanvasItemView),
                new FrameworkPropertyMetadata((d, e) => ((CanvasItemView)d).EnforceMinimumSize()));
        }
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

            var current = e.GetPosition(Parent as UIElement);
            var args = new AbsolutePositionChangedEventArgs()
            {
                RoutedEvent = AbsolutePositionChangedEvent,
                AbsoluteX = _itemStartPosition.X + (current.X - _dragStart!.Value.X) / Scale,
                AbsoluteY = _itemStartPosition.Y + (current.Y - _dragStart!.Value.Y) / Scale,
            };
            RaiseEvent(args);
        }
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragStart = null;
            ReleaseMouseCapture();
        }

        public event EventHandler<AbsolutePositionChangedEventArgs> AbsolutePositionChanged
        {
            add => AddHandler(AbsolutePositionChangedEvent, value);
            remove => RemoveHandler(AbsolutePositionChangedEvent, value);
        }
        public static readonly RoutedEvent AbsolutePositionChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(AbsolutePositionChanged), RoutingStrategy.Bubble, typeof(EventHandler<AbsolutePositionChangedEventArgs>), typeof(CanvasItemView));

        public string ElementContent
        {
            get => (string)GetValue(ElementContentProperty);
            set => SetValue(ElementContentProperty, value);
        }
        public static readonly DependencyProperty ElementContentProperty =
            DependencyProperty.Register(nameof(ElementContent), typeof(string), typeof(CanvasItemView),
                                        new PropertyMetadata(string.Empty, (d, e) => ((CanvasItemView)d).EnforceMinimumSize()));

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

        public double ElementWidth
        {
            get => (double)GetValue(ElementWidthProperty);
            set => SetValue(ElementWidthProperty, value);
        }
        public static readonly DependencyProperty ElementWidthProperty =
            DependencyProperty.Register(nameof(ElementWidth), typeof(double), typeof(CanvasItemView),
                                        new PropertyMetadata(0.0, (d, e) => ((CanvasItemView)d).EnforceMinimumSize()));

        public double ElementHeight
        {
            get => (double)GetValue(ElementHeightProperty);
            set => SetValue(ElementHeightProperty, value);
        }
        public static readonly DependencyProperty ElementHeightProperty =
            DependencyProperty.Register(nameof(ElementHeight), typeof(double), typeof(CanvasItemView),
                                        new PropertyMetadata(0.0, (d, e) => ((CanvasItemView)d).EnforceMinimumSize()));


        public event EventHandler<SelectionRequestedEventArgs> SelectionRequested
        {
            add => AddHandler(SelectionRequestedEvent, value);
            remove => RemoveHandler(SelectionRequestedEvent, value);
        }
        public static readonly RoutedEvent SelectionRequestedEvent =
            EventManager.RegisterRoutedEvent(nameof(SelectionRequested), RoutingStrategy.Bubble, typeof(EventHandler<SelectionRequestedEventArgs>), typeof(CanvasItemView));

        private Size MeasureText()
        {
            var typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            var formattedText = new FormattedText(
                ElementContent,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                FontSize,
                Brushes.Black,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            return new Size(formattedText.Width, formattedText.Height);
        }
        private void EnforceMinimumSize()
        {
            var size = MeasureText();
            var args = new SizeConstraintRequestedEventArgs()
            {
                RoutedEvent = SizeConstraintRequestedEvent,
                MinWidth = size.Width,
                MinHeight = size.Height,
            };
            RaiseEvent(args);
        }
        public event EventHandler<SizeConstraintRequestedEventArgs> SizeConstraintRequested
        {
            add => AddHandler(SizeConstraintRequestedEvent, value);
            remove => RemoveHandler(SizeConstraintRequestedEvent, value);
        }
        public static readonly RoutedEvent SizeConstraintRequestedEvent =
            EventManager.RegisterRoutedEvent(nameof(SizeConstraintRequested), RoutingStrategy.Bubble, typeof(EventHandler<SizeConstraintRequestedEventArgs>), typeof(CanvasItemView));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(nameof(Scale), typeof(double), typeof(CanvasItemView), new PropertyMetadata(1.0));
    }
}
