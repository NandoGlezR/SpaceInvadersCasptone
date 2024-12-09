using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents an enemy model in the game, including its size, destruction status, and score value.
/// </summary>
public partial class EnemyModel : IdentityObject
{
    [ObservableProperty] private bool _isDestruyed;
    [ObservableProperty] protected int _score;

    public EnemyModel()
    {
        Width = (int)EEnemy.WIDTH_SHIP;
        Height = (int)EEnemy.HEIGHT_SHIP;
    }
    /// <summary>
    /// Gets the score value associated with this enemy.
    /// </summary>
    public int GetScore
    {
        get => Score;
    }
}
