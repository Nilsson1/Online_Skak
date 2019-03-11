using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Online_Skak
{
    public abstract class ButtonClass
    {
        protected int InitRow;
        protected int InitCol;
        protected Button buttonSwap;
        protected Button element;
 
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        protected void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;
            InitRow = GetRow(e);
            InitCol = GetColumn(e);
        }

        //Sets the button in the grid according to the desired position.
        protected void SetButtonPosition(Button button, int row, int column)
        {
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
        }

        //Checks if the current button is an empty board or a moveable chesspiece.
        protected bool CheckIfBoard(object sender)
        {
            Button s = (Button)sender;
            if (s.Name == "board")
            {
                Console.WriteLine("clicked: " + s.Name);
                return true;
            }
            return false;
        }

        //Returns the column.
        protected int GetColumn(MouseButtonEventArgs e)
        {
            double x = e.GetPosition(Form.GridName).X;
            double cstart = 0.0;
            int column = 0;
            foreach (ColumnDefinition cd in Form.GridName.ColumnDefinitions)
            {
                cstart += cd.ActualWidth;
                if (x < cstart)
                {
                    break;
                }
                column++;
            }
            return column;
        }

        //Returns the row.
        protected int GetRow(MouseButtonEventArgs e)
        {
            double y = e.GetPosition(Form.GridName).Y;
            double start = 0.0;
            int row = 0;
            foreach (RowDefinition rd in Form.GridName.RowDefinitions)
            {
                start += rd.ActualHeight;
                if (y < start)
                {
                    break;
                }
                row++;
            }
            return row;
        }

        //Returns the button to swap with.
        protected UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row
                      &&
                   Grid.GetColumn(child) == column)
                {
                    //Console.WriteLine(child);
                    return child;
                }
            }
            Console.WriteLine("Returned null in GetChildren");
            return null;
        }

        //This happens when a button is clicked, but it doesnt do anything, but could be used. 
        protected void ClickedButton(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            //MessageBox.Show("you have clicked button:" + s.Name);
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

        //Sets the fore- and background color of both buttons that are swapping positions.
        protected void SetButtonColor(int row1, int col1, int row2, int col2)
        {
            bool firstButtonIsWhite = (row1 % 2 == 0 && col1 % 2 != 0) || (row1 % 2 != 0 && col1 % 2 == 0);
            bool secondButtonIsWhite = (row2 % 2 == 0 && col2 % 2 != 0) || (row2 % 2 != 0 && col2 % 2 == 0);

            if (firstButtonIsWhite)
            {
                SetColor(buttonSwap, Colors.White, Colors.Black);
            }
            else
            {
                SetColor(buttonSwap, Colors.Black, Colors.White);
            }

            if (secondButtonIsWhite)
            {
                SetColor(element, Colors.White, Colors.Black);
            }
            else
            {
                SetColor(element, Colors.Black, Colors.White);
            }
        }

        //Sets the fore- and background color of a button that is created at the start of the game.
        protected void SetDefaultButtonColor(Button button, int i, int j)
        {
            bool defaultButtonIsWhite = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
            if (defaultButtonIsWhite)
            {
                SetColor(button, Colors.White, Colors.Black);
            }
            else
            {
                SetColor(button, Colors.Black, Colors.White);
            }
        }
    }
}
