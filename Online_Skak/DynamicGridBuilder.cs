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
    public class DynamicGridBuilder : ButtonClass
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        private int counter = 0;
        private Pawn pawn;
        private Tower tower;
        private Bishop bishop;
        //Creates 64 buttons for the chess board.
        public Button[,] CreateBoardButtons()
        {
            Button[,] buttonArray = new Button[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (row == 1 || row == 6)
                    {
                        pawn = new Pawn(row, column);
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 0) || (row == 0 && column == 7))
                    {
                        tower = new Tower(row, column);
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 2) || (row == 0 && column == 5))
                    {
                        bishop = new Bishop(row, column);
                        counter++;
                        continue;
                    }

                    BoardButton boardButton = new BoardButton(row, column, counter);
                    counter++;

                }
            }
            return buttonArray;
        }
    }
}
