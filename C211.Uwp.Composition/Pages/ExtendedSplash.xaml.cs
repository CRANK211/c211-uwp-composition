using System;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace C211.Uwp.Composition.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        private readonly SplashScreen _splash; // Variable to hold the splash screen object.
        private readonly Type _redirectToPageType;
        internal bool Dismissed; // Variable to track splash screen dismissal status.
        internal Frame RootFrame;
        internal Rect SplashImageRect; // Rect to store splash screen image coordinates.
        private double _titleBarHeight;

        public ExtendedSplash(
            SplashScreen splashscreen, 
            Type redirectToPageType, 
            ImageSource splashImage,
            Color tintColor)
        {
            InitializeComponent();

            BackgroundBrush.TintColor = tintColor;
            OpacityLayer.Fill = new SolidColorBrush(tintColor);

            ConfigureTitleBar();

            _redirectToPageType = redirectToPageType;

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += ExtendedSplash_OnResize;

            _splash = splashscreen;
            if (_splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                _splash.Dismissed += DismissedEventHandler;

                // Retrieve the window coordinates of the splash screen image.
                SplashImageRect = _splash.ImageLocation;
                extendedSplashImage.Source = splashImage;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                PositionRing();
            }

            // Create a Frame to act as the navigation context
            RootFrame = new Frame();
            BackgroundOpacityStoryboard.Begin();
        }

        private void ConfigureTitleBar()
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            _titleBarHeight = coreTitleBar.Height;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = BackgroundBrush.TintColor;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.White;
        }

        public Compositor Compositor => ElementCompositionPreview.GetElementVisual(this).Compositor;

        private void PositionImage()
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            _titleBarHeight = coreTitleBar.Height == 0.0 ? 32 : coreTitleBar.Height;
            extendedSplashImage.SetValue(Canvas.LeftProperty, SplashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, SplashImageRect.Y + _titleBarHeight);
            extendedSplashImage.Height = SplashImageRect.Height;
            extendedSplashImage.Width = SplashImageRect.Width;
        }

        private void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty,
                SplashImageRect.X + SplashImageRect.Width * 0.5 - splashProgressRing.Width * 0.5);
            splashProgressRing.SetValue(Canvas.TopProperty,
                SplashImageRect.Y  + SplashImageRect.Height + SplashImageRect.Height * 0.1);
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        private void DismissedEventHandler(SplashScreen sender, object e)
        {
            Dismissed = true;

            // Complete app setup operations here...
        }

        public void DismissExtendedSplash()
        {
            // Navigate to mainpage
            RootFrame.Navigate(_redirectToPageType);
            // Place the frame in the current Window
            Window.Current.Content = RootFrame;
        }

        private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be executed when a user resizes the window.
            if (_splash != null)
            {
                // Update the coordinates of the splash screen image.
                SplashImageRect = _splash.ImageLocation;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                PositionRing();
            }
        }
    }
}