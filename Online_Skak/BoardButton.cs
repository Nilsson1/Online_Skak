using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class BoardButton : ButtonClass
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public BoardButton(int row, int column, int counter)
        {
            Button button = new Button();
            SetDefaultButtonColor(button, row, column);
            SetButtonPosition(button, row, column);

            SetButtonName(button, "board");

            button.Content = counter;

            button.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
         
            Form.GridName.Children.Add(button);
        }
    }
}
