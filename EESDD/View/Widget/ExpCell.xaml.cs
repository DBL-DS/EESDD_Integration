using EESDD.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EESDD.View.Widget
{
    /// <summary>
    /// Interaction logic for ExpCell.xaml
    /// </summary>
    public partial class ExpCell : UserControl
    {
        public ExpCell()
        {
            Init();
        }

        public ExpCell(int num, Exp exp)
        {
            Init();
            SetCell(num, exp, true);
        }

        public ExpCell(int num, Exp exp, bool enable)
        {
            Init();
            SetCell(num, exp, enable);
        }

        public void Init()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent CheckEvent =
            EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ExpCell));
        public static readonly RoutedEvent UncheckEvent =
            EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ExpCell));
        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.Register("Enable", typeof(bool), typeof(ExpCell));
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ExpCell));

        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public void SetCell(int num, Exp exp, bool enable)
        {
            eNum.Text = num.ToString();
            eScene.Text = exp.Scene;
            eMode.Text = exp.Mode;
            eStartTime.Text = exp.StartTime.ToString(DateFormat);
            eEndTime.Text = exp.EndTime.ToString(DateFormat);
            eType.Text = exp.ExpType.ToString();
            eScore.Text = exp.TotalArea.Score.ToString();
            Enable = enable;
        }

        public event RoutedEventHandler Checked
        {
            add { AddHandler(CheckEvent, value); }
            remove { RemoveHandler(CheckEvent, value); }
        }

        public event RoutedEventHandler Unchecked
        {
            add { AddHandler(UncheckEvent, value); }
            remove { RemoveHandler(UncheckEvent, value); }
        } 

        private void RaiseCheckedEvent()
        {
            RaiseEvent(new RoutedEventArgs(CheckEvent));
        }

        private void RaiseUncheckedEvent()
        {
            RaiseEvent(new RoutedEventArgs(UncheckEvent));
        }

        public bool Enable
        {
            get { return eCoverUnable.Visibility != Visibility.Visible; }
            set
            {
                eCoverUnable.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public bool IsChecked
        {
            get { return eCoverChecked.Visibility != Visibility.Visible; }
            set
            {
                eCoverChecked.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            IsChecked = !IsChecked;
            if (IsChecked)
                RaiseCheckedEvent();
            else
                RaiseUncheckedEvent();
        }
    }
}
