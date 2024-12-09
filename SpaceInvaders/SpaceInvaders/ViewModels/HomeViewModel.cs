namespace SpaceInvaders.ViewModels;

public class HomeViewModel
{
    public ICommand OpenScoreCommand { get; }

    public HomeViewModel()
    {
        OpenScoreCommand = new AsyncRelayCommand<XamlRoot>(OpenScoreAsync!);
    }
    
    /// <summary>
    /// Asynchronously opens the score dialog with the specified <see cref="XamlRoot"/>.
    /// </summary>
    /// <param name="xamlRoot">The XAML root to which the score dialog should be attached.</param>
    private async Task OpenScoreAsync(XamlRoot xamlRoot)
    {
        var scoreDialog = new ContentDialogScores()
        {
            XamlRoot = xamlRoot
        };
        await scoreDialog.ShowAsync();
    }
}
