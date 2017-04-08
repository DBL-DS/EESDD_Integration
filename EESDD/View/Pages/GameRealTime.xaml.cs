using EESDD.Class.Control;
using EESDD.Class.Model;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Dtos;
using System;
using System.Windows;
using System.Windows.Controls;

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

        public GameRealTime()
        {
            InitializeComponent();
            ContentGrid.DataContext = this;
            first = true;
        }

        private SpeedType CurrentType;

        private Func<float, string> fTime = val => GetTimeString(val, false);
        /* For Action Binding. Action Binding Only Once */
        private bool first;
        public bool FirstRun
        {
            get
            {
                bool tmp= first;
                first = false;
                return tmp;
            }

            set
            {
                first = value;
            }
        }


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
        private ChartValues<CorePoint> TSpeed;
        private ChartValues<CorePoint> TAcc;
        private ChartValues<CorePoint> TOffset;
        private ChartValues<CorePoint> TSTWAngle;
        private ChartValues<CorePoint> TFarToFront;
        private ChartValues<CorePoint> DSpeed;
        private ChartValues<CorePoint> DAcc;
        private ChartValues<CorePoint> DOffset;
        private ChartValues<CorePoint> DSTWAngle;
        private ChartValues<CorePoint> DFarToFront;

        /* Cache Points */
        private ChartValues<CorePoint> CacheTSpeed;
        private ChartValues<CorePoint> CacheTAcc;
        private ChartValues<CorePoint> CacheTOffset;
        private ChartValues<CorePoint> CacheTSTWAngle;
        private ChartValues<CorePoint> CacheTFarToFront;
        private ChartValues<CorePoint> CacheDSpeed;
        private ChartValues<CorePoint> CacheDAcc;
        private ChartValues<CorePoint> CacheDOffset;
        private ChartValues<CorePoint> CacheDSTWAngle;
        private ChartValues<CorePoint> CacheDFarToFront;

        private const int CacheSize = 10;
        private int CacheCount;

        /* ChartValues */
        public ChartValues<CorePoint> CenterChart { get; set; }
        public ChartValues<CorePoint> SpeedChart { get; set; }
        public ChartValues<CorePoint> AccChart { get; set; }
        public ChartValues<CorePoint> OffsetChart { get; set; }
        public ChartValues<CorePoint> STWAngleChart { get; set; }
        public ChartValues<CorePoint> FarToFrontChart { get; set; }

        private void ClearValues()
        {
            TSpeed = new ChartValues<CorePoint>();
            TAcc = new ChartValues<CorePoint>();
            TOffset = new ChartValues<CorePoint>();
            TSTWAngle = new ChartValues<CorePoint>();
            TFarToFront = new ChartValues<CorePoint>();
            DSpeed = new ChartValues<CorePoint>();
            DAcc = new ChartValues<CorePoint>();
            DOffset = new ChartValues<CorePoint>();
            DSTWAngle = new ChartValues<CorePoint>();
            DFarToFront = new ChartValues<CorePoint>();
        }

        private void ChartInit()
        {
            SetMapper();
            BindTime();
            CacheCount = 0;
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
            var mapper = Mappers.Xy<CorePoint>()
                .X(point => point.X)
                .Y(point => point.Y);
            Charting.For<CorePoint>(mapper);
        }

        private void UpdateChart(Recorder record)
        {
            if (CacheCount >= CacheSize)
                FlushCache();
            if (CacheCount == 0)
                ClearCache();

            Cache(record);
        }

        private void ClearCache()
        {
            CacheTSpeed = new ChartValues<CorePoint>();
            CacheTAcc = new ChartValues<CorePoint>();
            CacheTOffset = new ChartValues<CorePoint>();
            CacheTSTWAngle = new ChartValues<CorePoint>();
            CacheTFarToFront = new ChartValues<CorePoint>();
            CacheDSpeed = new ChartValues<CorePoint>();
            CacheDAcc = new ChartValues<CorePoint>();
            CacheDOffset = new ChartValues<CorePoint>();
            CacheDSTWAngle = new ChartValues<CorePoint>();
            CacheDFarToFront = new ChartValues<CorePoint>();
        }

        private void Cache(Recorder record)
        {
            Svframe f = record.CurrentFrame;
            float t = f.Time;
            float d = f.Distance;

            CacheTSpeed.Add(new CorePoint(t, f.Speed));
            CacheTAcc.Add(new CorePoint(t, f.Acc));
            CacheTOffset.Add(new CorePoint(t, f.Offset));
            CacheTSTWAngle.Add(new CorePoint(t, f.StwAngle));
            CacheTFarToFront.Add(new CorePoint(t, f.FarToFront));
            CacheDSpeed.Add(new CorePoint(d, f.Speed));
            CacheDAcc.Add(new CorePoint(d, f.Acc));
            CacheDOffset.Add(new CorePoint(d, f.Offset));
            CacheDSTWAngle.Add(new CorePoint(d, f.StwAngle));
            CacheDFarToFront.Add(new CorePoint(d, f.FarToFront));

            CacheCount++;
        }
        private void FlushCache()
        {
            TSpeed.AddRange(CacheTSpeed);
            TAcc.AddRange(CacheTAcc);
            TOffset.AddRange(CacheTOffset);
            TSTWAngle.AddRange(CacheTSTWAngle);
            TFarToFront.AddRange(CacheTFarToFront);
            DSpeed.AddRange(CacheDSpeed);
            DAcc.AddRange(CacheDAcc);
            DOffset.AddRange(CacheDOffset);
            DSTWAngle.AddRange(CacheDSTWAngle);
            DFarToFront.AddRange(CacheDFarToFront);

            CacheCount = 0;
        }
        #endregion

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            CU.MG_Page.GameEndAction();
        }
    }
}
