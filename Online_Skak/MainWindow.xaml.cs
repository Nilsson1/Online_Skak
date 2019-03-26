
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
            dynamicGridBuilder = new DynamicGridBuilder();
            var name = dynamicGridBuilder.CreateBoardButtons();

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Console.WriteLine(name[i, j]);
                }
            }
        }
    }
}
