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
        private Color color;
        private SerialPort arduinoPort;

        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
            color = new Color() { A = 255, R = 0, G = 0, B = 0 };
            try
            {
                arduinoPort = new SerialPort(SerialPort.GetPortNames().First(), 115200);
                arduinoPort.Open();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception");
                throw;
            }
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            if (arduinoPort != null)
            {
                if (arduinoPort.IsOpen)
                arduinoPort.Close();
            }
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetColorText((int)(((Slider)sender).Value), ReturnPositionIndex((Slider)sender));
            ColorOutGrid.Background = new SolidColorBrush(color);
            PrintColorToSerial(color);
        }

        private void PrintColorToSerial(Color xcolor)
        {
            arduinoPort.Write(new byte[] { (byte)'#', xcolor.R, xcolor.G, xcolor.B }, 0, 4);
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
