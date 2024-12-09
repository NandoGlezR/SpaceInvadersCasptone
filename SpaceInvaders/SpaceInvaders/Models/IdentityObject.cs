namespace SpaceInvaders.Models;

/// <summary>
/// Represents a base class for game objects that have a position, size, and texture.
/// Provides common properties and methods for handling game object interactions.
/// </summary>

public abstract partial class IdentityObject : ObservableObject
{
    [ObservableProperty] private double _positionX;

    [ObservableProperty] private double _positionY;

    [ObservableProperty] private double _height;

    [ObservableProperty] private double _width;
    [ObservableProperty] private string _texture;
    /// <summary>
    /// This variable is necessary to adjust if a projectile does not collide.
    /// </summary>
    private const uint CollisionBorder = 10;

    /// <summary>
    /// Checks if a projectile has collided with this object based on its position and size.
    /// </summary>
    /// <param name="projectile">The projectile to check for collision.</param>
    /// <returns>True if the projectile has collided with this object; otherwise, false.</returns>
    public bool CheckCollision(ProjectileModel projectile)
    {
        return (projectile.PositionX > PositionX && projectile.PositionX < PositionX + (Width + CollisionBorder)) &&
               (projectile.PositionY > PositionY && projectile.PositionY < PositionY + Height);
    }
}
