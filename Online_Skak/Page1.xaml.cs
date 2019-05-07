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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Online_Skak
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Page1()
        {
            InitializeComponent();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            DynamicGridBuilder d = new DynamicGridBuilder();
            d.CreateBoardButtons();

        }

        private void Settingsbutton_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Settings(this);
            menu.Visibility = Visibility.Hidden;

        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
           Application.Current.MainWindow.Close();
        }

      /*  private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            // Updating the Label which displays the current second
            test.Content = DateTime.Now.Second;

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }*/

        private void Time_Loaded(object sender, RoutedEventArgs e)
        {
            
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.0);
            dispatcherTimer.Tick += dtTicker;
            dispatcherTimer.Start();

            
        }

        private int increment = 11;
        private void dtTicker(object sender, EventArgs e)
        {
            increment--;
            time.Content = increment.ToString();
            if ((String)time.Content == "5")
            {
                dispatcherTimer.Stop();
            }
            dispatcherTimer.Start();               
        }
    }
}
