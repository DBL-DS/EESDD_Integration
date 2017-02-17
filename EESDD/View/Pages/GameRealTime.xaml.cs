using EESDD.Class.Control;
using EESDD.Class.Model;
using LiveCharts;
using LiveCharts.Configurations;
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

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for GameRealTime.xaml
    /// </summary>
    public partial class GameRealTime : Page
    {
        private enum SpeedType
        {
            KMPERHOUR,
            MPERSECOND
        }

        private enum ChartType
        {
            Speed,
            Acc,
            Offset,
            STWAngle,
            FarToFront
        }

        private class Point
        {
            public Point(float x, float y)
            {
                this.X = x;
                this.Y = y;
            }
            public float X { get; set; }
            public float Y { get; set; }
        }

        public GameRealTime()
        {
            InitializeComponent();
        }

        private SpeedType CurrentType;

        private Func<float, string> fTime = val => GetTimeString(val, false);


        public void SetPage (Recorder record)
        {
            this.Dispatcher.BeginInvoke((System.Action)(delegate() 
            {
                UpdatePanel(record);
                UpdateChart(record);
            }));
        }

        public void ResetPage()
        {
            ClearValues();
            ChartInit();
        }

        #region Panel
        private void UpdatePanel(Recorder record)
        {
            Svframe frame = record.CurrentFrame;

            pTime.Text = GetTimeString(frame.Time, true);
            SetDistance(frame.Distance);
            SetSpeed(frame.Speed, CurrentType);
            pAcceleration.Text = frame.Acc.ToString();
            pBrakeDistance.Text = record.BrakeDistance.ToString();
            SetReactionTime(record.ReactionTime);
        }

        private static string GetTimeString(float seconds, bool WithHour)
        {
            if (WithHour)
                return DateTime.Parse("2017-02-17 00:00:00").AddSeconds(seconds)
                    .ToString("HH:mm:ss");
            else
                return DateTime.Parse("2017-02-17 00:00:00").AddSeconds(seconds)
                    .ToString("mm:ss");
        }

        private void SetDistance(float distance)
        {
            if (distance < 1000)
            {
                pDistance.Text = distance.ToString("F0");
                pDistanceUnit.Text = "m";
            }
            else
            {
                pDistance.Text = (distance / 1000).ToString("F2");
                pDistanceUnit.Text = "km";
            }
        }

        private void SetSpeed(float speed, SpeedType type)
        {
            switch (type)
            {
                case SpeedType.KMPERHOUR:
                    pSpeed.Text = speed.ToString("F0");
                    pSpeedUnit.Text = "km/h";
                    break;
                case SpeedType.MPERSECOND:
                    pSpeed.Text = (speed / 3.6).ToString("F1");
                    pSpeedUnit.Text = "m/s";
                    break;
                default:
                    break;
            }
        }

        private void SetReactionTime(float reactionTime)
        {
            if (reactionTime < 1000)
            {
                pReactionTime.Text = reactionTime.ToString("F0");
                pReactionTimeUnit.Text = "ms";
            }
            else
            {
                pReactionTime.Text = (reactionTime / 1000).ToString("F2");
                pReactionTimeUnit.Text = "s";
            }
        }
        #endregion

        #region Chart
        /* Points. T for Time, D for Distance */
        private ChartValues<Point> TSpeed;
        private ChartValues<Point> TAcc;
        private ChartValues<Point> TOffset;
        private ChartValues<Point> TSTWAngle;
        private ChartValues<Point> TFarToFront;
        private ChartValues<Point> DSpeed;
        private ChartValues<Point> DAcc;
        private ChartValues<Point> DOffset;
        private ChartValues<Point> DSTWAngle;
        private ChartValues<Point> DFarToFront;

        /* ChartValues */
        private ChartValues<Point> CenterChart { get; set; }
        private ChartValues<Point> SpeedChart { get; set; }
        private ChartValues<Point> AccChart { get; set; }
        private ChartValues<Point> OffsetChart { get; set; }
        private ChartValues<Point> STWAngleChart { get; set; }
        private ChartValues<Point> FarToFrontChart { get; set; }

        private void ClearValues()
        {
            TSpeed = new ChartValues<Point>();
            TAcc = new ChartValues<Point>();
            TOffset = new ChartValues<Point>();
            TSTWAngle = new ChartValues<Point>();
            TFarToFront = new ChartValues<Point>();
            DSpeed = new ChartValues<Point>();
            DAcc = new ChartValues<Point>();
            DOffset = new ChartValues<Point>();
            DSTWAngle = new ChartValues<Point>();
            DFarToFront = new ChartValues<Point>();
        }

        private void ChartInit()
        {
            SetMapper();
            BindTime();
        }

        private void BindTime()
        {
            CenterChart = TSpeed;
            SpeedChart = TSpeed;
            AccChart = TAcc;
            OffsetChart = TOffset;
            STWAngleChart = TSTWAngle;
            FarToFrontChart = TFarToFront;
        }

        private void BindDistance()
        {
            CenterChart = DSpeed;
            SpeedChart = DSpeed;
            AccChart = DAcc;
            OffsetChart = DOffset;
            STWAngleChart = DSTWAngle;
            FarToFrontChart = DFarToFront;
        }

        private void SetMapper()
        {
            var mapper = Mappers.Xy<Point>()
                .X(point => point.X)
                .Y(point => point.Y);
            Charting.For<Point>(mapper);
        }

        private void UpdateChart(Recorder record)
        {
            Svframe f = record.CurrentFrame;
            float t = f.Time;
            float d = f.Distance;

            TSpeed.Add(new Point(t, f.Speed));
            TAcc.Add(new Point(t, f.Acc));
            TOffset.Add(new Point(t, f.Offset));
            TSTWAngle.Add(new Point(t, f.StwAngle));
            TFarToFront.Add(new Point(t, f.FarToFront));
            DSpeed.Add(new Point(d, f.Speed));
            DAcc.Add(new Point(d, f.Acc));
            DOffset.Add(new Point(d, f.Offset));
            DSTWAngle.Add(new Point(d, f.StwAngle));
            DFarToFront.Add(new Point(d, f.FarToFront));
        }
        #endregion

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            CU.MG_Page.GameEndAction();
        }
    }
}
