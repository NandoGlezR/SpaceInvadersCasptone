using System.Collections.ObjectModel;
using SpaceInvaders.Core.Enums;

namespace SpaceInvaders.Controllers;

public partial class ProtectionController : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ProtectionBlockModel> _protectionBlockCollection;

    public ProtectionController()
    {
        _protectionBlockCollection = new ObservableCollection<ProtectionBlockModel>();
        InitializeProtectionBlocks();
    }

    
    /// <summary>
    /// Initializes the protection blocks, arranging them into multiple structures that provide cover for the player.
    /// </summary>
    private void InitializeProtectionBlocks()
    {
        double startX = (int)EProtectionBlock.INITIAL_POSITION_X;
        double startY = (int)EProtectionBlock.INITIAL_POSITION_Y;
        double blockWidth = (int)EProtectionBlock.WIDTH;
        double blockHeight = (int)EProtectionBlock.HEIGHT;
        double columnSpacing = 5;
        int addThreeBlock = 3;
        int addTwoBlock = 2;
        int numberProtectionStructures = 4;
        for (int structure = 0; structure < numberProtectionStructures; structure++)
        {
            double structureX = startX + structure * (blockWidth * 5 + columnSpacing * 2); 
            for (int row = 0; row < 2; row++)
            {
                for (int column = 0; column < (row == 0 ? addThreeBlock : addTwoBlock); column++)
                {
                    // To give structure, when the condition is met move the block position to column 2.
                    double posX = column == 1 && row == 1 ? structureX + 2 * blockWidth : structureX + column * blockWidth;
                    var block = new ProtectionBlockModel()
                    {
                        PositionX = posX,
                        PositionY = startY + row * (blockHeight)
                    };
                    ProtectionBlockCollection.Add(block);
                }
            }
        }
    }
}
