using Microsoft.UI.Xaml.Input;
using Windows.System;
using SpaceInvaders.ViewModels;


namespace SpaceInvaders.Presentation;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GamePage : Page
{
    private DispatcherTimer _timer;
    private GameViewModel _viewModel => (GameViewModel)DataContext;
    public GamePage()
    {
        InitializeComponent();
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(30);
        _timer.Tick += OnGameTick!;
        _timer.Start();
    }

    /// <summary>
    /// Handles the game loop tick, updating the game state by moving entities, checking for collisions, and handling game-over conditions.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data.</param>
    private void OnGameTick(object sender, object e)
    {
        _viewModel.EnemyController.MoveMotherShip();
        _viewModel.EnemyController.MoveEnemies();
        _viewModel.EnemyProjectileController.FireEnemyProjectiles();
        _viewModel.ProjectileController.Projectile.Move();
        _viewModel.ProjectileController.UpdateProjectile();
        _viewModel.CollisionController.CheckCollisions();
        
        _viewModel.EnemyProjectileController.UpdateEnemyProjectile();
        _viewModel.EnemyController.CheckLevelSkip();
        if (_viewModel.IsGameOver())
        {
            _timer.Stop();
            ShowGameOverAsync();
        }
    }
    
    /// <summary>
    /// Handles keyboard input for controlling the player's ship, allowing movement and shooting.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data containing the key that was pressed.</param>
    private void KeyDownPlayer(object sender, KeyRoutedEventArgs e)
    {
        switch (e.Key)
        {
            case VirtualKey.Left:
                _viewModel.MovePlayerLeftCommand.Execute(null);
                break;
            case VirtualKey.Right:
                _viewModel.MovePlayerRightCommand.Execute(null);
                break;
            case VirtualKey.Space:
                _viewModel.ShootPlayerCommand.Execute(null);
                break;
        }
    }

    /// <summary>
    /// Displays the game-over dialog, allowing the player to save their score and return to the home page.
    /// </summary>
    private async void ShowGameOverAsync()
    {
        var scoreViewModel = new SaveScoreViewModel();
        scoreViewModel.TextPlayerScore = _viewModel.CollisionController.ScoreTotal;
        var saveScore = new ContentDialogSaveScore()
        {
            DataContext = scoreViewModel,
            XamlRoot = XamlRoot
        };
       var optionSelect =  await saveScore.ShowAsync();
       if (optionSelect == ContentDialogResult.Primary)
       {
           Frame.Navigate(typeof(HomePage));
       }
    }
}
