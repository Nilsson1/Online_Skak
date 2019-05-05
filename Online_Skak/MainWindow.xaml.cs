
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Online_Skak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetSource("Page1.xaml");
        }
        public void SetSource(string URL)
        {
            frame.Source = new Uri(URL, UriKind.RelativeOrAbsolute);
        }
    }
}
