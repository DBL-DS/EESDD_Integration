﻿using EESDD.Class.Model;
using EESDD.View.Widget;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Dtos;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows.Controls;

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for GameData.xaml
    /// </summary>
    public partial class GameData : Page
    {
        public GameData()
        {
            InitializeComponent();
            Init();
        }

        private int ExpNum;
        private List<Exp> CheckedExp;

        private CheckButton checkedX;
        private CheckButton checkedY;

        private List<CheckButton> CheckButtons;

        private void Init()
        {
            CheckButtons = new List<CheckButton>()
            {
                cTime, cDistance,
                cSpeed, cAcc, cStwAngle, cOffset, cFarToFront, cLane,
                cDelete, cEvaluate, cPlot
            };
            SetMapper();
            InitButtons();
        }

        /* Run when swithed to this page, called in PageManager */
        public void Reset()
        {
            LoadExpList();
            CheckedX = cTime;
            CheckedY = cSpeed;
            LoadChart();
        }

        /* 
         * Temp way to init button backgrounds. 
         * There are some problems with checkbutton's property dynamic binding
         */
        private void InitButtons()
        {
            foreach (var button in CheckButtons)
                button.IsChecked = false;
        }

        #region chart
        /* For Chart Plot */
        private void SetMapper()
        {
            var mapper = Mappers.Xy<CorePoint>()
                .X(point => point.X)
                .Y(point => point.Y);
            Charting.For<CorePoint>(mapper);
        }

        private CheckButton CheckedX
        {
            get { return checkedX; }
            set
            {
                if(checkedX == null || !checkedX.Equals(value))
                {
                    if (checkedX != null)
                        checkedX.IsChecked = false;

                    checkedX = value;
                    checkedX.IsChecked = true;
                    LoadChart();
                }
            }
        }

        private CheckButton CheckedY
        {
            get { return checkedY; }
            set
            {
                if (checkedY == null || !checkedY.Equals(value))
                {
                    if (checkedY != null)
                        checkedY.IsChecked = false;

                    checkedY = value;
                    checkedY.IsChecked = true;
                    LoadChart();
                }
            }
        }

        private void LoadChart()
        {
            LineChart.Series.Clear();
            if (CheckedExp != null && CheckedExp.Count != 0)
            {
                foreach (var exp in CheckedExp)
                {
                    AddLineToChart(exp);
                }
            }
        }

        private void AddLineToChart(Exp exp)
        {
            if (exp != null && exp.TotalArea.Svframes != null 
                && exp.TotalArea.Svframes.Count != 0)
            {
                var line = new LineSeries();
                line.Values = GetChartValues(exp);
                LineChart.Series.Add(line);
            }
        }

        private ChartValues<CorePoint> GetChartValues(Exp exp)
        {
            if (checkedX == null || checkedY == null)
                return null;

            ChartValues<CorePoint> points = new ChartValues<CorePoint>();
            foreach (var frame in exp.TotalArea.Svframes)
            {
                double x, y;

                if (checkedX.Equals(cTime))
                    x = frame.Time;
                else if (checkedX.Equals(cDistance))
                    x = frame.Distance;
                else
                    return null;

                if (checkedY.Equals(cSpeed))
                    y = frame.Speed;
                else if (checkedY.Equals(cAcc))
                    y = frame.Acc;
                else if (checkedY.Equals(cStwAngle))
                    y = frame.StwAngle;
                else if (checkedY.Equals(cOffset))
                    y = frame.Offset;
                else if (checkedY.Equals(cFarToFront))
                    y = frame.FarToFront;
                else if (checkedY.Equals(cLane))
                    y = frame.Lane;
                else
                    return null;

                points.Add(new CorePoint(x, y));
            }
            return points;
        }
        #endregion

        #region explist
        public void LoadExpList()
        {
            ClearExpList();
            if (CU.MG_User.User.Group == UserGroup.REGULAR &&
                CU.MG_Exp.Exps != null)
            {
                foreach (var exp in CU.MG_Exp.Exps)
                {
                    AddExpCell(exp);
                }
            }
        }

        public void ClearExpList()
        {
            ExpNum = 0;
            CheckedExp = new List<Exp>();
            ExpListPanel.Children.Clear();
        }

        private void AddExpCell(Exp exp)
        {
            var expCell = new ExpCell(++ExpNum ,exp);
            expCell.Checked += (sender, e) => { CheckAction(exp); };
            expCell.Unchecked += (sender, e) => { UncheckAction(exp); };
            if (exp.ExpType != ExpType.Normal && exp.ExpType != ExpType.CarCrash_Normal)
                expCell.Enable = false;
            ExpListPanel.Children.Add(expCell);
        }

        private void DeleteExps()
        {
            foreach (var exp in CheckedExp)
            {
                CU.MG_Exp.RemoveExp(exp);
            }
            CheckedExp.Clear();
            LoadExpList();
            LoadChart();
        }
        #endregion

        public void CheckAction(Exp exp)
        {
            CheckedExp.Add(exp);
        }

        public void UncheckAction(Exp exp)
        {
            CheckedExp.Remove(exp);
        }

        private void CheckButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadChart();
        }

        private void AxisXButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckedX = sender as CheckButton;
        }

        private void AxisYButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckedY = sender as CheckButton;
        }

        private void DeleteExpButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteExps();
        }
    }
}
