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
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageSwitchButton : UserControl
    {
        public ImageSwitchButton()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ImageSwitchButton));
        public static readonly DependencyProperty HoverImageProperty =
            DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(ImageSwitchButton));
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ImageSwitchButton));

        public ImageSource NormalImage
        {
            get { return imgNormal.ImageSource; }
            set { imgNormal.ImageSource = value; }
        }

        public ImageSource HoverImage
        {
            get { return imgHover.ImageSource; }
            set { imgHover.ImageSource = value; }
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            NormalButton.Visibility = System.Windows.Visibility.Collapsed;
            HoverButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            NormalButton.Visibility = System.Windows.Visibility.Visible;
            HoverButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected virtual void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs =
                new RoutedEventArgs(ImageSwitchButton.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void HoverButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
        }
    }
}
