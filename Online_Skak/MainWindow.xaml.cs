﻿
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Online_Skak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        DynamicGridBuilder dynamicGridBuilder = null;
        public MainWindow()
        {
            InitializeComponent();

            Menu test = new Menu();
            

            dynamicGridBuilder = new DynamicGridBuilder();
            var name = dynamicGridBuilder.CreateBoardButtons();
        }
    }
}
