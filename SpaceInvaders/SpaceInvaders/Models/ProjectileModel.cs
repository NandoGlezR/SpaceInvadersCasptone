using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models;

public partial class ProjectileModel : ObservableObject
{
    [ObservableProperty] private bool _isFired;

    [ObservableProperty] private double _speed = (int)EProjectile.SPEED;

    [ObservableProperty] private double _radius = (int)EProjectile.RADIUS;

    [ObservableProperty] private double _positionX;

    [ObservableProperty] private double _positionY;
    [ObservableProperty] private bool _headsDownwards;
    /// <summary>
    /// Moves the projectile along the Y-axis based on its speed and direction.
    /// If <see cref="HeadsDownwards"/> is true, the projectile moves downwards; otherwise, it moves upwards.
    /// </summary>
    public void Move()
    {
        PositionY += HeadsDownwards ? Speed : -Speed;
    }
}
