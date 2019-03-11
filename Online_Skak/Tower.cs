﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Online_Skak
{
    public class Tower : ButtonClass
    {

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public Tower(int row, int column)
        {
            Button towerButton = new Button();

            SetButtonPosition(towerButton, row, column);


            towerButton.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
            towerButton.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

            SetDefaultButtonColor(towerButton, row, column);

            SetButtonName(towerButton, "Tower");
            towerButton.Content = "Tower";

            Form.GridName.Children.Add(towerButton);
        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfBoard(sender)) return;

            row = GetRow(e);
            column = GetColumn(e);

            if (!(ValidMove(row, column, InitRow, InitCol))) return;

            SwapTwoButtons(e); 
        }
    }
}