using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Online_Skak
{
    public class DynamicGridBuilder
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        public void TextBlock()
        { 
            for(int i = 0; i<8; i++) 
            { 
                for(int j = 1; j< 9; j++) 
                { 
                    Button txt = new Button();
                    txt.Content = "Cell: " + i + "," + j;
                    Grid.SetRow(txt, i);
                    Grid.SetColumn(txt, j);
                    Form.GridName.Children.Add(txt);
                }
            }
        }
    }
}

