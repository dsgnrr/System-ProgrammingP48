using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstDesktop.Windows
{
    /// <summary>
    /// Interaction logic for Containers.xaml
    /// </summary>
    public partial class Containers : Window
    {
        public Containers()
        {
            InitializeComponent();
            pushWrap();
        }

        private void pushWrap()
        {
            for(int i = 0; i < 100; i++)
            {
                wrapPanel.Children.Add(generateButton((i+1).ToString()));
            }
        }
        private Button generateButton(string id)
        {
            Random rnd = new Random();
            Button b = new Button();
            b.Height = 70;
            b.Width = 70;
            b.Margin = new Thickness(10);
            b.Background = new SolidColorBrush(
                Color.FromRgb(
                    (byte)rnd.Next(0, 255),
                    (byte)rnd.Next(0, 255),
                    (byte)rnd.Next(0, 255)
                    ));
            b.FontWeight = FontWeights.Bold;
            b.FontSize = 25;
            b.Content = $"#{id}";
            return b;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Ok");
        }
    }
}
