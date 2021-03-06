﻿using System;
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
            DynamicGridBuilder d = new DynamicGridBuilder(false);
            d.CreateBoardButtons();

        }

        private void PlayButton_Time(object sender, RoutedEventArgs e)
        {
            DynamicGridBuilder d = new DynamicGridBuilder(true);
            d.CreateBoardButtons();

        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
           Application.Current.MainWindow.Close();
        }
    }
}
