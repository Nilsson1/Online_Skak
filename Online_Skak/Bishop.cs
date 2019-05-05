using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Online_Skak
{
    public class Bishop : ButtonClass
    {

        Button bishopButton;
        public Bishop(int row, int column, string color, int team, MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            bishopButton = new Button();


            SetButtonPosition(bishopButton, row, column);

            bishopButton.PreviewMouseLeftButtonDown += down; ;
            bishopButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(bishopButton, row, column);

            SetButtonName(bishopButton, "Bishop" + color + "_"+team);
            bishopButton.Content = SetImage(team, "Bishop");

            Form.GridName.Children.Add(bishopButton);

        }

        public string GetButtonName()
        {
            return bishopButton.Name;
        }

        public Button GetButton()
        {
            return bishopButton;
        }

        public bool Move(int row, int col, int desiredRow, int desiredCol)
        {
            if (desiredCol == col || desiredRow == row)
            {
                return false;
            }
            else if (Math.Abs(desiredRow - row) == Math.Abs(desiredCol - col))
            {
                return true;
            }
            return false;
        }
    }
}
