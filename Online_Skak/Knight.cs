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
    public class Knight : ButtonClass
    {
        public Knight(int row, int column)
        {
            Button knightButton = new Button();

            SetButtonPosition(knightButton, row, column);


            knightButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            knightButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(knightButton, row, column);

            SetButtonName(knightButton, "Knight");
            knightButton.Content = "Knight";

            Form.GridName.Children.Add(knightButton);
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
