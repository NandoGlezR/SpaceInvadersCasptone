namespace SpaceInvaders.Presentation;

public partial class ControlsPage : Page
{
    public ControlsPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Navigates to the controls page when the user selects the option to view home controls.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data associated with the click event.</param>
    private void NavigateBack(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(HomePage));
    }
}

