using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models.EnemyEntity;

public class MotherShip : EnemyModel
{
    // The primary direction is a reference that moves to the left.
    public bool DirectionPrimary { get; set; }

    public MotherShip()
    {
        Width = 50;
        Height = 50;
        Score = PickerRandom();
        Texture = ETextures.MotherShip;
    }

    private int PickerRandom()
    {
        var generateRandomValue = new Random();
        var listValues = new List<int>() { 10, 40, 50, 100 };
        return  listValues[generateRandomValue.Next(listValues.Count)];
    }
}
