using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Uno.Extensions.Specialized;

namespace SpaceInvaders.ViewModels;

public partial class ScoresViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ScoresJsonModel> _scoreCollection;
    private const string Path = @"./Assets/Documents/scores.json";

    public ScoresViewModel()
    {
        _scoreCollection = new ObservableCollection<ScoresJsonModel>();
        ReadJson();
        SortByHighestScore();
    }

    /// <summary>
    /// Reads the player scores from the JSON file and adds them to the <see cref="ScoreCollection"/>.
    /// </summary>
    private void ReadJson()
    {
        string json = File.ReadAllText(Path);
        var scores = JsonConvert.DeserializeObject<List<ScoresJsonModel>>(json)!;
        foreach (var score in scores)
        {
            ScoreCollection.Add(score);
        }
    }

    /// <summary>
    /// Sorts the <see cref="ScoreCollection"/> by score in descending order, so that the highest scores appear first.
    /// </summary>
    private void SortByHighestScore()
    {
        var scores = ScoreCollection.OrderByDescending(score => score.Score).ToList();
        ScoreCollection.Clear();
        foreach (var itemScore in scores)
        {
            ScoreCollection.Add(itemScore);
        }
    }
}
