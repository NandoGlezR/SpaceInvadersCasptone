using SpaceInvaders.ViewModels;

namespace SpaceInvaders.Presentation;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    private HomeViewModel _viewModel => (HomeViewModel)DataContext;
    public HomePage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Navigates to the game page when the user selects the option to start the game.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data associated with the click event.</param>
    private void NavigateGameView(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(GamePage));
    }

    /// <summary>
    /// Opens the scores dialog to display the player scores when the user selects the option to view scores.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data associated with the click event.</param>
    private void OpenDialogScores(object sender, RoutedEventArgs e)
    {
        _viewModel.OpenScoreCommand.Execute(this.XamlRoot);
    }

    /// <summary>
    /// Navigates to the controls page when the user selects the option to view game controls.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data associated with the click event.</param>
    private void NavigateControlsView(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ControlsPage));
    }
}
