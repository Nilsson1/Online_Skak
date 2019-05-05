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

namespace Online_Skak
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
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
            //Main.Content = new Settings();
            menu.Visibility = Visibility.Hidden;

        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            // Application.Exit();
        }
    }
}
