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
            labelWhite.Loaded += Time_LoadedWhite;
            labelBlack.Loaded += TimeloadedBlack;

        }

        private void Time_LoadedWhite(object sender, RoutedEventArgs e)
        {
            dtWhite.Interval = TimeSpan.FromSeconds(1.0);
            dtWhite.Tick += DtTickerWhite;
            //dtWhite.Start();
            labelWhite.Content = "10";
            Console.WriteLine(sender + "_" + e);

        }

        private void TimeloadedBlack(object sender, RoutedEventArgs e)
        {
            dtBlack.Interval = TimeSpan.FromSeconds(1.0);
            dtBlack.Tick += DtTickerBlack;
            //dtBlack.Start();
            labelBlack.Content = "10";
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

        private int incrementwhite = 10;
        private void DtTickerWhite(object sender, EventArgs e)
        {
            incrementwhite--;
            labelWhite.Content = incrementwhite.ToString();
            if ((String)labelWhite.Content == "0")
            {
                dtWhite.Stop();
                MessageBox.Show("White has run out of time: Black wins!");
            }
            //dispatcherTimer.Start();
        }

        private int incrementblack = 10;
        private void DtTickerBlack(object sender, EventArgs e)
        {
            incrementblack--;
            labelBlack.Content = incrementblack.ToString();
            if ((String)labelBlack.Content == "0")
            {
                dtBlack.Stop();
                MessageBox.Show("Black has run out of time: White wins!");
            }
            //dispatcherTimer.Start();
        }

        public void TimeStopWhite()
        {
            dtWhite.Stop();
        }

        public void TimeStopBlack()
        {
            dtBlack.Stop();
        }

        public void TimeStartWhite()
        {
            dtWhite.Start();
        }

        public void TimeStartBlack()
        {
            dtBlack.Start();
        }
    }

}
