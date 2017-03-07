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
            InitializeComponent();
        }

        public static readonly RoutedEvent CheckEvent =
            EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ExpCell));
        public static readonly RoutedEvent UncheckEvent =
            EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ExpCell));

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

        private void RaiseCheckedEvent(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CheckEvent));
        }

        private void RaiseUncheckedEvent(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(UncheckEvent));
        }
    }
}
