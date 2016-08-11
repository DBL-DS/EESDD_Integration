using EESDD.Class.Control;
using EESDD.View;
using EESDD.View.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            StartAnimation();
            ShowMainWindow();
        }

        private void StartAnimation()
        {
            long MinVisibleTime = 2000;
            long CloseTime = 500;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SplashScreen splash = 
                new SplashScreen("/View/Image/car_500.png");
            splash.Show(false, true);

            Task();

            stopwatch.Stop();

            long TimeRemian = MinVisibleTime - stopwatch.ElapsedMilliseconds;
            Console.WriteLine(TimeRemian);
            if (TimeRemian > 0)
                Thread.Sleep(TimeSpan.FromMilliseconds(TimeRemian));

            splash.Close(TimeSpan.FromMilliseconds(CloseTime));
        }

        private void ShowMainWindow()
        {
            PageManger.CurrentPage = PageCluster.Login;
            PageManger.ShowMain();
        }

        private void Task()
        {
            PageManger.Hit();
            FileManger.Hit();
            SettingManger.Hit();
            ThreadManager.Hit();
        }
    }
}
