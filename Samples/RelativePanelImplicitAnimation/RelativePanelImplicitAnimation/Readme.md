# Relative Panel Implicit Animations #

This sample demonstrates how a `Behavior` can be used to improve the appearance of an app when it is being resized. If an unmodified UWP application that defines a number of visual states using `AdaptiveTriggers` is resized, each state is applied instantly resulting in a jarring change of layout, etc. By leverageing Windows UI Composition Implicit Animations we are able to have the composition visual layer automatically move and size our elements to provide a more pleasing effect.

## Assumptions ##
In this sample I am assuming the following:

* Visual Triggers are used to define layouts for specific view dimensions
* RelativePanels are used to position elements

## Dependencies
In this sample we are using:

* Robmikh.CompositionSurfaceFactory to load an image

### Getting Started ###
1. `Main.xaml` defines the layout for our sample app. We start with a `Grid` element as out LayoutRoot:

```xaml
<Grid x:Name="LayoutRoot">

</Grid>
```

2. We then add the visual state defintions that determine how our UI reacts based upon the view size:

```xaml
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

3. No we are going to create our layout. We will use a `RelativePanel` to position 3 blocks of content:
    * An image
    * A description of the image
    * A scrolling region of items representing CommentsScrollviewer

    Insert the following below the `</VisualStateManager.VisualStateGroups>` line:

    ```xaml
            <RelativePanel HorizontalAlignment="Stretch"
                       Margin="20">

            <controls:VisualControl ImageUri="ms-appx:///assets/IMG_1343.JPG"
                                    x:Name="Poster"
                                    Width="600"
                                    Height="900"
                                    Margin="12">
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

            </StackPanel>

            <ScrollViewer x:Name="CommentsScrollviewer">
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

