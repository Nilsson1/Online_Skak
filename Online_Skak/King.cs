using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class King : ButtonClass
    {
        Button kingButton;
        public King(int row, int column, int team , MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            kingButton = new Button();

            SetButtonPosition(kingButton, row, column);


            kingButton.PreviewMouseLeftButtonDown += down;
            kingButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(kingButton, row, column);

            SetButtonName(kingButton, "King_" + team);
            kingButton.Content = "King " + team;

            Form.GridName.Children.Add(kingButton);
        }

        public string GetButtonName()
        {
            return kingButton.Name;
        }

        public Button GetButton()
        {
            return kingButton;
        }


        public bool Move(int row, int col, int desiredRow, int desiredCol)
        {
            int tempRow = (Math.Abs(desiredRow - row));
            int tempCol = (Math.Abs(desiredCol - col));
            if (tempRow == tempCol && tempRow < 2)
            {
                return true;
            }
            else if ((desiredCol == col || desiredRow == row) && (tempRow < 2 && tempCol < 2))
            {
                return true;
            }
            return false;
        }
    }
}
