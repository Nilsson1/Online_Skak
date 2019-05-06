using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class BoardButton : ButtonClass
    {
        Button button;
        public BoardButton(int row, int column, int counter, MouseButtonEventHandler down)
        {
            button = new Button();
            SetDefaultButtonColor(button, row, column);
            SetButtonPosition(button, row, column);

            SetButtonName(button, "board");

            //button.Content = counter;

            button.PreviewMouseLeftButtonDown += down;
         
            //Form.GridName.Children.Add(button);
        }

        public Button GetButton()
        {
            return button;
        }
    }
}
