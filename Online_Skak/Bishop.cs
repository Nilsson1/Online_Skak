using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    class Bishop : ButtonClass
    {

        public Bishop(int row, int column)
        {
            Button bishopButton = new Button();


            SetButtonPosition(bishopButton, row, column);

            bishopButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            bishopButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(bishopButton, row, column);

            SetButtonName(bishopButton, "Bishop");
            bishopButton.Content = "Bishop";

            Form.GridName.Children.Add(bishopButton);

        }
        //This happens when leftmousebutton is released.
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
