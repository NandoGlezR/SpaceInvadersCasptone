using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models.EnemyEntity;

public class ProtectionShip : EnemyModel
{
    public ProtectionShip()
    {
        Score = (int)EEnemy.PROTECTION_SHIP_SCORE;
        Texture = ETextures.ProtectionShip;
    }
}
