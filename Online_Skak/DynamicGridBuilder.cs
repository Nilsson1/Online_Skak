using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Online_Skak
{
    public class DynamicGridBuilder
    {
        private int counter = 0;
        private int InitRow, InitCol, row, column;
        private Button buttonSwap;
        private Button element;
        private Pawn pawn;
        private Tower tower;
        private Knight knight;
        private Bishop bishop;
        private King king;
        private Queen queen;

        string[,] objectArray = new string[8, 8];

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        //Creates 64 buttons for the chess board.
        public string[,] CreateBoardButtons()
        {
            Button[,] buttonArray = new Button[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (row == 1)
                    {
                        pawn = new Pawn(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        counter++;
                        objectArray[row, column] = pawn.GetButton();
                        Console.WriteLine(pawn.GetButton());
                        continue;
                    }
                    if(row == 6)
                    {
                        pawn = new Pawn(row, column, 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        counter++;
                        objectArray[row, column] = pawn.GetButton();
                        Console.WriteLine(pawn.GetButton());
                        continue;
                    }
                    else if ((row == 0 && column == 0) || (row == 0 && column == 7))
                    {
                        tower = new Tower(row, column, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = tower.GetButton();
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 1) || (row == 0 && column == 6))
                    {
                        knight = new Knight(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = knight.GetButton();
                        counter++;
                        continue;
                    }
                    else if (row == 0 && column == 2)
                    {
                        bishop = new Bishop(row, column,"White", 0,  Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = bishop.GetButton();
                        counter++;
                        continue;
                    }
                    else if(row == 0 && column == 5)
                    {
                        bishop = new Bishop(row, column, "Black", 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = bishop.GetButton();
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 4))
                    {
                        king = new King(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = king.GetButton();
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 3))
                    {
                        queen = new Queen(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = queen.GetButton();
                        counter++;
                        continue;
                    }
                    BoardButton boardButton = new BoardButton(row, column, counter, Btn_PreviewMouseLeftButtonDown);
                    objectArray[row, column] = boardButton.ToString();
                    counter++;
                }
            }
            return objectArray;
        }

        //This is called whenever a button is pressed down.
        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            row = GetRow(e);
            column = GetColumn(e);

            if (CheckIfBoard(sender)) return;

            Button b = (Button)sender;
            string s = b.Name;

           // Console.WriteLine(row + "" + column + "" + InitRow + "" + InitCol);
            if(InitRow != row || InitCol != column)
            {
                if (MoveChessPiece(InitRow, InitCol, row, column, s))
                {
                    SwapTwoButtons(e);
                    SetButtonColor(InitRow, InitCol, row, column);
                }
            }
        }

        //This is called whenever a button is released (no longer pressed down).
        private void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;
            InitRow = GetRow(e);
            InitCol = GetColumn(e);
        }

        //Swap the two buttons chosen by Btn_PreviewMouseLeftButtonUp and Btn_PreviewMouseLeftButtonDown.
        private void SwapTwoButtons(MouseButtonEventArgs e)
        {
            buttonSwap = (Button)GetChildren(Form.GridName, row, column);
            element = (Button)(UIElement)e.Source;

            SetButtonPosition(element, row, column);
            Form.GridName.Children.Remove(buttonSwap);
            BoardButton button = new BoardButton(InitRow, InitCol, 0, Btn_PreviewMouseLeftButtonDown);

            objectArray[InitRow, InitCol] = button.ToString();
            objectArray[row, column] = element.Name;

            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);
        }

        //Get all children in the grid
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

        //Sets the position of the button in the grid(parameter).
        private void SetButtonPosition(Button button, int row, int column)
        {
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
        }

        //Set the color of a button (both foreground and background).
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

        //Check if we have clicked a board and not a chesspiece.
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

        //Returns if we have proposed a legal move of a chess piece following the ruleset (true/false).
        private bool MoveChessPiece(int row, int col, int desiredRow, int desiredCol, string s)
        {
            bool b, c;
           
            switch (s)
            {
                case "Tower0":
                    b = tower.Move(row, col, desiredRow, desiredCol);
                    c = TowerMove(row, col, desiredRow, desiredCol);
                    if (b && c) return true;
                    return false;

                case "Pawn0":
                case "Pawn1":
                    b = pawn.Move(row, col, desiredRow, desiredCol, s);
                    c = PawnMove(row, col, desiredRow, desiredCol);
                    if (b && c) return true;
                    return false;

                case "BishopWhite0":
                    b = bishop.Move(row, col, desiredRow, desiredCol);
                    c = BishopMove(row, col, desiredRow, desiredCol, "White");
                    if (b && c) return true;
                    return false;

                case "BishopBlack0":
                    b = bishop.Move(row, col, desiredRow, desiredCol);
                    c = BishopMove(row, col, desiredRow, desiredCol, "Black");
                    if (b && c) return true;
                    return false;

                case "Knight":
                    //return KnightMove(row, col, desiredRow, desiredCol);

                case "King":
                    //return KingMove(row, col, desiredRow, desiredCol);

                case "Queen0":
                    if((InitRow % 2 == 0 && InitCol % 2 != 0) || (InitRow % 2 != 0 && InitCol % 2 == 0))
                    {
                        c = QueenMove(row, col, desiredRow, desiredCol, "Black");
                    }
                    else
                    {
                        c = QueenMove(row, col, desiredRow, desiredCol, "White");
                    }
                    b = queen.Move(row, col, desiredRow, desiredCol);
                    if (b && c) return true;
                    return false;

                default:
                    return false;
            }
        }

        //Adds the chesspiece to the list if its "in the way" of the proposed move.
        private void AddChessPieceToList(int i, int j, List<String> list, string s)
        {
            s = objectArray[i, j];

            char lastSecond = s[s.Length - 1];

            if (lastSecond != 'n')
            {
                list.Add(s);
            }
        }

        //Checks how we want to move around on the chessboard, as we have to swap the rows/columns around based on which direction of the board we want to move (because of the way its implemented in the Move functions).
        private string[] CheckBool(int InitRow, int InitCol, int row, int column)
        {
            bool b1 = true;
            bool b2 = true;

            string[] list = new string[6];

            if (InitRow > row && InitCol > column)
            {
                int temp = row;
                row = InitRow;
                InitRow = temp;
                b1 = false;
                int temp2 = column;
                column = InitCol;
                InitCol = temp2;
                b2 = false;
            }
            if (InitRow > row && InitCol < column)
            {
                int temp = row;
                row = InitRow;
                InitRow = temp;
                b1 = false;
            }
            if (InitRow < row && InitCol > column)
            {

                int temp2 = column;
                column = InitCol;
                InitCol = temp2;
                b2 = false;

            }
            if (InitRow < row && InitCol < column)
            {
                //do nothing, default is true true
            }
            if(InitRow == row && InitCol < column)
            {
                // do nothing
            }
            if(InitRow < row && InitCol == column)
            {
                //do nothing
            }
            if(InitRow == row && InitCol > column)
            {
                int temp2 = column;
                column = InitCol;
                InitCol = temp2;
                b2 = false;
            }
            if(InitRow > row && InitCol == column)
            {
                int temp = row;
                row = InitRow;
                InitRow = temp;
                b1 = false;
            }

            list[0] = b1.ToString();
            list[1] = b2.ToString();
            list[2] = InitRow.ToString();
            list[3] = InitCol.ToString();
            list[4] = row.ToString();
            list[5] = column.ToString();

            return list;
        }
    
        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool BishopMove(int InitRow, int InitCol, int row, int column, string color)
        {
            List<String> list = new List<String>();
            string[] listNew;

            bool b1 = true;
            bool b2 = true;

            listNew = CheckBool(InitRow, InitCol, row, column);

            b1 = Boolean.Parse(listNew[0]);
            b2 = Boolean.Parse(listNew[1]);
            InitRow = Int32.Parse(listNew[2]);
            InitCol = Int32.Parse(listNew[3]);
            row = Int32.Parse(listNew[4]);
            column = Int32.Parse(listNew[5]);

            string click = objectArray[row, column];
            char lastClick = click[click.Length - 1];

            bool bishopWhite;

            Console.WriteLine(b1+ ""+ b2);

            for (int i = InitRow; i <= row; i++)
            {
                for (int j = InitCol; j <= column; j++)
                {
                    if (color == "White")
                    {
                        bishopWhite = ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0));
                    }
                    else
                    {
                        bishopWhite = ((j % 2 != 0 && i % 2 == 0) || (j % 2 == 0 && i % 2 != 0));
                    }
                    if (bishopWhite)
                    {
                        if (b1 == true && b2 == true)
                        {
                            if ((row - i == column - j))
                            {
                                AddChessPieceToList(i, j, list, objectArray[i, j]);
                            }
                        }
                        else if (b1 == true && b2 == false)
                        {
                            if (row - i == j - InitCol)
                            {
                                AddChessPieceToList(i, j, list, objectArray[i, j]);
                            }
                        }
                        else if (b1 == false && b2 == false)
                        {
                            if (i - InitRow == j - InitCol)
                            {
                                AddChessPieceToList(i, j, list, objectArray[i, j]);
                            }
                        }
                        else
                        {
                            if (i - InitRow == column - j)
                            {
                                AddChessPieceToList(i, j, list, objectArray[i, j]);
                            }
                        }
                    }
                }
            }
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click);
        }

        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool TowerMove(int InitRow, int InitCol, int row, int column)
        {
            List<String> list = new List<String>();
            string[] listNew;

            bool b1 = true;
            bool b2 = true;
            
            listNew = CheckBool(InitRow, InitCol, row, column);

            b1 = Boolean.Parse(listNew[0]);
            b2 = Boolean.Parse(listNew[1]);
            InitRow = Int32.Parse(listNew[2]);
            InitCol = Int32.Parse(listNew[3]);
            row = Int32.Parse(listNew[4]);
            column = Int32.Parse(listNew[5]);

            string click = objectArray[row, column];
            char lastClick = click[click.Length - 1];

            for (int i = InitRow; i <= row; i++)
            {
                for (int j = InitCol; j <= column; j++)
                {
                    AddChessPieceToList(i, j, list, objectArray[i, j]);
                }
            }
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click);
        }

        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool PawnMove(int InitRow, int InitCol, int row, int column)
        {
            List<String> list = new List<String>();
            string[] listNew;

            bool b1 = true;
            bool b2 = true;

            listNew = CheckBool(InitRow, InitCol, row, column);

            b1 = Boolean.Parse(listNew[0]);
            b2 = Boolean.Parse(listNew[1]);
            InitRow = Int32.Parse(listNew[2]);
            InitCol = Int32.Parse(listNew[3]);
            row = Int32.Parse(listNew[4]);
            column = Int32.Parse(listNew[5]);

            string click = objectArray[row, column];
            char lastClick = click[click.Length - 1];

            for (int i = InitRow; i <= row; i++)
            {
                for (int j = InitCol; j <= column; j++)
                {
                    AddChessPieceToList(i, j, list, objectArray[i, j]);
                }
            }
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click);
        }

        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool QueenMove(int InitRow, int InitCol, int row, int column, string color)
        {
            if (InitRow == row || InitCol == column)
            {
                return TowerMove(InitRow, InitCol, row, column);
            }
            else
            {
                return BishopMove(InitRow, InitCol, row, column, color);
            }
        }

        //Checks the list provided by the ".Move" functions above and see if they contain any other chesspiece than the one wanting to move. If it does, it checks if it contains a piece from the opponent team on the distination tile. 
        //Returns true if this is the case.
        private bool CheckIfMoveIsLegal(List<string> list, bool b1, bool b2, char lastClick, string click)
        {

            StackTrace stackTrace = new StackTrace();

            // Get calling method name
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            string s;

            click = objectArray[row, column];
            lastClick = click[click.Length - 1];

            foreach(string s1 in list)
            {
                Console.WriteLine(s1);
            }
            Console.WriteLine(b1 + "" + b2);

            if (list.Count == 2)
            {
                if (b1 == true && b2 == true)
                {
                    s = list[1];
                    if ((s[s.Length - 1] == '1') && lastClick != 'n')
                    {
                        list.Remove(list[1]);
                    }
                }
                else if (b1 == false && b2 == true)
                {
                    s = list[0];
                    if ((s[s.Length - 1] == '1') && lastClick != 'n')
                    {
                        list.Remove(list[0]);
                    }

                }
                else if (b1 == true && b2 == false)
                {
                    if(callingMethod == "TowerMove")
                    {
                        s = list[0];
                    }
                    else
                    {
                        s = list[1];
                    }
                    
                    if ((s[s.Length - 1] == '1') && lastClick != 'n')
                    {
                         list.Remove(list[1]);
                    }
                }
                else if(b1 == false && b2 == false)
                {
                    s = list[0];
                    if ((s[s.Length - 1] == '1') && lastClick != 'n')
                    {
                        list.Remove(list[1]);
                    }
                }
            }
            if (list.Count <= 1)
            {
                return true;
            }
            return false;
        }
    }
}
