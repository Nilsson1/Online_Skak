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
    public class King : ButtonClass
    {
        public King(int row, int column)
        {
            Button kingButton = new Button();

            SetButtonPosition(kingButton, row, column);


            kingButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            kingButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(kingButton, row, column);

            SetButtonName(kingButton, "King");
            kingButton.Content = "King";

            Form.GridName.Children.Add(kingButton);
        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;

            row = GetRow(e);
            column = GetColumn(e);

            if (!(ValidMove(row, column, InitRow, InitCol))) return;

            SwapTwoButtons(e);
        }
    }
}
