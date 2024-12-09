using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models;

public partial class PlayerModel : IdentityObject
{
    [ObservableProperty] private double _speed = (int)EPlayer.SPEED;
    [ObservableProperty] private int _lives = (int)EPlayer.LIVES;
    private const int MaximumLivesAllow = (int)EPlayer.MAXIMUM_LIVES_ALLOWED;
    public PlayerModel()
    {
        PositionX = (int)EPlayer.X;
        PositionY = (int)EPlayer.Y;
        Width = (int)EPlayer.WIDTH;
        Height = (int)EPlayer.HEIGHT;
        Texture = ETextures.PlayerShip;
    }
    /// <summary>
    /// Increases the player's lives by one, if the current number of lives is less than the maximum allowed.
    /// </summary>
    public void GenerateNewLife()
    {
        if (Lives < MaximumLivesAllow)
        {
            Lives++;
        }
    }

    /// <summary>
    /// Moves the player to the left by subtracting the player's speed from their current X-coordinate.
    /// </summary>
    public void MoveLeft()
    {
        PositionX -= Speed;
    }

    /// <summary>
    /// Moves the player to the right by adding the player's speed to their current X-coordinate.
    /// </summary>
    public void MoveRigth()
    {
        PositionX += Speed;
    }
}
