using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class Tower : ButtonClass
    {
        Button towerButton;

        public Tower(int row, int column, MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            towerButton = new Button();

            SetButtonPosition(towerButton, row, column);


            towerButton.PreviewMouseLeftButtonDown += down;
            towerButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(towerButton, row, column);

            towerButton.Name = "Tower0";
            towerButton.Content = "Tower";

            Form.GridName.Children.Add(towerButton);
        }

        public string GetButton()
        {
            return towerButton.Name;
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
