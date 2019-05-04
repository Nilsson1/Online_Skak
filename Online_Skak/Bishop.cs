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
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/Pieces/BishopB.png", UriKind.Relative));
            image.Source = new BitmapImage(new Uri(@"/Pieces/BishopW.png", UriKind.Relative));
            bishopButton.Content = image;

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

        public void SetImage()
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"/Pieces/BishopB.png", UriKind.Relative))
            image.Source = new BitmapImage(new Uri(@"/Pieces/BishopW.png", UriKind.Relative)   ;
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
