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
    public class Pawn
    {
        private int InitRow;
        private int InitCol;
        private Button buttonSwap;
        private Button element;
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public Pawn(int row, int column)
        {
            Button pawnButton = new Button();
            Grid.SetRow(pawnButton, row);
            Grid.SetColumn(pawnButton, column);

            pawnButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            pawnButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(pawnButton, row, column);

            pawnButton.Name = "Pawn";
            pawnButton.Content = "Pawn";
            Form.GridName.Children.Add(pawnButton);
        }

        public void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;
            InitRow = GetRow(e);
            InitCol = GetColumn(e);
        }

        //This happens when leftmousebutton is released.
        public void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;

            int row = GetRow(e);
            int column = GetColumn(e);

           if (!(ValidMove(row, column, InitRow, InitCol))) return;
        
            buttonSwap = (Button)GetChildren(Form.GridName, row, column);
            element = (Button)(UIElement)e.Source;

            SetButtonPosition(buttonSwap, InitRow, InitCol);
            SetButtonPosition(element, row, column);

            SetButtonColor(InitRow, InitCol, row, column);
        }

        //Sets the button in the grid according to the desired position.
        private void SetButtonPosition(Button button, int row, int column)
        {
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
        }

        //Returns the column.
        private int GetColumn(MouseButtonEventArgs e)
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
        private int GetRow(MouseButtonEventArgs e)
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
        private UIElement GetChildren(Grid grid, int row, int column)
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

        private bool CheckIfBoard(object sender)
        {
            Button s = (Button)sender;
            if (s.Name == "board")
            {
                Console.WriteLine("clicked: " + s.Name);
                return true;
            }
            return false;
        }

        public bool ValidMove(int row, int col, int desiredRow, int desiredCol)
        {
            if (desiredCol != col)
            {
                return false;
            }
            else if(desiredRow - row > 2 || row - desiredRow > 2)
            {
                return false;
            }

            return true;
        }
        private void SetColor(Button button, Color colorFG, Color colorBG)
        {
            button.Background = new SolidColorBrush(colorBG);
            button.Foreground = new SolidColorBrush(colorFG);
        }

        //Sets the fore- and background color of both buttons that are swapping positions.
        private void SetButtonColor(int row1, int col1, int row2, int col2)
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
        private void SetDefaultButtonColor(Button button, int i, int j)
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
