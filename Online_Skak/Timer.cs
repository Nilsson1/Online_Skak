using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Online_Skak
{
    class Timer
    {
        public DispatcherTimer dtWhite = new DispatcherTimer();
        public DispatcherTimer dtBlack = new DispatcherTimer();
        Label labelWhite, labelBlack ;
       
 
        public Timer(int row, int column)
        {
            labelWhite = new Label();
            labelBlack = new Label();
            //label.Name = "labelTime";
            labelWhite.Content = "Test";
            labelBlack.Content = "test2";
            Grid.SetRow(labelWhite, row);
            Grid.SetColumn(labelWhite, column);
            Grid.SetRow(labelBlack, row);
            Grid.SetColumn(labelBlack, column);
            labelWhite.Loaded += Time_Loaded;
            labelBlack.Loaded += Timeloaded2;

        }

        private void Time_Loaded(object sender, RoutedEventArgs e)
        {
            dtWhite.Interval = TimeSpan.FromSeconds(1.0);
            dtWhite.Tick += dtTickerWhite;
            dtWhite.Start();
            labelWhite.Content = " ";
            Console.WriteLine(sender + "_" + e);
        }

        private void Timeloaded2(object sender, RoutedEventArgs e)
        {
            dtBlack.Interval = TimeSpan.FromSeconds(1.0);
            dtBlack.Tick += dtTickerBlack;
            dtBlack.Start();
            labelBlack.Content = " ";
            Console.WriteLine(sender + "_" + e);
        }

        public Label GetLabelwhite()
        {
            return labelWhite;
        }

        public Label GetLabelblack()
        {
            return labelBlack;
        }

        private int incrementwhite = 11;
        private void dtTickerWhite(object sender, EventArgs e)
        {
            incrementwhite--;
            labelWhite.Content = incrementwhite.ToString();
            if ((String)labelWhite.Content == "0")
            {
                dtWhite.Stop();
            }
            //dispatcherTimer.Start();
        }

        private int incrementblack = 5;
        private void dtTickerBlack(object sender, EventArgs e)
        {
            incrementblack--;
            labelBlack.Content = incrementblack.ToString();
            if ((String)labelBlack.Content == "0")
            {
                dtBlack.Stop();
            }
            //dispatcherTimer.Start();
        }
    }

}
