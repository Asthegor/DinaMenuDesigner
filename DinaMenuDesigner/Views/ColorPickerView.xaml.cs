using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DinaMenuDesigner.Views
{
    /// <summary>
    /// Logique d'interaction pour ColorPickerView.xaml
    /// </summary>
    public partial class ColorPickerView : UserControl, INotifyPropertyChanged
    {
        public ColorPickerView()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(ColorPickerView), new PropertyMetadata(string.Empty));

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorPickerView), new PropertyMetadata(Colors.White, OnSelectedColorChanged));

        public event PropertyChangedEventHandler? PropertyChanged;

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (ColorPickerView)d;
            instance.RaisePropertyChanged(nameof(instance.R));
            instance.RaisePropertyChanged(nameof(instance.G));
            instance.RaisePropertyChanged(nameof(instance.B));
            instance.RaisePropertyChanged(nameof(instance.A));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public byte R
        {
            get => SelectedColor.R;
            set => SelectedColor = Color.FromArgb(SelectedColor.A, value, SelectedColor.G, SelectedColor.B);
        }
        public byte G
        {
            get => SelectedColor.G;
            set => SelectedColor = Color.FromArgb(SelectedColor.A, SelectedColor.R, value, SelectedColor.B);
        }
        public byte B
        {
            get => SelectedColor.B;
            set => SelectedColor = Color.FromArgb(SelectedColor.A, SelectedColor.R, SelectedColor.G, value);
        }
        public byte A
        {
            get => SelectedColor.A;
            set => SelectedColor = Color.FromArgb(value, SelectedColor.R, SelectedColor.G, SelectedColor.B);
        }
    }
}
