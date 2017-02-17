using EESDD.Class.Control;
using EESDD.Class.Model;
using LiveCharts;
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
        public GameRealTime()
        {
            InitializeComponent();
        }

        private SpeedType CurrentType;

        #region Formatter
        private Func<float, string> fTime = val => GetTimeString(val, false);
        #endregion

        public void SetPage (Recorder record)
        {
            SetPage(record);
            SetChart(record);
        }


        private void SetPanel(Recorder record)
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
            return DateTime.Parse("2017-02-17 00:00:00").AddSeconds(seconds)
                .ToLongDateString();
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

        private void SetChart(Recorder record)
        {

        }

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            CU.MG_Page.GameEndAction();
        }
    }
}
