using Windows.ApplicationModel.Core;
using Windows.Graphics.Effects;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Microsoft.Graphics.Canvas.Effects;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TransparentWindows
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        public Compositor Compositor { get; set; }
    }

    /// <summary>
    ///     This class extends the <see cref="XamlCompositionBrushBase" /> class
    ///     to create a simple brush that can used whereever a regular XAML brush
    ///     can be used. This one creates a glass background for our app.
    /// </summary>
    public class TransparentBrush : XamlCompositionBrushBase
    {
        private Compositor _compositor;

        public Compositor Compositor
        {
            get { return _compositor; }
            set
            {
                _compositor = value;

                // adding a grey tint to the background
                var colorMatrix = new Matrix5x4()
                {
                    M11 = 0.8f,
                    M12 = 0f,
                    M13 = 0f,
                    M14 = 0,
                    M21 = 0f,
                    M22 = 0.8f,
                    M23 = 0f,
                    M24 = 0,
                    M31 = 0f,
                    M32 = 0f,
                    M33 = 0.8f,
                    M34 = 0,
                    M41 = 0f,
                    M42 = 0f,
                    M43 = 0f,
                    M44 = 1f,
                    M51 = 0,
                    M52 = 0,
                    M53 = 0,
                    M54 = 0
                };

                var graphicsEffect = new ColorMatrixEffect()
                {
                    ColorMatrix = colorMatrix,
                    Source = new CompositionEffectSourceParameter("Background")
                };

                var effectFactory = _compositor.CreateEffectFactory(graphicsEffect, null);
                var brush = effectFactory.CreateBrush();

                brush.SetSourceParameter("Background", _compositor.CreateHostBackdropBrush());
                CompositionBrush = brush;
            }
        }
    }
}