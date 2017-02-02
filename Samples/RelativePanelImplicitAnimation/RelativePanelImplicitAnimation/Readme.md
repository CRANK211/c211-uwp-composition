# Relative Panel Implicit Animations #

This sample demonstrates how a `Behavior` can be used to improve the appearance of an app when it is being resized. If an unmodified UWP application that defines a number of visual states using `AdaptiveTriggers` is resized, each state is applied instantly resulting in a jarring change of layout, etc. By leverageing Windows UI Composition Implicit Animations we are able to have the composition visual layer automatically move and size our elements to provide a more pleasing effect.

## Assumptions ##
In this sample I am assuming the following:

* Visual Triggers are used to define layouts for specific view dimensions
* RelativePanels are used to position elements

## Dependencies
In this sample we are using:

* `Robmikh.CompositionSurfaceFactory` NuGet to load an image
* `Microsoft.Xaml.Behaviors.Uwp.Managed` NuGet to build a `Behavior`

### Getting Started ###
 1. `Main.xaml` defines the layout for our sample app. We start with a `Grid` element as out LayoutRoot:

    ```xml
    <Grid x:Name="LayoutRoot">

    </Grid>
    ```

 1. Within the `Grid` we find the visual state definitions that determine how our UI reacts based upon the view size:

    ```xml
        <Grid x:Name="LayoutRoot">
            <VisualStateManager.VisualStateGroups>
                <VisualStateGroup>
                    <VisualState x:Name="WideState">
                        <VisualState.StateTriggers>
                            <AdaptiveTrigger MinWindowWidth="1200" />
                        </VisualState.StateTriggers>

                        <VisualState.Setters>
                            <Setter Target="ImageDetails.(RelativePanel.RightOf)"
                                    Value="Poster" />
                            <Setter Target="ImageDetails.(RelativePanel.AlignRightWithPanel)"
                                    Value="True" />
                            <Setter Target="CommentsScrollviewer.(RelativePanel.RightOf)"
                                    Value="Poster" />
                            <Setter Target="CommentsScrollviewer.(RelativePanel.Below)"
                                    Value="ImageDetails" />
                            <Setter Target="CommentsScrollviewer.(RelativePanel.AlignBottomWithPanel)"
                                    Value="True" />
                        </VisualState.Setters>
                    </VisualState>

                    <VisualState x:Name="NormalState">
                        <VisualState.StateTriggers>
                            <AdaptiveTrigger MinWindowWidth="800" />
                        </VisualState.StateTriggers>

                        <VisualState.Setters>
                            <Setter Target="Poster.(UserControl.Width)"
                                    Value="500" />
                            <Setter Target="Poster.(UserControl.Height)"
                                    Value="700" />

                            <Setter Target="ImageDetails.(RelativePanel.Below)"
                                    Value="Poster" />
                            <Setter Target="ImageDetails.(RelativePanel.AlignRightWith)"
                                    Value="Poster" />
                            <Setter Target="ImageDetails.(RelativePanel.AlignLeftWithPanel)"
                                    Value="True" />
                            <Setter Target="ImageDetails.Margin"
                                    Value="12" />

                            <Setter Target="CommentsScrollviewer.(RelativePanel.RightOf)"
                                    Value="Poster" />
                            <Setter Target="CommentsScrollviewer.(RelativePanel.AlignBottomWithPanel)"
                                    Value="True" />

                        </VisualState.Setters>
                    </VisualState>

                    <VisualState x:Name="NarrowState">
                        <VisualState.StateTriggers>
                            <AdaptiveTrigger MinWindowWidth="0" />
                        </VisualState.StateTriggers>

                        <VisualState.Setters>

                            <Setter Target="Poster.(UserControl.Width)"
                                    Value="200" />
                            <Setter Target="Poster.(UserControl.Height)"
                                    Value="300" />

                            <Setter Target="ImageDetails.(RelativePanel.Below)"
                                    Value="Poster" />
                            <Setter Target="CommentsScrollviewer.(RelativePanel.Below)"
                                    Value="ImageDetails" />

                        </VisualState.Setters>
                    </VisualState>
                </VisualStateGroup>
            </VisualStateManager.VisualStateGroups>

    ```

    > **Note**: We have defined a number of snap points in our app >=1200 >=800 and >=0

 1. Now we are going to specify the layout. You will see we use a `RelativePanel` to position 3 blocks of content:
    * An image
    * A description of the image
    * A scrolling region of items representing CommentsScrollviewer

    ```xml
        <RelativePanel HorizontalAlignment="Stretch"
                       Margin="20">

            <controls:VisualControl ImageUri="ms-appx:///assets/IMG_1343.JPG"
                                    x:Name="Poster"
                                    Width="600"
                                    Height="900"
                                    Margin="12">
                <interactivity:Interaction.Behaviors>
                    <behaviors:SizeAndOffsetImplicitAnimationBehavior DurationMilliSeconds="300" />
                </interactivity:Interaction.Behaviors>
            </controls:VisualControl>

            <StackPanel x:Name="ImageDetails"
                        Background="LightBlue">
                <StackPanel Margin="12">
                    <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                               Text="This is the Image Title"
                               Margin="12" />
                    <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                               Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                               Margin="12" />
                </StackPanel>

                <interactivity:Interaction.Behaviors>
                    <behaviors:SizeAndOffsetImplicitAnimationBehavior DurationMilliSeconds="300" />
                </interactivity:Interaction.Behaviors>

            </StackPanel>

            <ScrollViewer x:Name="CommentsScrollviewer">
                <interactivity:Interaction.Behaviors>
                    <behaviors:SizeAndOffsetImplicitAnimationBehavior DurationMilliSeconds="300" />
                </interactivity:Interaction.Behaviors>
                <StackPanel Background="Azure">
                    <TextBlock x:Name="FirstNameLabel"
                               Style="{StaticResource HeaderTextBlockStyle}"
                               Text="Comments"
                               Margin="12" />

                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                    <StackPanel Margin="12">
                        <TextBlock Style="{StaticResource SubheaderTextBlockStyle}"
                                   Text="This is a comment title"
                                   Margin="12" />
                        <TextBlock Style="{StaticResource TitleTextBlockStyle}"
                                   Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean aliquam tortor lacus, vel vulputate felis malesuada nec. Mauris posuere a velit ac condimentum. Nullam gravida ante erat, ullamcorper ultrices metus congue et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum venenatis, tortor a scelerisque interdum, nunc ipsum tincidunt nisi, ut tempor sem neque a ligula. Nullam iaculis nisi ac maximus ornare. Vivamus vitae euismod sem. Integer venenatis lorem non blandit lacinia. Mauris tincidunt velit ante, sit amet malesuada arcu commodo at."
                                   Margin="12" />
                    </StackPanel>
                </StackPanel>
            </ScrollViewer>
        </RelativePanel>
    ```
    > Note: You will see within each of the blocks we reference a behavior:
    ```xml
                    <interactivity:Interaction.Behaviors>
                        <behaviors:SizeAndOffsetImplicitAnimationBehavior DurationMilliSeconds="300" />
                    </interactivity:Interaction.Behaviors>
    ```
    This behavior is where the magic happens...

 1. Open the `SizeAndOffsetImplicitAnimationBehavior` (snappily named...) within the `C211.Uwp.Composition` class library and lets review what happens there.

 1. First we define the class and that it extends the Behavior base class:
    ```csharp
        public class SizeAndOffsetImplicitAnimationBehavior : Behavior<DependencyObject>
        {
    ```
 1. We create member that maintains a reference to the `Compositor` instance once we have obtained a reference to it.
    ```csharp
    private static Compositor _compositor;
    ```

 1. We define a `DependencyProperty` that allows the animation duration to be specified. 
    ```csharp
            /// <summary>
            ///     The duration of the animations in MilliSeconds
            /// </summary>
            public static readonly DependencyProperty DurationMilliSecondsProperty = DependencyProperty.Register(
                "DurationMilliSeconds", typeof(double), typeof(SizeAndOffsetImplicitAnimationBehavior),
                new PropertyMetadata(200d));

            /// <summary>
            ///     The duration of the animations in MilliSeconds
            /// </summary>
            public double DurationMilliSeconds
            {
                get { return (double) GetValue(DurationMilliSecondsProperty); }
                set { SetValue(DurationMilliSecondsProperty, value); }
            }
    ``` 

 1. We define a collection to hold our implicit animations:
    ```csharp
            private ImplicitAnimationCollection _implicitAnimations;
    ```
    > **Note**: To learn more about implicit animations see @robmikh blog post [Exploring Implicit Animations](http://blog.robmikh.com/uwp/xaml/composition/2016/08/18/exploring-implicit-animations.html)

 1. We now get to the key parts of a `Behavior` - what happens when the behavior is attached to an element and when it is detached from an element.

    ```csharp
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
    ```

    In this code, we first ensure we have been attached to an instance of a `UIElement`. We then make sure we have setup our implicit animations (we'll cover that method shortly). We then determine what visual we are actually going to be animating. Here I have made an assumption - if there is no child visual (which could be either a `SpriteVisual` or a `ContainerVisual`) then we are going to animate the underlying visual for the element itself. However, if there is a child visual, then we are going to assume that is the visual that will be animated.

    > **Note**: I have created some extension methods that simplify getting the visuals - they live in the `Extensions.cs` file in the root of the `C211.Uwp.Composition` class library.

    > **Note**: In this sample, I have included a user control `VisualControl` - this control uses a `SpriteVisual` to render an image so you can see this logic working. The other two display regions are a `StackPanel` and a `ScrollViewer` - in these cases, it will the element visual that is animated.

1. Now lets look at the detaching behavior:

    ```csharp
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
    ```

    Once again, we first ensure we have been attached to an instance of a `UIElement`. We then determine what visual we have been animating. We then remove the implicit animations from it.

 1. Finally, we need to create out implicit animations:

    ```csharp
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
    ```

    We first check to see if we have already created our animations - no sense creating them again! We then grab a reference to the `Compositor` for our element. We then create two animations - one that will animate our visual's Offset (i.e. position specifed in X, Y & Z - hence `Vector3`) and another, our visual's size (specified in X & Y, hence `Vector2`). Once we have our animations, we create an `ImplicitAnimationCollection` and add our animations to the collection with a key set to the string name of the controlling property. The implicit animations are then ready to be used elsewhere.
    
    The key thing to understand about implicit animations is that once they are set up, the composition system will monitor the values of the visual properties (e.g. `Offset`) and if the system detects a change, it will run the associated animation *from* the current value *to* the new final value.

    > **Note**: To learn more about implicit animations see @robmikh blog post [Exploring Implicit Animations](http://blog.robmikh.com/uwp/xaml/composition/2016/08/18/exploring-implicit-animations.html) 

    > **Note**: I have created some extension methods that simplify grabbing the compositor and creating the animations - they live in the `Extensions.cs` file in the root of the `C211.Uwp.Composition` class library.

 1. Run the sample and you should see the smooth animations. By attaching the behavior to each of the elements within the `RelativePanel`, the composition system monitors their position and size by way fo the implicit animations. When a trigger fires to reposition an element, the implicit animation detects that change and runs an offset and/or size animation as necessary.