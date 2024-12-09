using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Models;

public partial class ProtectionBlockModel : IdentityObject
{
    [ObservableProperty] private int _durability = (int)EProtectionBlock.DURABILITY;
    [ObservableProperty] private bool _isDestruyed;

    private const int MinimumStrength = 0;

    public ProtectionBlockModel()
    {
        Width = (int)EProtectionBlock.WIDTH;
        Height = (int)EProtectionBlock.HEIGHT;
    }
    /// <summary>
    /// Reduces the durability of the protection block when it takes damage.
    /// If the durability reaches or falls below the minimum strength, the block is marked as destroyed.
    /// </summary>
    public void TakeDamage()
    {
        if (Durability > MinimumStrength)
        {
            --Durability;
            if (Durability <= MinimumStrength)
            {
                IsDestruyed = true;
            }
        }
    }
}
