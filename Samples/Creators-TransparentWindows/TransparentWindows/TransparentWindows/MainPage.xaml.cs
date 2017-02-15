using Windows.ApplicationModel.Core;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

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
                CompositionBrush = _compositor.CreateHostBackdropBrush();
            }
        }
    }
}