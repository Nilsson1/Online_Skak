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
    public class Pawn : ButtonClass
    {

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public Pawn(int row, int column)
        {
            Button pawnButton = new Button();

            SetButtonPosition(pawnButton, row, column);


            pawnButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            pawnButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(pawnButton, row, column);

            SetButtonName(pawnButton, "Pawn");
            pawnButton.Content = "Pawn";

            Form.GridName.Children.Add(pawnButton);
        }


        //This happens when leftmousebutton is released.
        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

      }
}
