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
    public class Queen : ButtonClass
    {
        public Queen(int row, int column)
        {
            Button queenButton = new Button();

            SetButtonPosition(queenButton, row, column);


            queenButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            queenButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(queenButton, row, column);

            SetButtonName(queenButton, "Queen");
            queenButton.Content = "Queen";

            Form.GridName.Children.Add(queenButton);
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
