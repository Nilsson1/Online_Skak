using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class Knight : ButtonClass
    {
        Button knightButton;
        public Knight(int row, int column, int team, MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            knightButton = new Button();

            SetButtonPosition(knightButton, row, column);


            knightButton.PreviewMouseLeftButtonDown += down;
            knightButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(knightButton, row, column);

            SetButtonName(knightButton, "Knight" + team);
            knightButton.Content = "Knight";

            Form.GridName.Children.Add(knightButton);
        }

        public string GetButton()
        {
            return knightButton.Name;
        }
    }
}
