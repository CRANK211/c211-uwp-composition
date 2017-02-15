using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using C211.Uwp.Composition.Controls;

namespace C211.Uwp.Composition.Services
{
    public class ConnectedVisualService
    {
        private static ConnectedVisualService _instance;
        private readonly Frame _hostFrame;
        private SpriteVisual _floatingVisual;
        private Uri _floatingVisualUri;

        private ConnectedVisualService(Frame frame)
        {
            _hostFrame = frame;
            _instance = this;
        }

        public bool HasFloatingVisual => _floatingVisual != null;

        public static ConnectedVisualService Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("ConnectedVisualService has not been initialized");
                }

                return _instance;
            }
        }

        public static bool IsInitialized => _instance != null;

        public void AttachVisual(UIElement element)
        {
            if (_floatingVisual == null)
            {
                throw new InvalidOperationException("No floating visual set");
            }
            _floatingVisual.Parent?.Children.Remove(_floatingVisual);
            ElementCompositionPreview.SetElementChildVisual(_hostFrame, null);
            ElementCompositionPreview.SetElementChildVisual(element, _floatingVisual);
            _floatingVisual = null;
        }

        public void AttachVisual(VisualImage visualImage)
        {
            if (_floatingVisual == null)
            {
                throw new InvalidOperationException("No floating visual set");
            }

            visualImage.ImageLoaded += VisualImageOnImageLoaded;
            visualImage.ImageUri = _floatingVisualUri;
            visualImage.Opacity = 0;
        }


        public void DetachVisual(VisualImage source)
        {
            if (_floatingVisual != null)
            {
                throw new InvalidOperationException("A floating visual is already in use");
            }

            _floatingVisual = source.ForegroundVisual.Compositor.CreateSpriteVisual();
            _floatingVisual.Size = source.ForegroundVisual.Size;
            _floatingVisual.Brush = source.ForegroundVisual.Brush;

            // Determine the offset from the host to the source element used in the transition
            var coordinate = source.TransformToVisual(_hostFrame);
            var position = coordinate.TransformPoint(new Point(0, 0));

            // Set the sprite to that offset relative to the host
            _floatingVisual.Offset = new Vector3((float) position.X + source.ForegroundVisual.Offset.X,
                (float) position.Y + source.ForegroundVisual.Offset.Y, 0);
            _floatingVisualUri = source.ImageUri;

            // Set the sprite as the content under the host
            ElementCompositionPreview.SetElementChildVisual(_hostFrame, _floatingVisual);
        }

        public static ConnectedVisualService Initialize(Frame frame)
        {
            if (frame == null)
            {
                throw new ArgumentException("frame cannot be null");
            }

            if (_instance != null)
            {
                throw new InvalidOperationException("ConnectedVisualService has already been initialized");
            }

            return new ConnectedVisualService(frame);
        }

        private void StartOpacityAnimations(VisualImage visualImage)
        {
            var batch = visualImage.ForegroundVisual.Compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
            var opacityAnimation = visualImage.ForegroundVisual.Compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.InsertKeyFrame(0f, 0f);
            opacityAnimation.InsertKeyFrame(1f, 1f);
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(400);

            var visual = ElementCompositionPreview.GetElementVisual(visualImage);
            visual.StartAnimation(nameof(Visual.Opacity), opacityAnimation);

            var opacityHideAnimation = _floatingVisual.Compositor.CreateScalarKeyFrameAnimation();
            opacityHideAnimation.InsertKeyFrame(0f, 1f);
            opacityHideAnimation.InsertKeyFrame(1f, 0f);
            opacityHideAnimation.Duration = TimeSpan.FromMilliseconds(400);

            _floatingVisual.StartAnimation(nameof(Visual.Opacity), opacityHideAnimation);

            batch.Completed += (sender, args) =>
            {
                ElementCompositionPreview.SetElementChildVisual(_hostFrame, null);
                _floatingVisual?.Dispose();
                _floatingVisual = null;
            };
            batch.End();
        }

        private void VisualImageOnImageLoaded(object sender, EventArgs eventArgs)
        {
            var visualImage = (VisualImage) sender;
            visualImage.ImageLoaded -= VisualImageOnImageLoaded;

            var coordinate = visualImage.TransformToVisual(_hostFrame);
            var position = coordinate.TransformPoint(new Point(0, 0));

            // setup initial visual position
            var currentOffset = _floatingVisual.Offset;
            currentOffset.X -= (float) position.X;
            currentOffset.Y -= (float) position.Y;
            _floatingVisual.Offset = currentOffset;

            // calculate destination values
            var destinationOffset = new Vector3((float) position.X + (float) visualImage.ImageMargin.Left,
                (float) position.Y + (float) visualImage.ImageMargin.Top, 0);
            var destinationHorizontalRatio =
                ((CompositionSurfaceBrush) visualImage.ForegroundVisual.Brush).HorizontalAlignmentRatio;
            var destinationSize = visualImage.ForegroundVisual.Size;

            var compositor = visualImage.ForegroundVisual.Compositor;
            var totalDuration = TimeSpan.FromMilliseconds(500);

            var batch = compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

            var sizeAnimation = compositor.CreateVector2KeyFrameAnimation();
            sizeAnimation.InsertKeyFrame(1f, destinationSize);
            sizeAnimation.Duration = totalDuration;

            var offsetAnimation = compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.InsertKeyFrame(1f, destinationOffset);
            offsetAnimation.Duration = totalDuration;

            var horizontalRatioAnimation = compositor.CreateScalarKeyFrameAnimation();
            horizontalRatioAnimation.InsertKeyFrame(1f, destinationHorizontalRatio);
            horizontalRatioAnimation.Duration = totalDuration;

            _floatingVisual.StartAnimation(nameof(Visual.Offset), offsetAnimation);
            _floatingVisual.StartAnimation(nameof(Visual.Size), sizeAnimation);
            ((CompositionSurfaceBrush) _floatingVisual.Brush).StartAnimation(
                nameof(CompositionSurfaceBrush.HorizontalAlignmentRatio), horizontalRatioAnimation);

            batch.Completed += (o, args) => StartOpacityAnimations(visualImage);
            batch.End();
        }
    }
}