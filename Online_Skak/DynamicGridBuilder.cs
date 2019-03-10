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
        private UIElement InitUE;
        private bool defaultButtonIsBlack;
        private bool firstButtonIsBlack;
        private bool secondButtonIsBlack;
        private Button buttonSwap;

        public Button[,] CreateBoardButtons()
        {
            Button[,] buttonArray = new Button[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new Button();

                    defaultButtonIsBlack = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                    if (defaultButtonIsBlack)
                    {
                        SetColor(button, "Black");
                    }
                    else
                    {
                        SetColor(button, "White");
                    }
          
                    string counterString = counter.ToString();
                    
                    button.Content = counterString;

                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    if (i > 1 && i < 6)
                    {
                        button.Name = "board";
                        //button.IsEnabled = false;
                    }
                    else
                    {
                        button.Name = "boardpiece";
                    }

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

        private void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button s = (Button)sender;
            if (s.Name == "board")
            {
                Console.WriteLine("clicked: " + s.Name);
                return;
            }
            InitUE = (UIElement)e.Source;
            InitCol = Grid.GetColumn(InitUE);
            InitRow = Grid.GetRow(InitUE);
        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button s = (Button)sender;
            if (s.Name == "board")
            {
                Console.WriteLine("clicked: " + s.Name);
                return;
            }

            Button element = (Button)(UIElement)e.Source;

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
           
            buttonSwap = (Button)GetChildren(Form.GridName, row, column);

            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);

            Grid.SetColumn(buttonSwap, InitCol);
            Grid.SetRow(buttonSwap, InitRow);

            firstButtonIsBlack = (InitRow % 2 == 0 && InitCol % 2 != 0) || (InitRow % 2 != 0 && InitCol % 2 == 0);
            secondButtonIsBlack = (row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0);

            if (firstButtonIsBlack)
            {
                SetColor(buttonSwap, "Black");
            }
            else
            {
                SetColor(buttonSwap, "White");
            }

            if (secondButtonIsBlack)
            {
                SetColor(element, "Black");
            }
            else
            {
                SetColor(element, "White");
            }
        }

        private UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row
                      &&
                   Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            Console.WriteLine("Returned null in GetChildren");
            return null;
        }

        private void ClickedButton(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            //MessageBox.Show("you have clicked button:" + s.Name);
        }

        private void SetColor(Button button, string ButtonColor)
        {
            if (ButtonColor == "White")
            {
                button.Background = new SolidColorBrush(Colors.White);
                button.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                button.Background = new SolidColorBrush(Colors.Black);
                button.Foreground = new SolidColorBrush(Colors.White);
            }
        }
    }
}
