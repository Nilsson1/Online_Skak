using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Online_Skak
{
    public class DynamicGridBuilder
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        private int counter = 0;
        private int InitRow;
        private int InitCol;
        private bool firstButtonIsBlack;
        private bool secondButtonIsBlack;
        private Button buttonSwap;
        private Button element;

        //Creates 64 buttons for the chess board.
        public Button[,] CreateBoardButtons()
        {
            Button[,] buttonArray = new Button[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new Button();

                    SetDefaultButtonColor(button, i, j);
                    SetButtonPosition(button, i, j);
                    SetButtonName(button, i);
                    SetButtonContent(button, counter);

                    button.Click += new RoutedEventHandler(ClickedButton);

                    button.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
                    button.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

                    Form.GridName.Children.Add(button);

                    counter++;
                    buttonArray[i, j] = button;
                }
            }
            return buttonArray;
        }

        //This happens when leftmousebutton is held down.
        private void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;
            InitRow = GetRow(e);
            InitCol = GetColumn(e);
        }

        //This happens when leftmousebutton is released.
        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;

            int row = GetRow(e);
            int column = GetColumn(e);

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

        //Checks if the current button is an empty board or a moveable chesspiece.
        private bool CheckIfBoard(object sender){
            Button s = (Button)sender;     
            if (s.Name == "board")
            {
                Console.WriteLine("clicked: " + s.Name);
                return true;
            }
            return false;
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

        //This happens when a button is clicked, but it doesnt do anything, but could be used. 
        private void ClickedButton(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            //MessageBox.Show("you have clicked button:" + s.Name);
        }

        //Sets the name of a button.
        private void SetButtonName(Button button, int i)
        {
            if (i > 1 && i < 6)
            {
                button.Name = "board";
                //button.IsEnabled = false;
            }
            else
            {
                button.Name = "boardpiece";
            }
        }

        //Sets the content of a button.
        private void SetButtonContent(Button button, int counter)
        {
            string counterString = counter.ToString();
            button.Content = counterString;
        }

        //Sets the fore- and background color of a button.
        private void SetColor(Button button, Color colorFG, Color colorBG)
        {
                button.Background = new SolidColorBrush(colorBG);
                button.Foreground = new SolidColorBrush(colorFG);
        }

        //Sets the fore- and background color of both buttons that are swapping positions.
        private void SetButtonColor(int row1, int col1, int row2, int col2) 
        {
            firstButtonIsBlack = (row1 % 2 == 0 && col1 % 2 != 0) || (row1 % 2 != 0 && col1 % 2 == 0);
            secondButtonIsBlack = (row2 % 2 == 0 && col2 % 2 != 0) || (row2 % 2 != 0 && col2 % 2 == 0);

            if (firstButtonIsBlack)
            {
                SetColor(buttonSwap, Colors.Black, Colors.White);
            }
            else
            {
                SetColor(buttonSwap, Colors.White, Colors.Black);
            }

            if (secondButtonIsBlack)
            {
                SetColor(element, Colors.Black, Colors.White);
            }
            else
            {
                SetColor(element, Colors.White, Colors.Black);
            }
        }

        //Sets the fore- and background color of a button that is created at the start of the game.
        private void SetDefaultButtonColor(Button button, int i, int j)
        {
            bool defaultButtonIsBlack = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
            if (defaultButtonIsBlack)
            {
                SetColor(button, Colors.Black, Colors.White);
            }
            else
            {
                SetColor(button, Colors.White, Colors.Black);
            }
        }
    }
}
