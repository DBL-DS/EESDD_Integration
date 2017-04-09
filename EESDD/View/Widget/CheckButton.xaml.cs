using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CheckButton : UserControl, INotifyPropertyChanged
    {
        public CheckButton()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        private string text;
        private Brush buttonBack;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Brush DefaultColor
        {
            get { return GetValue(DefaultColorProperty) as Brush; }
            set
            {
                SetValue(DefaultColorProperty, value);
                IsChecked = IsChecked;
            }
        }

        public Brush CheckedColor
        {
            get { return GetValue(CheckedColorProperty) as Brush; }
            set
            {
                SetValue(CheckedColorProperty, value);
                IsChecked = IsChecked;
            }
        }

        public Brush TextColor
        {
            get { return GetValue(TextColorProperty) as Brush; }
            set { SetValue(TextColorProperty, value); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Brush ButtonBack
        {
            get { return buttonBack; }
            set
            {
                buttonBack = value;
                NotifyPropertyChange("ButtonBack");
            }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set
            {
                SetValue(CheckedProperty, value);
                ButtonBack = IsChecked ? CheckedColor : DefaultColor;
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
