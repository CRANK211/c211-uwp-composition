using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Microsoft.Graphics.Canvas.Effects;

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

        public static Matrix5x4 ToMatrix5X4(this Color color)
        {
            var colorMatrix = new Matrix5x4
            {
                M11 = color.R * (1.0f / 255.0f),
                M12 = 0f,
                M13 = 0f,
                M14 = 0,
                M21 = 0f,
                M22 = color.G * (1.0f / 255.0f),
                M23 = 0f,
                M24 = 0,
                M31 = 0f,
                M32 = 0f,
                M33 = color.B * (1.0f / 255.0f),
                M34 = 0,
                M41 = 0f,
                M42 = 0f,
                M43 = 0f,
                M44 = color.A * (1.0f / 255.0f),
                M51 = 0,
                M52 = 0,
                M53 = 0,
                M54 = 0
            };

            return colorMatrix;
        }

    }
}
