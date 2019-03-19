using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class BoardButton : ButtonClass
    {
        public BoardButton(int row, int column, int counter, MouseButtonEventHandler down)
        {
            Button button = new Button();
            SetDefaultButtonColor(button, row, column);
            SetButtonPosition(button, row, column);

            SetButtonName(button, "board");

            button.Content = counter;

            button.PreviewMouseLeftButtonDown += down;
         
            Form.GridName.Children.Add(button);
        }
    }
}
