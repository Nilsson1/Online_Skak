using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Online_Skak
{
    public class Pawn : ButtonClass
    {
        Button pawnButton;
        public Pawn(int row, int column, int team, MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            pawnButton = new Button();
            SetButtonPosition(pawnButton, row, column);

            pawnButton.PreviewMouseLeftButtonDown += down;
            pawnButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(pawnButton, row, column);
            pawnButton.Name = "Pawn_" + team;

            pawnButton.Content = SetImage(team, "Pawn");
            //Form.GridName.Children.Add(pawnButton);
        }

        public string GetButtonName()
        {
            return pawnButton.Name;
        }

        public Button GetButton()
        {
            return pawnButton;
        }

        public bool Move(int row, int col, int desiredRow, int desiredCol, string name)
        {
            if (desiredCol != col)
            {
                return false;
            }
            
            if(name == "Pawn_0" && ((desiredRow - row <= 2) && desiredRow - row > 0) && row == 1)
            {
                return true;
            } else
            {
                if (name == "Pawn_0" && ((desiredRow - row <= 1) && desiredRow - row > 0))
                {
                    return true;
                }
            }
            if (name == "Pawn_1" && ((row - desiredRow <= 2) && row - desiredRow > 0) && row == 6)
            {
                return true;
            } else
            {
                if (name == "Pawn_1" && ((row - desiredRow <= 1) && row - desiredRow > 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
