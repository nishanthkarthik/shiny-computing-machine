using System;
using System.IO.Ports;
using System.Linq;
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
        private Color _color;
        private readonly SerialPort _arduinoPort;

        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
            _color = new Color() { A = 255, R = 0, G = 0, B = 0 };
            try
            {
                _arduinoPort = new SerialPort(SerialPort.GetPortNames().First(), 115200);
                _arduinoPort.Open();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception");
                throw;
            }
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            if (_arduinoPort != null)
            {
                if (_arduinoPort.IsOpen)
                _arduinoPort.Close();
            }
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetColorText((int)(((Slider)sender).Value), ReturnPositionIndex((Slider)sender));
            ColorOutGrid.Background = new SolidColorBrush(_color);
            PrintColorToSerial(_color);
        }

        private void PrintColorToSerial(Color xcolor)
        {
            try
            {
                _arduinoPort.Write(new byte[] { (byte)'#', xcolor.R, xcolor.G, xcolor.B }, 0, 4);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception");
                throw;
            }
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
                    _color.R = (byte)colorValue;
                    break;
                case ColorIndex.Green:
                    GreenBlock.Text = colorValue.ToString();
                    _color.G = (byte)colorValue;
                    break;
                case ColorIndex.Blue:
                    BlueBlock.Text = colorValue.ToString();
                    _color.B = (byte)colorValue;
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
