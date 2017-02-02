using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace C211.Uwp.Composition
{
    public static class Extensions
    {
        public static Compositor GetCompositor(this UIElement uiElement)
        {
            return ElementCompositionPreview.GetElementVisual(uiElement).Compositor;
        }

        public static Visual GetVisual(this UIElement uiElement)
        {
            return ElementCompositionPreview.GetElementVisual(uiElement);
        }

        public static Visual GetChildVisual(this UIElement uiElement)
        {
            return ElementCompositionPreview.GetElementChildVisual(uiElement);
        }

        public static Vector3KeyFrameAnimation CreateImplicitVector3Animation(this Compositor compositor, string target,
            TimeSpan duration)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.Target = target;
            animation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            animation.Duration = duration;
            return animation;
        }
        public static Vector2KeyFrameAnimation CreateImplicitVector2Animation(this Compositor compositor, string target,
            TimeSpan duration)
        {
            var animation = compositor.CreateVector2KeyFrameAnimation();
            animation.Target = target;
            animation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            animation.Duration = duration;
            return animation;
        }

    }
}
