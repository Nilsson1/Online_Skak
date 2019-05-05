using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Online_Skak
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            { 
            MainWindow window = new MainWindow();
            window.ShowDialog();
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("hello: {0}"); 
                
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MainWindow test = new MainWindow();
            test.Show();

        }

        private void Settingsbutton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Settings();
            menu.Visibility = Visibility.Hidden;
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
           // Application.Exit();
        }
    }
}
