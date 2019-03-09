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

        Button [,] buttonArray = new Button[8, 8];

        int counter = 0;

        private int InitRow;
        private int InitCol;
        private UIElement InitUE;

        public void TextBlock()
        {
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
                    button.Name = "btn" + counterString;
                   
                    button.Click += new RoutedEventHandler(ClickedButton);
                    button.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
                    button.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

                    Form.GridName.Children.Add(button);
                    buttonArray[i, j] = button;
                    counter++;
                }
            }
        }

        private void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            InitUE = (UIElement)e.Source;
            InitCol = Grid.GetColumn(InitUE);
            InitRow = Grid.GetRow(InitUE);

            Console.WriteLine(InitRow + "," + InitCol);
        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;

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

            UIElement uIElement = GetChildren(Form.GridName, row, column);

            Form.GridName.Children.Remove(uIElement);
            
            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);

            Form.GridName.Children.Add(uIElement);

            Grid.SetColumn(uIElement, InitCol);
            Grid.SetRow(uIElement, InitRow);

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
