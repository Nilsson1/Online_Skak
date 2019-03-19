using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class King : ButtonClass
    {
        Button kingButton;
        public King(int row, int column, int team , MouseButtonEventHandler up, MouseButtonEventHandler down)
        {
            kingButton = new Button();

            SetButtonPosition(kingButton, row, column);


            kingButton.PreviewMouseLeftButtonDown += down;
            kingButton.PreviewMouseLeftButtonUp += up;

            SetDefaultButtonColor(kingButton, row, column);

            SetButtonName(kingButton, "King" + team);
            kingButton.Content = "King";

            Form.GridName.Children.Add(kingButton);
        }

        public string GetButton()
        {
            return kingButton.Name;
        }
    }
}
