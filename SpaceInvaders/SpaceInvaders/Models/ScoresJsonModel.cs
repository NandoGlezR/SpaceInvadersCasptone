namespace SpaceInvaders.Models;

/// <summary>
/// Represents a model for storing player scores in JSON format.
/// </summary>
public class ScoresJsonModel
{
    public string PlayerName { get; set; } = null!;
    public int Score { get; set; }
}
