namespace SpaceInvaders.Controllers;

public class ProjectileController
{
    private const int MinimumScreenHeightLimit = 0;
    public ProjectileModel Projectile { get; }

    public ProjectileController(ProjectileModel projectile)
    {
        Projectile = projectile;
    }

    /// <summary>
    /// Updates the projectile's position if it is fired. If the projectile moves off the top of the screen, it is marked as not fired.
    /// </summary>
    public void UpdateProjectile()
    {
        if (Projectile.IsFired)
        {
            Projectile.Move();
            if (Projectile.PositionY < MinimumScreenHeightLimit)
            {
                Projectile.IsFired = false;
            }
        }
    }

    /// <summary>
    /// Creates and positions a projectile relative to a given entity, setting its initial position and marking it as fired.
    /// </summary>
    /// <param name="entity">The entity (e.g., a ship) from which the projectile is fired.</param>
    public void CreateProjectile(IdentityObject entity)
    {
        if (!Projectile.IsFired)
        {
            Projectile.PositionX = entity.PositionX + (entity.Width / 2) - (Projectile.Radius / 2);
            Projectile.PositionY = entity.PositionY - Projectile.Radius;
            Projectile.IsFired = true;
        }
    }
}
