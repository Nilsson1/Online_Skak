using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Online_Skak
{
    
    public class DynamicGridBuilder
    {
        ServiceHandler serviceHandler;
        private int counter = 0;
        private int InitRow, InitCol, row, column;
        private Button buttonSwap;
        private Pawn pawn;
        private Tower tower;
        private Knight knight;
        private Bishop bishop;
        private King king;
        private Queen queen;
        private int team;
        private int roundGameCounter = 0;
        private Button b;
        private bool classMoveBool;
        string[,] objectArray = new string[8, 8];
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

      
        public DynamicGridBuilder()
        {
            serviceHandler = new ServiceHandler();
            serviceHandler.MessageRecieved += HandleMessage;
        }

        public void HandleMessage(string message)
        {
            Console.WriteLine(message);
            string[] splitMsg = message.Split(',');

            Button b = new Button();
            string sender = splitMsg[0];
            //Console.WriteLine("handlesender: " + sender);

            InitRow = Int32.Parse(splitMsg[1]);
            InitCol = Int32.Parse(splitMsg[2]);
            row = Int32.Parse(splitMsg[3]);
            column = Int32.Parse(splitMsg[4]);

            b = SwapTwoButtons(sender);
            SetButtonColor(b, InitRow, InitCol, row, column);
            roundGameCounter++;
        }

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
                        objectArray[row, column] = pawn.GetButtonName();
                        continue;
                    }
                    if(row == 6)
                    {
                        pawn = new Pawn(row, column, 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        counter++;
                        objectArray[row, column] = pawn.GetButtonName();
                        continue;
                    }
                    else if ((row == 0 && column == 0) || (row == 0 && column == 7))
                    {
                        tower = new Tower(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = tower.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 7 && column == 0)|| (row == 7 && column == 7))
                    {
                        tower = new Tower(row, column, 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = tower.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 1) || (row == 0 && column == 6))
                    {
                        knight = new Knight(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = knight.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 7 && column == 1) || (row == 7 && column == 6))
                    {
                        knight = new Knight(row, column, 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = knight.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if (row == 0 && column == 2)
                    {
                        bishop = new Bishop(row, column,"White", 0,  Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = bishop.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if(row == 0 && column == 5)
                    {
                        bishop = new Bishop(row, column, "Black", 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = bishop.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if (row == 7 && column == 5)
                    {
                        bishop = new Bishop(row, column, "White", 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = bishop.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if (row == 7 && column == 2)
                    {
                        bishop = new Bishop(row, column, "Black", 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = bishop.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 4))
                    {
                        king = new King(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = king.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 7 && column == 4))
                    {
                        king = new King(row, column, 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = king.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 0 && column == 3))
                    {
                        queen = new Queen(row, column, 0, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = queen.GetButtonName();
                        counter++;
                        continue;
                    }
                    else if ((row == 7 && column == 3))
                    {
                        queen = new Queen(row, column, 1, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                        objectArray[row, column] = queen.GetButtonName();
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
            Console.WriteLine("sender: " + sender);
            Button btn = new Button();
            
            row = GetRow(e);
            column = GetColumn(e);

            if (CheckIfBoard(sender)) return;
            b = (Button)sender;
            string s = b.Name;

            FindChessPieceTeam(s);

            if ((InitRow != row || InitCol != column))
            {
                if(team == roundGameCounter % 2)
                {
                    if (MoveChessPiece(InitRow, InitCol, row, column, s))
                    {
                        btn = SwapTwoButtons(s);
                        SetButtonColor(btn, InitRow, InitCol, row, column);
                        roundGameCounter++;
                        serviceHandler.SendMessage(btn.Name + "," + InitRow.ToString() + "," + InitCol.ToString() + "," + row.ToString() + "," + column.ToString());
                    }
                    Console.WriteLine("Team is: " + team);
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

        //Finds the team (0 ir 1) the current chess piece belongs to.
        private void FindChessPieceTeam(string s)
        {
            string[] sSplit = s.Split('_');

            switch (sSplit[1])
            {
                case "0":
                    team = 0;
                    break;
                case "1":
                    team = 1;
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
        }

        //Returns which chesspiece (which type of button defined by each class) that is going to be moved.
        private Button FindButtonClass(string classType, Button element)
        {
            switch (classType)
            {
                case "Pawn_0":
                case "Pawn_1":
                    Pawn pawn1 = new Pawn(row, column, team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = pawn1.GetButton();
                    break;
                case "Tower_0":
                case "Tower_1":
                    Tower tower1 = new Tower(row, column, team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = tower1.GetButton();
                    break;
                case "Knight_0":
                case "Knight_1":
                    Knight knight1 = new Knight(row, column, team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = knight1.GetButton();
                    break;
                case "BishopBlack_0":
                case "BishopBlack_1":
                    Bishop bishopBlack = new Bishop(row, column, "Black", team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = bishopBlack.GetButton();
                    break;
                case "BishopWhite_1":
                case "BishopWhite_0":
                    Bishop bishopWhite = new Bishop(row, column, "White", team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = bishopWhite.GetButton();
                    break;
                case "King_0":
                case "King_1":
                    King king1 = new King(row, column, team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = king1.GetButton();
                    break;
                case "Queen_0":
                case "Queen_1":
                    Queen queen1 = new Queen(row, column, team, Btn_PreviewMouseLeftButtonUp, Btn_PreviewMouseLeftButtonDown);
                    element = queen1.GetButton();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
            return element;
        }

        //Swap the two buttons chosen by Btn_PreviewMouseLeftButtonUp and Btn_PreviewMouseLeftButtonDown.
        private Button SwapTwoButtons(string classType)
        {
            buttonSwap = (Button)GetChildren(Form.GridName, row, column);
            FindChessPieceTeam(classType);

            Button buttonType = new Button();
            buttonType = FindButtonClass(classType, buttonType);
            SetButtonPosition(buttonType, row, column);

            Form.GridName.Children.Remove(buttonSwap);
            BoardButton button = new BoardButton(InitRow, InitCol, 0, Btn_PreviewMouseLeftButtonDown);

            objectArray[InitRow, InitCol] = button.ToString();
            objectArray[row, column] = buttonType.Name;

            int kingCounter = 0;
            string kingString = "";

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    string s = objectArray[i, j];
                    if(s == "King_0" || s == "King_1")
                    {
                        kingString = s;
                        kingCounter++;
                    }
                }
            }
            if(kingCounter < 2)
            {
                string[] winner = kingString.Split('_');
                MessageBoxResult result = MessageBox.Show("The Winner is Team: " + winner[1]);
                Console.WriteLine(kingString);
                System.Windows.Application.Current.Shutdown();
            }
            return buttonType;
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
        private void SetButtonColor(Button b, int row1, int col1, int row2, int col2)
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
                SetColor(b, Colors.White, Colors.Black);
            }
            else
            {
                SetColor(b, Colors.Black, Colors.White);
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
            bool c;
            string[] st = s.Split('_');
            switch (s)
            {
                case "Tower_0":
                case "Tower_1":
                    classMoveBool = tower.Move(row, col, desiredRow, desiredCol);
                    c = TowerMove(row, col, desiredRow, desiredCol, s);
                    if (classMoveBool && c) return true;
                    return false;

                case "Pawn_0":
                case "Pawn_1":
                    classMoveBool = pawn.Move(row, col, desiredRow, desiredCol, s);
                    c = PawnMove(row, col, desiredRow, desiredCol, s);
                    Console.WriteLine(classMoveBool + "" + c);
                    if (classMoveBool && c) return true;
                    return false;

                case "BishopWhite_0":
                case "BishopWhite_1":
                    classMoveBool = bishop.Move(row, col, desiredRow, desiredCol);
                    c = BishopMove(row, col, desiredRow, desiredCol, "White", s);
                    if (classMoveBool && c) return true;
                    return false;

                case "BishopBlack_0":
                case "BishopBlack_1":
                    classMoveBool = bishop.Move(row, col, desiredRow, desiredCol);
                    c = BishopMove(row, col, desiredRow, desiredCol, "Black", s);
                    if (classMoveBool && c) return true;
                    return false;

                case "Knight_0":
                case "Knight_1":
                    classMoveBool = knight.Move(row, col, desiredRow, desiredCol);
                    c = KnightMove(row, col, desiredRow, desiredCol, s);
                    if (classMoveBool && c) return true;
                    return false;

                case "King_0":
                case "King_1":
                    classMoveBool = king.Move(row, col, desiredRow, desiredCol);
                    c = KingMove(row, col, desiredRow, desiredCol, s);
                    if (classMoveBool && c) return true;
                    return false;

                case "Queen_0":
                case "Queen_1":
                    if ((InitRow % 2 == 0 && InitCol % 2 != 0) || (InitRow % 2 != 0 && InitCol % 2 == 0))
                    {
                        c = QueenMove(row, col, desiredRow, desiredCol, "Black", s);
                    }
                    else
                    {
                        c = QueenMove(row, col, desiredRow, desiredCol, "White", s);
                    }
                    classMoveBool = queen.Move(row, col, desiredRow, desiredCol);
                    if (classMoveBool && c) return true;
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
        private bool BishopMove(int InitRow, int InitCol, int row, int column, string color, string caller)
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
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click, caller);
        }

        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool TowerMove(int InitRow, int InitCol, int row, int column, string caller)
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
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click, caller);
        }

        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool PawnMove(int InitRow, int InitCol, int row, int column, string caller)
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
            string btnDown = objectArray[InitRow, InitCol];
            string click = objectArray[row, column];
            char lastClick = click[click.Length - 1];

            Console.WriteLine("hej" + InitRow + "" + InitCol + "" + row + "" + column);
            Console.WriteLine(btnDown);
            if(roundGameCounter % 2 == 0)
            {
                if (InitRow == 7)
                {
                    classMoveBool = false;
                }
                else if (InitRow == 6)
                {
                    if (objectArray[InitRow + 1, InitCol] != "Online_Skak.BoardButton" && InitCol == column && row - InitRow == 1)
                    {
                        classMoveBool = false;
                    }
                }
                else if (InitRow == 1 && Math.Abs(row - InitRow) == 2)
                {
                    if (objectArray[InitRow + 2, InitCol] != "Online_Skak.BoardButton" && InitCol == column && row - InitRow == 2)
                    {
                        classMoveBool = false;
                    }
                }
                else
                {
                    if (objectArray[InitRow + 1, InitCol] != "Online_Skak.BoardButton" && InitCol == column && row - InitRow == 1)
                    {
                        classMoveBool = false;
                    }
                }

                if (b1 == true && b2 == true)
                {
                    if (InitCol != 7 && objectArray[row, column] != "Online_Skak.BoardButton" && row - InitRow == 1 && Math.Abs(column - InitCol) == 1)
                    {
                        classMoveBool = true;
                    }
                }
                else if (b1 == true && b2 == false)
                {
                    if (InitCol != 0 && objectArray[row, InitCol] != "Online_Skak.BoardButton" && row - InitRow == 1 && Math.Abs(column - InitCol) == 1)
                    {
                        classMoveBool = true;
                    }
                }
            }
            else
            {
                if(row == 0)
                {
                    classMoveBool = false;
                }
                else if (row == 1)
                {
                    if (objectArray[row - 1, column] != "Online_Skak.BoardButton" && InitCol == column && row - InitRow == 1)
                    {
                        classMoveBool = false;
                    }
                }
                else if(row == 6 && Math.Abs(row - InitRow) == 2)
                {
                    if (objectArray[row - 2, column] != "Online_Skak.BoardButton" && InitCol == column && row - InitRow == 2)
                    {
                        classMoveBool = false;
                    }
                }
                else
                {
                    if (objectArray[row - 1, column] != "Online_Skak.BoardButton" && InitCol == column && row - InitRow == 1)
                    {
                        classMoveBool = false;
                    }
                }

                if (b1 == false && b2 == false)
                {
                    if (column != 7 && objectArray[InitRow, InitCol] != "Online_Skak.BoardButton" && row - InitRow == 1 && Math.Abs(column - InitCol) == 1)
                    {
                        classMoveBool = true;
                    }
                }
                else if (b1 == false && b2 == true)
                {
                    if (column != 0 && objectArray[InitRow, InitCol + 1] != "Online_Skak.BoardButton" && row - InitRow == 1 && Math.Abs(column - InitCol) == 1)
                    {
                        classMoveBool = true;
                    }
                }
            }
            if(InitCol != column)
            {
                AddChessPieceToList(InitRow,InitCol, list, objectArray[InitRow, InitCol]);
                AddChessPieceToList(row,column, list, objectArray[row, column]);
            } else
            {
                for (int i = InitRow; i <= row; i++)
                {
                    for (int j = InitCol; j <= column; j++)
                    {
                        AddChessPieceToList(i, j, list, objectArray[i, j]);
                    }
                }
            }
            
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click, caller);
        }

        //Checks the start and end position and puts the name of the chesspiece into a list if there are any on those positions (there will always be on start pos for obvious reasons). 
        //Calls "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool KnightMove(int InitRow, int InitCol, int row, int column, string caller)
        {
            List<String> list = new List<String>();

            bool b1 = true;
            bool b2 = true;

            string click = objectArray[row, column];
            char lastClick = click[click.Length - 1];

            int tempRow = (Math.Abs(row - InitRow));
            int tempCol = (Math.Abs(column - InitCol));

            AddChessPieceToList(InitRow, InitCol, list, objectArray[InitRow, InitCol]);

            if (tempRow == 2 && tempCol == 1)
            {
                AddChessPieceToList(row, column, list, objectArray[row, column]);
            }
            else if (tempCol == 2 && tempRow == 1)
            {
                AddChessPieceToList(row, column, list, objectArray[row, column]);
            }
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click, caller);
        }

        //Checks the start and end position and puts the name of the chesspiece into a list if there are any on those positions (there will always be on start pos for obvious reasons). 
        //Calls "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool KingMove(int InitRow, int InitCol, int row, int column, string caller)
        {
            List<String> list = new List<String>();

            bool b1 = true;
            bool b2 = true;
           
            string click = objectArray[row, column];
            char lastClick = click[click.Length - 1];

            int tempRow = (Math.Abs(row - InitRow));
            int tempCol = (Math.Abs(column - InitCol));

            AddChessPieceToList(InitRow, InitCol, list, objectArray[InitRow, InitCol]);

            if (tempRow == tempCol && tempRow < 2)
            {
                AddChessPieceToList(row, column, list, objectArray[row, column]);
            }
            else if ((column == InitCol || row == InitRow) && (tempRow < 2 && tempCol < 2))
            {
                AddChessPieceToList(row, column, list, objectArray[row, column]);
            }
            return CheckIfMoveIsLegal(list, b1, b2, lastClick, click, caller);
        }

        //Puts all the different chesspieces into the List if they are in the way of the move and calls the "CheckIfMoveIsLegal" before returning true/false depending on the "CheckIfMoveIsLegal" function.
        private bool QueenMove(int InitRow, int InitCol, int row, int column, string color, string caller)
        {
            if (InitRow == row || InitCol == column)
            {
                return TowerMove(InitRow, InitCol, row, column, caller);
            }
            else
            {
                return BishopMove(InitRow, InitCol, row, column, color, caller);
            }
        }

        //Checks the list provided by the ".Move" functions above and see if they contain any other chesspiece than the one wanting to move. If it does, it checks if it contains a piece from the opponent team on the distination tile. 
        //Returns true if this is the case.
        private bool CheckIfMoveIsLegal(List<string> list, bool b1, bool b2, char lastClick, string click, string caller)
        {
            StackTrace stackTrace = new StackTrace();

            // Get calling method name
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            string s;
            string[] splitCaller;

            splitCaller = caller.Split('_');

            click = objectArray[row, column];
            lastClick = click[click.Length - 1];

            foreach (string s1 in list)
            {
                Console.WriteLine(s1);
            }
            Console.WriteLine(b1 + "" + b2);
            Console.WriteLine("Splitcaller: " + splitCaller[1]);

            if (splitCaller[1] == "0")
            {
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
                        if (callingMethod == "TowerMove")
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
                    else if (b1 == false && b2 == false)
                    {
                        s = list[0];
                        if ((s[s.Length - 1] == '1') && lastClick != 'n')
                        {
                            list.Remove(list[1]);
                        }
                    }
                }
                Console.WriteLine(list.Count);
                if (list.Count <= 1)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (list.Count == 2)
                {
                    if (b1 == true && b2 == true)
                    {
                        s = list[1];
                        if ((s[s.Length - 1] == '0') && lastClick != 'n')
                        {
                            list.Remove(list[1]);
                        }
                    }
                    else if (b1 == false && b2 == true)
                    {
                        s = list[0];
                        if ((s[s.Length - 1] == '0') && lastClick != 'n')
                        {
                            list.Remove(list[0]);
                        }

                    }
                    else if (b1 == true && b2 == false)
                    {
                        if (callingMethod == "TowerMove")
                        {
                            s = list[0];
                        }
                        else
                        {
                            s = list[1];
                        }

                        if ((s[s.Length - 1] == '0') && lastClick != 'n')
                        {
                            list.Remove(list[1]);
                        }
                    }
                    else if (b1 == false && b2 == false)
                    {
                        s = list[0];
                        if ((s[s.Length - 1] == '0') && lastClick != 'n')
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
}
