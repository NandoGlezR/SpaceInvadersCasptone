using System.Collections.ObjectModel;
using SpaceInvaders.Core.Enums;
using SpaceInvaders.Models.EnemyEntity;

namespace SpaceInvaders.Controllers;

public partial class EnemyProjectileController : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ProjectileModel> _enemyProjectile;
    private EnemyController _enemyController;
    public EnemyProjectileController(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemyProjectile = new ObservableCollection<ProjectileModel>();
    }

    /// <summary>
    /// Updates the position of all enemy projectiles. If a projectile moves off the screen, it is marked as not fired.
    /// </summary>
    public void UpdateEnemyProjectile()
    {
        foreach (var projectile in EnemyProjectile)
        {
            projectile.Move();
            if (projectile.PositionY > (ushort)EGameScreen.SCREEN_HEIGHT)
            {
                projectile.IsFired = false;
            }
        }
    }
    
    /// <summary>
    /// Fires projectiles randomly from the attack ships. The probability of firing is determined by a predefined occurrence value.
    /// </summary>
    public void FireEnemyProjectiles()
    {
        var bulletOccurrenceValue = 0.002;
        var randomGenerationBullets = new Random();
        foreach (var attackShip in _enemyController.Enemies.Where(e => e is AttackShip).ToList())
        {
            if (randomGenerationBullets.NextDouble() < bulletOccurrenceValue)
            {
                FireEnemyProjectiles(attackShip);
            }
        }
    }
    
    /// <summary>
    /// Fires a projectile from a specific enemy ship, creating a new projectile instance and adding it to the enemy projectile collection.
    /// </summary>
    /// <param name="enemy">The enemy ship from which the projectile is fired.</param>
    private void FireEnemyProjectiles(EnemyModel enemy)
    {
        var projectile = new ProjectileModel() { HeadsDownwards = true };
        var initializeProjectile = new ProjectileController(projectile);
        initializeProjectile.CreateProjectile(enemy);
        EnemyProjectile.Add(projectile);
    }
}
