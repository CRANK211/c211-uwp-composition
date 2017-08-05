using System;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace C211.Uwp.Composition.Behaviors
{
    public class SizeAndOffsetImplicitAnimationBehavior : Behavior<DependencyObject>
    {
        private static Compositor _compositor;

        /// <summary>
        ///     The duration of the animations in MilliSeconds
        /// </summary>
        public static readonly DependencyProperty DurationMilliSecondsProperty = DependencyProperty.Register(
            "DurationMilliSeconds", typeof(double), typeof(SizeAndOffsetImplicitAnimationBehavior),
            new PropertyMetadata(200d));

        private static ImplicitAnimationCollection _implicitAnimations;

        /// <summary>
        ///     The duration of the animations in MilliSeconds
        /// </summary>
        public double DurationMilliSeconds
        {
            get { return (double) GetValue(DurationMilliSecondsProperty); }
            set { SetValue(DurationMilliSecondsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            var element = AssociatedObject as UIElement;
            if (element != null)
            {
                EnsureImplicitAnimations(element);
                // Check to see if the element has a SpriteVisual - assumption being that is what is going to be sized
                var visual = element.GetChildVisual() ?? element.GetVisual();
                visual.ImplicitAnimations = _implicitAnimations;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var element = AssociatedObject as UIElement;
            if (element != null)
            {
                var visual = element.GetChildVisual() ?? element.GetVisual();
                visual.ImplicitAnimations = null;
            }
        }

        private void EnsureImplicitAnimations(UIElement element)
        {
            if (_implicitAnimations == null)
            {
                if (_compositor == null)
                {
                    _compositor = element.GetCompositor();
                }
                var offsetAnimation = _compositor.CreateImplicitVector3Animation(
                    nameof(Visual.Offset),
                    TimeSpan.FromMilliseconds(DurationMilliSeconds));

                var sizeAnimation = _compositor.CreateImplicitVector2Animation(
                    nameof(Visual.Size),
                    TimeSpan.FromMilliseconds(DurationMilliSeconds));

                _implicitAnimations = _compositor.CreateImplicitAnimationCollection();
                _implicitAnimations[nameof(Visual.Offset)] = offsetAnimation;
                _implicitAnimations[nameof(Visual.Size)] = sizeAnimation;
            }
        }
    }
}