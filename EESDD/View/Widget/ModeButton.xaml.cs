﻿using System;
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
    /// Interaction logic for ModeButton.xaml
    /// </summary>
    public partial class ModeButton : UserControl
    {
        public ModeButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ModeImageProperty =
            DependencyProperty.Register("ModeImage", typeof(ImageSource), typeof(ModeButton));
        public static readonly DependencyProperty ModeTextProperty =
            DependencyProperty.Register("ModeText", typeof(string), typeof(ModeButton));
        public static readonly DependencyProperty CheckProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ModeButton));
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ModeButton));

        private const double CHECKEDOPACITY = 0.8;
        private const double UNCHECKEDOPACITY = 0.3;
        private const double CHECKEBLUR = 2;
        private const double UNCHECKEDBLUR = 10;

        public ImageSource ModeImage
        {
            get { return mImage.Source; }
            set { mImage.Source = value; }
        }

        public string ModeText
        {
            get { return mText.Text; }
            set { mText.Text = value; }
        }

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public bool IsChecked
        {
            get { return mImage.Opacity == CHECKEDOPACITY; }
            set
            {
                mImage.Opacity = value ? CHECKEDOPACITY : UNCHECKEDOPACITY;
                mBlur.Radius = value ? CHECKEBLUR : UNCHECKEDBLUR;
            }
        }

        protected virtual void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs =
                new RoutedEventArgs(ModeButton.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
        }
    }
}
