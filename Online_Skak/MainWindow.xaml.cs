
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
        Class1 test = new Class1 {Name = "Lukas er noob"};
        
        public MainWindow()
        {
            InitializeComponent();
            dynamicGridBuilder = new DynamicGridBuilder();
            dynamicGridBuilder.TextBlock();
            this.DataContext = test;
                
        }
    }
}
