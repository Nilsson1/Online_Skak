using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Online_Skak
{
    public class Tower : ButtonClass
    {
        Button towerButton;

        public Tower(int row, int column, int team, MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            towerButton = new Button();

            SetButtonPosition(towerButton, row, column);


            towerButton.PreviewMouseLeftButtonDown += down;
            towerButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(towerButton, row, column);

            towerButton.Name = "Tower_"+team;
            towerButton.Content = SetImage(team, "Rook");

            Form.GridName.Children.Add(towerButton);
        }

        public string GetButtonName()
        {
            return towerButton.Name;
        }

        public Button GetButton()
        {
            return towerButton;
        }

        public bool Move(int row, int col, int desiredRow, int desiredCol)
        {
            if (!(desiredCol == col || desiredRow == row))
            {
                return false;
            }

            return true;
        }
    }
}
