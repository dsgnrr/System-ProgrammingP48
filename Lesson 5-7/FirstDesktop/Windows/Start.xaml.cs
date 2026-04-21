using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstDesktop
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

        private void touch_btn_Click(object sender, RoutedEventArgs e)
        {
            myButton.Background = new SolidColorBrush(Color.FromRgb(60, 128, 135));
            MessageBoxResult result = MessageBox.Show("DON`T", "1", MessageBoxButton.OKCancel, MessageBoxImage.Error);

            if (result == MessageBoxResult.OK)
            {
                MessageBox.Show("Ok");
            }
            else
            {
                MessageBox.Show("Cancel");
            }
        }
    }
}