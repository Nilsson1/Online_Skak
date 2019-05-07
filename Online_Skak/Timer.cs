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
        Label label = new Label();

        public Timer(Label label)
        {
            this.label = label;
        }

        private void Time_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.0);
            dispatcherTimer.Tick += dtTicker;
            dispatcherTimer.Start();
            label.Content = "hej";
            Console.WriteLine(sender + "_" + e);
        }

        private int increment = 11;
        private void dtTicker(object sender, EventArgs e)
        {
            
            var label = (Label)sender;
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
