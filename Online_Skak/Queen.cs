using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

            SetButtonName(queenButton, "Queen_" + team);
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/Pieces/PawnB.png", UriKind.Relative));
            image.Source = new BitmapImage(new Uri(@"/Pieces/PawnW.png", UriKind.Relative));
            queenButton.Content = image;

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

        public string GetButtonName()
        {
            return queenButton.Name;
        }

        public Button GetButton()
        {
            return queenButton;
        }

        public void SetImage()
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/Pieces/QueenB.png", UriKind.Relative));
            image.Source = new BitmapImage(new Uri(@"/Pieces/QueenW.png", UriKind.Relative));
        }

    }
}
