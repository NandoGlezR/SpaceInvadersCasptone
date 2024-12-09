using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models.EnemyEntity;

public class AttackShip : EnemyModel
{
   public AttackShip()
   {
       Score = (int)EEnemy.ATTACK_SHIP_SCORE;
       Texture = ETextures.AttackShip;
   }
   
}
