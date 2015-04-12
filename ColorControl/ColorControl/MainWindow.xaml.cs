using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Color color;

        public MainWindow()
        {
            InitializeComponent();
            color = new Color() { A = 255 };
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetColorText((int)(((Slider)sender).Value), ReturnPositionIndex((Slider)sender));
            ColorOutGrid.Background = new SolidColorBrush(color);
        }

        ColorIndex ReturnPositionIndex(Slider slider)
        {
            if (slider.Name == "RedSlider")
                return ColorIndex.Red;
            if (slider.Name == "GreenSlider")
                return ColorIndex.Green;
            if (slider.Name == "BlueSlider")
                return ColorIndex.Blue;
            return ColorIndex.Red;
        }

        void SetColorText(int colorValue, ColorIndex index)
        {
            switch (index)
            {
                case ColorIndex.Red:
                    RedBlock.Text = colorValue.ToString();
                    color.R = (byte)colorValue;
                    break;
                case ColorIndex.Green:
                    GreenBlock.Text = colorValue.ToString();
                    color.G = (byte)colorValue;
                    break;
                case ColorIndex.Blue:
                    BlueBlock.Text = colorValue.ToString();
                    color.B = (byte)colorValue;
                    break;
            }
        }
    }

    enum ColorIndex
    {
        Red,
        Green,
        Blue
    }

}
