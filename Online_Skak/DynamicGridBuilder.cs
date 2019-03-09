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

        

        Polygon polygon = new Polygon();

        int counter = 0;

        private int InitRow;
        private int InitCol;
        private UIElement InitUE;

        public Button[,] TextBlock()
        {
            Button[,] buttonArray = new Button[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new Button();
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        button.Background = new SolidColorBrush(Colors.Black);
                        button.Foreground = new SolidColorBrush(Colors.White);
                    }

                    else
                    {
                        button.Background = new SolidColorBrush(Colors.White);
                    }



                    string counterString = counter.ToString();



                    button.Content = counterString;

                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    if (i > 1 && i < 6) {
                        button.Name = "board";
                    }else{
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

            Console.WriteLine(InitRow + "," + InitCol);
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

            //UIElement uIElement = GetChildren(Form.GridName, row, column);
            Button button = (Button)GetChildren(Form.GridName, row, column);
            Form.GridName.Children.Remove(button);
            
            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);

            Console.WriteLine(row + "" + column);

            if ((row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0))
            {
                element.Background = new SolidColorBrush(Colors.Black);
                element.Foreground = new SolidColorBrush(Colors.White);

            }

            else
            {
                element.Background = new SolidColorBrush(Colors.White);
                element.Foreground = new SolidColorBrush(Colors.Black);
            }

            Form.GridName.Children.Add(button);

            if ((InitRow % 2 == 0 && InitCol % 2 != 0) || (InitRow % 2 != 0 && InitCol % 2 == 0))
            {
                button.Background = new SolidColorBrush(Colors.Black);
                button.Foreground = new SolidColorBrush(Colors.White);

            }

            else
            {
                button.Background = new SolidColorBrush(Colors.White);
                button.Foreground = new SolidColorBrush(Colors.Black);
            }
            Grid.SetColumn(button, InitCol);
            Grid.SetRow(button, InitRow);

            Console.WriteLine("MouseLeftButtonUp {0} -- {1}", row, column);
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
            return null ;
        }

        private void ClickedButton(object sender, EventArgs e)
        {

            Button s = (Button)sender;
            //MessageBox.Show("you have clicked button:" + s.Name);
        }
    }
}
