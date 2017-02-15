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
        private readonly Compositor _compositor;

        public MainPage()
        {
            InitializeComponent();
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            LayoutRoot.Background = new TransparentBrush(_compositor);
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }
    }

    /// <summary>
    /// This class extends the <see cref="XamlCompositionBrushBase"/> class
    /// to create a simple brush that can used whereever a regular XAML brush
    /// can be used. This one creates a glass background for our app.
    /// </summary>
    public class TransparentBrush : XamlCompositionBrushBase
    {
        public TransparentBrush(Compositor compositor)
        {
            CompositionBrush = compositor.CreateHostBackdropBrush();
        }
    }
}