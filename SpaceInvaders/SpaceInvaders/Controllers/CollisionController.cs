using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Controllers;

public partial class CollisionController(
    EnemyController enemyController,
    ProjectileController projectileController,
    ProtectionController protectionController,
    EnemyProjectileController enemyProjectileController,
    PlayerModel player) : ObservableObject
{
    private const int MinimumLifeLimit = 0;
    private const int RemoveFromScreenBorder = -10;
    private const int RemoveFromScreenHeight = (int)EGameScreen.SCREEN_HEIGHT + 10;
    [ObservableProperty] private int _scoreTotal;

    /// <summary>
    /// Checks and handles all collision scenarios in the game.
    /// </summary>
    public void CheckCollisions()
    {
        CheckIfAnyCollidesPlayer();
        CheckProjectileWithEnemyCollision();
        CheckProtectionBlockAnyCollision();
        RemoveDestroyedEnemy();
        RemoveDestroyedProtectionBlock();
        CheckProjectileWithPlayerCollision();
    }

    /// <summary>
    /// Checks if any enemy projectile has collided with the player.
    /// If a collision is detected, the projectile is marked as not fired, moved off-screen, and the player's lives are decremented.
    /// </summary>
    private void CheckProjectileWithPlayerCollision()
    {
        foreach (var enemyProjectile in enemyProjectileController.EnemyProjectile.ToList()
                     .Where(projectile => player.Lives > MinimumLifeLimit && player.CheckCollision(projectile)))
        {
            enemyProjectile.IsFired = false;
            enemyProjectile.PositionY = RemoveFromScreenHeight;
            player.Lives--;
        }
    }

    /// <summary>
    /// Checks if any enemy has reached the player's position on the Y-axis.
    /// If a collision is detected, the player's lives are decremented.
    /// </summary>
    private void CheckIfAnyCollidesPlayer()
    {
        if (enemyController.Enemies.Any(enemy => enemy.PositionY >= player.PositionY))
        {
            player.Lives--;
        }
    }

    /// <summary>
    /// Checks if any player projectile has collided with an enemy or a supreme ship.
    /// If a collision is detected, the projectile is marked as not fired, moved off-screen, and the enemy or supreme ship is destroyed.
    /// The player's score is incremented based on the enemy's value.
    /// </summary>
    private void CheckProjectileWithEnemyCollision()
    {
        if (enemyController.SupremeShip != null &&
            enemyController.SupremeShip.CheckCollision(projectileController.Projectile))
        {
            ScoreTotal += enemyController.SupremeShip.GetScore;
            enemyController.SupremeShip = null;
        }

        foreach (var enemy in enemyController.Enemies.ToList().Where(enemy =>
                     !enemy.IsDestruyed && enemy.CheckCollision(projectileController.Projectile)))
        {
            projectileController.Projectile.IsFired = false;
            projectileController.Projectile.PositionY = RemoveFromScreenBorder;
            enemy.IsDestruyed = true;
            ScoreTotal += enemy.GetScore;
            break;
        }
    }

    /// <summary>
    /// Checks if any projectile, either from the player or enemies, has collided with a protective block.
    /// If a collision is detected, the projectile is marked as not fired, moved off-screen, and the block's health is reduced.
    /// </summary>
    private void CheckProtectionBlockAnyCollision()
    {
        foreach (var block in protectionController.ProtectionBlockCollection.ToList())
        {
            if (!block.IsDestruyed && block.CheckCollision((projectileController.Projectile)))
            {
                projectileController.Projectile.IsFired = false;
                projectileController.Projectile.PositionY = RemoveFromScreenBorder;
                block.TakeDamage();
            }

            foreach (var enemyProjectile in enemyProjectileController.EnemyProjectile.ToList()
                         .Where(enemyProjectile => !block.IsDestruyed && block.CheckCollision((enemyProjectile))))
            {
                enemyProjectile.IsFired = false;
                enemyProjectile.PositionY = RemoveFromScreenHeight;
                block.TakeDamage();
            }
        }
    }

    /// <summary>
    /// Removes all destroyed enemies from the game.
    /// </summary>
    private void RemoveDestroyedEnemy()
    {
        var removeEnemies = enemyController.Enemies.Where(enemy => enemy.IsDestruyed).ToList();
        foreach (var enemy in removeEnemies)
        {
            enemyController.Enemies.Remove(enemy);
        }
    }

    /// <summary>
    /// Removes all destroyed protective blocks from the game.
    /// </summary>
    private void RemoveDestroyedProtectionBlock()
    {
        var removeBlock = protectionController.ProtectionBlockCollection.Where(block => block.IsDestruyed).ToList();
        foreach (var block in removeBlock)
        {
            protectionController.ProtectionBlockCollection.Remove(block);
        }
    }
}
