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
    /// Interaction logic for CheckButton.xaml
    /// </summary>
    public partial class CheckButton : UserControl
    {
        public CheckButton()
        {
            InitializeComponent();
            defaultColor = new SolidColorBrush(Colors.White);
            checkedColor = new SolidColorBrush(Colors.LightCyan);
            IsChecked = false;
        }

        private Brush defaultColor;
        private Brush checkedColor;
        private bool isChecked;
        private string text;

        public static readonly DependencyProperty DefaultColorProperty =
            DependencyProperty.Register("DefaultColor", typeof(Brush), typeof(CheckButton));
        public static readonly DependencyProperty CheckedColorProperty =
            DependencyProperty.Register("CheckedColor", typeof(Brush), typeof(CheckButton));
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(CheckButton));
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(CheckButton));
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CheckButton));
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CheckButton));

        public Brush DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        public Brush CheckedColor
        {
            get { return checkedColor; }
            set { CheckedColor = value; }
        }

        public Brush TextColor
        {
            get { return btnText.Foreground; }
            set { btnText.Foreground = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                btnBorder.Background = isChecked ? checkedColor : defaultColor;
            }
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs =
                new RoutedEventArgs(ClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
        }
    }
}
