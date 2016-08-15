using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EESDD.View.Widget
{
    class SpacingTextBlock : TextBlock
    {
        public int Tracking
        {
            get { return (int)GetValue(TrackingProperty); }
            set { SetValue(TrackingProperty, value); }
        }

        public static readonly DependencyProperty TrackingProperty =
          DependencyProperty.Register("Tracking", typeof(int), typeof(SpacingTextBlock),
          new UIPropertyMetadata(0,
          new PropertyChangedCallback(TrackingPropertyChanged)));

        static void TrackingPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SpacingTextBlock tb = o as SpacingTextBlock;

            if (tb == null
              || String.IsNullOrEmpty(tb.Text))
                return;

            tb._Tracking.X = (int)e.NewValue;
            tb._TrackingAlignment.X = -(int)e.NewValue * tb.Text.Length;

            if (tb._LastTrackingTextLength == tb.Text.Length)
                return; // Avoid re-creating effects when you don't have to..

            // Remove unused effects (string has shortened)
            while (tb._TrackingEffects.Count > tb.Text.Length)
            {
                tb.TextEffects.Remove(tb._TrackingEffects[tb._TrackingEffects.Count - 1]);
                tb._TrackingEffects.RemoveAt(tb._TrackingEffects.Count - 1);
            }

            tb._LastTrackingTextLength = tb.Text.Length;

            // Add missing effects (string has grown)
            for (int i = tb._TrackingEffects.Count; i < tb.Text.Length; i++)
            {
                TextEffect fx = new TextEffect()
                {
                    PositionCount = i,
                    Transform = tb._Tracking
                };
                tb._TrackingEffects.Add(fx);
                tb.TextEffects.Add(fx);
            }

            // Ugly hack to fix overall alignment
            tb.RenderTransform = tb._TrackingAlignment;

        }

        TranslateTransform _Tracking = new TranslateTransform();
        TranslateTransform _TrackingAlignment = new TranslateTransform();
        List<TextEffect> _TrackingEffects = new List<TextEffect>();
        int _LastTrackingTextLength;
    }
}
