using System.Collections.ObjectModel;
using SpaceInvaders.Core.Enums;
using SpaceInvaders.Models.EnemyEntity;

namespace SpaceInvaders.Controllers;

public partial class EnemyController : ObservableObject
{
    [ObservableProperty] private ObservableCollection<EnemyModel> _enemies;
    [ObservableProperty] private double _screenWidth = (int)EGameScreen.SCREEN_WIDTH;
    [ObservableProperty] private double _screenHeight = (int)EGameScreen.SCREEN_HEIGHT;
    [ObservableProperty] private MotherShip? _supremeShip;
    private readonly double _enemyHeight = (int)EEnemy.HEIGHT_SHIP;
    private readonly double _enemyWidth = (int)EEnemy.WIDTH_SHIP;

    private double _enemySpeed = (int)EEnemy.SPEED;
    private double _motherShipSpeed = (int)EEnemy.SPEED_MOTHER_SHIP;
    private readonly double _separation = (int)EEnemy.SEPARATION_BETWEEN_SPACE_SHIPS;
    private bool _movingDown = true;
    private const double AmountIncreaseSpeed = 0.2;
    private PlayerModel _player;
    private readonly DispatcherTimer _motherShipTimer;

    public EnemyController(PlayerModel player)
    {
        _player = player;
        _enemies = new ObservableCollection<EnemyModel>();
        InitializeCollectionEnemies();

        _motherShipTimer = new DispatcherTimer();
        _motherShipTimer.Tick += (sender, e) => CreateMotherShip();
        SetMotherShipTimerInterval();
    }

    /// <summary>
    /// Sets a random interval for the Mother Ship timer and starts the timer.
    /// </summary>
    private void SetMotherShipTimerInterval()
    {
        var random = new Random();
        _motherShipTimer.Interval = TimeSpan.FromSeconds(random.Next(20, 60));
        _motherShipTimer.Start();
    }

    /// <summary>
    /// Initializes the enemy collection with a grid of different types of enemy ships.
    /// </summary>
    private void InitializeCollectionEnemies()
    {
        const int totalRow = 5;
        const int totalColumns = 11;
        var horizontalSeparation = _separation;
        var verticalSeparation = _separation;

        for (int row = 0; row < totalRow; row++)
        {
            for (int column = 0; column < totalColumns; column++)
            {
                EnemyModel enemy;
                if (row == 0)
                {
                    enemy = new AttackShip();
                }

                else if (row == 1 || row == 2)
                {
                    enemy = new DefenseShip();
                }
                else
                {
                    enemy = new ProtectionShip();
                }

                enemy.PositionX = horizontalSeparation + (column * (_enemyWidth + _separation));
                enemy.PositionY = verticalSeparation + (row * (_enemyHeight + _separation));
                Enemies.Add(enemy);
            }
        }
    }

    /// <summary>
    /// Creates a new Mother Ship and sets its initial position and movement direction.
    /// </summary>
    private void CreateMotherShip()
    {
        var random = new Random();
        var direction = random.Next(1, 3);

        SupremeShip = new MotherShip
        {
            PositionY = 10,
            PositionX = direction == 1 ? ScreenWidth : 0 - _enemyWidth
        };

        if (direction == 1)
        {
            SupremeShip.DirectionPrimary = true;
        }

        // Reset timer for next appearance
        SetMotherShipTimerInterval();
    }

    /// <summary>
    /// Moves all enemies in the current direction. If they reach the screen edge, they move down and reverse direction.
    /// The speed of the enemies and the Mother Ship is increased each time they reverse direction.
    /// </summary>
    public void MoveEnemies()
    {
        bool reachedEdge = false;

        foreach (var enemy in Enemies)
        {
            if (_movingDown)
            {
                enemy.PositionX += _enemySpeed;
                if (enemy.PositionX + enemy.Width >= ScreenWidth)
                {
                    reachedEdge = true;
                }
            }
            else
            {
                enemy.PositionX -= _enemySpeed;
                if (enemy.PositionX <= 0)
                {
                    reachedEdge = true;
                }
            }
        }

        if (reachedEdge)
        {
            foreach (var enemy in Enemies)
            {
                enemy.PositionY += _enemyHeight;
            }

            _movingDown = !_movingDown;
            _enemySpeed += AmountIncreaseSpeed;
            _motherShipSpeed += AmountIncreaseSpeed;
        }
    }

    /// <summary>
    /// Moves the Mother Ship across the screen. If it reaches the screen edge, it is removed from the game.
    /// </summary>
    public void MoveMotherShip()
    {
        if (SupremeShip == null) return;
        if (SupremeShip.DirectionPrimary)
        {
            SupremeShip.PositionX -= _motherShipSpeed;
            if (SupremeShip.PositionX < 0)
            {
                SupremeShip = null;
            }
        }
        else
        {
            SupremeShip.PositionX += _motherShipSpeed;
            if (SupremeShip.PositionX + SupremeShip.Width > ScreenWidth)
            {
                SupremeShip = null;
            }
        }
    }

    /// <summary>
    /// Checks if all enemies have been destroyed. If so, the player's life is regenerated, and a new set of enemies is initialized.
    /// </summary>
    public void CheckLevelSkip()
    {
        var quantityShip = Enemies.Count;
        if (quantityShip == 0)
        {
            Enemies.Clear();
            _player.GenerateNewLife();
            InitializeCollectionEnemies();
        }
    }
}
