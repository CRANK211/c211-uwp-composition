using C211.Uwp.Composition;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;

namespace TransparentMenu.Views
{
    public class CompositionPage : Page
    {
        public Compositor Compositor => this.GetCompositor();
    }
}
