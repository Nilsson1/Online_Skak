using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

            SetButtonName(knightButton, "Knight_" + team);
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/Pieces/KnightB.png", UriKind.Relative));
            image.Source = new BitmapImage(new Uri(@"/Pieces/KnightW.png", UriKind.Relative));
            knightButton.Content = image;

            Form.GridName.Children.Add(knightButton);
        }

        public string GetButtonName()
        {
            return knightButton.Name;
        }

        public Button GetButton()
        {
            return knightButton;
        }
        public void SetImage()
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/Pieces/KnightB.png", UriKind.Relative));
            image.Source = new BitmapImage(new Uri(@"/Pieces/KnightW.png", UriKind.Relative));
        }

        public bool Move(int row, int col, int desiredRow, int desiredCol)
        {
            int tempRow = (Math.Abs(desiredRow - row));
            int tempCol = (Math.Abs(desiredCol - col));
            if (tempRow == 2 && tempCol == 1)
            {
                return true;
            }
            else if (tempCol == 2 && tempRow == 1)
            {
                return true;
            }
            return false;

        }
    }
}
