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
    /// Interaction logic for ImageShapeButton.xaml
    /// </summary>
    public partial class ImageShapeButton : UserControl
    {
        public ImageShapeButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BackGroundColorProperty =
            DependencyProperty.Register("BGColor", typeof(Brush), typeof(ImageShapeButton));
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ImageSwitchButton));
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ImageShapeButton));

        public Brush BGColor
        {
            get { return InnrBorder.Background; }
            set { InnrBorder.Background = value; }
        }

        public ImageSource Icon
        {
            get { return IconImage.Source; }
            set { IconImage.Source = value; }
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }
        protected virtual void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs =
                new RoutedEventArgs(ImageShapeButton.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
        }
    }
}
