using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class Queen : ButtonClass
    {
        Button queenButton;
        public Queen(int row, int column, int team, MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            queenButton = new Button();

            SetButtonPosition(queenButton, row, column);


            queenButton.PreviewMouseLeftButtonDown += down;
            queenButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(queenButton, row, column);

            SetButtonName(queenButton, "Queen" + team);
            queenButton.Content = "Queen";

            Form.GridName.Children.Add(queenButton);
        }

        public bool Move(int row, int col, int desiredRow, int desiredCol)
        {
            int tempRow = (Math.Abs(desiredRow - row));
            int tempCol = (Math.Abs(desiredCol - col));
            if (tempRow == tempCol)
            {
                return true;
            }
            else if ((desiredCol == col || desiredRow == row) && (tempRow < 2 || tempCol < 2))
            {
                return true;
            }

            return false;
        }

        public string GetButton()
        {
            return queenButton.Name;
        }


    }
}
