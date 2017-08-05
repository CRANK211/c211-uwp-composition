using System;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Microsoft.Graphics.Canvas.Effects;

namespace C211.Uwp.Composition.Brushes
{
    public class TransparentBrush : XamlCompositionBrushBase
    {
        private Compositor _compositor;

        public TransparentBrush()
        {
            FallbackColor = Color.FromArgb(128, 55, 55, 55);
        }

        public static readonly DependencyProperty TintColorProperty = DependencyProperty.Register(
            "TintColor", typeof(Color), typeof(TransparentBrush), new PropertyMetadata(Color.FromArgb(255, 55, 55, 55), TintColorChanged));

        private static void TintColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as TransparentBrush;
            instance?.BuildBrush();
        }


        public Color TintColor
        {
            get { return (Color) GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }

        private void BuildBrush()
        {
            if (_compositor == null)
            {
                _compositor = Window.Current.Compositor;
            }

            // adding a grey tint to the background
            var colorMatrix = TintColor.ToMatrix5X4();

            var graphicsEffect = new ColorMatrixEffect
            {
                ColorMatrix = colorMatrix,
                Source = new CompositionEffectSourceParameter("Background")
            };

            var effectFactory = _compositor.CreateEffectFactory(graphicsEffect, null);
            var brush = effectFactory.CreateBrush();

            brush.SetSourceParameter("Background", _compositor.CreateHostBackdropBrush());
            CompositionBrush = brush;
        }


        protected override void OnConnected()
        {
            _compositor = Window.Current.Compositor;
            BuildBrush();
        }
    }
}