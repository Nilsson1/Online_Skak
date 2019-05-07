using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Online_Skak
{
    class Timer
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Label label;

        public Timer(int row, int column)
        {
            label = new Label();
            //label.Name = "labelTime";
            label.Content = "Test";
            Grid.SetRow(label, 3);
            Grid.SetColumn(label, 8);

            label.Loaded += Time_Loaded;
            
        }

        private void Time_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.0);
            dispatcherTimer.Tick += dtTicker;
            dispatcherTimer.Start();
            label.Content = "hej";
            Console.WriteLine(sender + "_" + e);
        }

        public Label GetLabel()
        {
            return label;
        }

        private int increment = 11;
        private void dtTicker(object sender, EventArgs e)
        {
            increment--;
            label.Content = increment.ToString();
            if ((String)label.Content == "5")
            {
                dispatcherTimer.Stop();
            }
            //dispatcherTimer.Start();               
        }


    }

}
