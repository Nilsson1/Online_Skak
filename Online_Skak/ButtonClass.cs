using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Online_Skak
{
    public abstract class ButtonClass
    {
 
        protected MainWindow Form = Application.Current.Windows[0] as MainWindow;


        //Sets the button in the grid according to the desired position.
        protected void SetButtonPosition(Button button, int row, int column)
        {
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
        }

        //Sets the name of a button.
        protected void SetButtonName(Button button, string s)
        {
            button.Name = s;
        }

        //Sets the content of a button.
        protected void SetButtonContent(Button button, int counter)
        {
            string counterString = counter.ToString();
            button.Content = counterString;
        }

        //Sets the fore- and background color of a button.
        protected void SetColor(Button button, Color colorFG, Color colorBG)
        {
            button.Background = new SolidColorBrush(colorBG);
            button.Foreground = new SolidColorBrush(colorFG);
        }

        protected Image SetImage(int team, string chessPiece)
        {
            Image image = new Image();
            if (team == 0)
            {
                image.Source = new BitmapImage(new Uri(@"/Pieces/" + chessPiece + "W.png", UriKind.Relative));
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"/Pieces/" + chessPiece + "B.png", UriKind.Relative));
            }
            return image;
        }

        //Sets the fore- and background color of a button that is created at the start of the game.
        protected void SetDefaultButtonColor(Button button, int i, int j)
        {
            bool defaultButtonIsWhite = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
            if (defaultButtonIsWhite)
            {
                SetColor(button, Colors.White, Colors.DarkGray);
            }
            else
            {
                SetColor(button, Colors.Black, Colors.White);
            }
        }
    }
}
