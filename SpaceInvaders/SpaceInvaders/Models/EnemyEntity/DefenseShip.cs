using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models.EnemyEntity;

public class DefenseShip : EnemyModel
{
    public DefenseShip()
    {
        Score = (int)EEnemy.DEFENSE_SHIP_SCORE;
        Texture = ETextures.DefenseShip;
    }
}
