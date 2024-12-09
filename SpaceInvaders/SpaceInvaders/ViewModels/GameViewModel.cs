using SpaceInvaders.Controllers;
using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.ViewModels;
/// <summary>
/// Represents the view model for the game, managing player actions, game state, and interactions between various game components.
/// </summary>
internal partial class GameViewModel : ObservableObject
{
    [ObservableProperty] private PlayerModel _player;

    [ObservableProperty] private double _screenWidth = (int)EGameScreen.SCREEN_WIDTH;
    private bool _gameOver;
    public ProjectileController ProjectileController { get; }
    public EnemyProjectileController EnemyProjectileController { get; }
    public EnemyController EnemyController { get; }
    public CollisionController CollisionController { get; private set; }
    public ProtectionController ProtectionController { get; private set; }
    public ICommand MovePlayerRightCommand { get; }
    public ICommand MovePlayerLeftCommand { get; }
    public ICommand ShootPlayerCommand { get; }

    public GameViewModel()
    {
        _player = new PlayerModel();
        var projectile = new ProjectileModel();
        EnemyController = new EnemyController(_player);
        EnemyProjectileController = new EnemyProjectileController(EnemyController);
        ProjectileController = new ProjectileController(projectile);
        ProtectionController = new ProtectionController();
        CollisionController = new CollisionController(EnemyController, ProjectileController, ProtectionController,
            EnemyProjectileController, _player);
        MovePlayerLeftCommand = new RelayCommand(MoveLeft);
        MovePlayerRightCommand = new RelayCommand(MoveRight);
        ShootPlayerCommand = new RelayCommand(Shoot);
    }

    /// <summary>
    /// Moves the player to the left, ensuring they do not move beyond the left edge of the screen.
    /// </summary>
    private void MoveLeft()
    {
        if (Player.PositionX - Player.Speed >= 0)
        {
            Player.MoveLeft();
        }
    }

    /// <summary>
    /// Moves the player to the right, ensuring they do not move beyond the right edge of the screen.
    /// </summary>
    private void MoveRight()
    {
        if (Player.PositionX + Player.Width + Player.Speed <= ScreenWidth)
        {
            Player.MoveRigth();
        }
    }

    /// <summary>
    /// Makes the player shoot a projectile.
    /// </summary>
    private void Shoot()
    {
        ProjectileController.CreateProjectile(Player);
    }

    public bool IsGameOver()
    {
        if (Player.Lives == 0)
        {
            return _gameOver = true;
        }
        return _gameOver;
    }
}
