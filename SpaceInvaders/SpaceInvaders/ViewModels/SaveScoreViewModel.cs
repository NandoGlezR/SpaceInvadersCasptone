using Newtonsoft.Json;

namespace SpaceInvaders.ViewModels;

/// <summary>
/// Represents the view model responsible for saving player scores to a JSON file.
/// </summary>
public partial class SaveScoreViewModel : ObservableObject
{
    [ObservableProperty] private string _textPlayerName;
    [ObservableProperty] private int _textPlayerScore;
    private const string Path = @"./Assets/Documents/scores.json";
    public ICommand SavePlayerCommand { get; }

    public SaveScoreViewModel()
    {
        SavePlayerCommand = new RelayCommand(WriteJson);
    }
   
    /// <summary>
    /// Writes the player's name and score to a JSON file. If the file already exists, it appends the new score; otherwise, it creates a new file.
    /// </summary>
    private void WriteJson()
    {
        List<ScoresJsonModel> scores;
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);
            scores = JsonConvert.DeserializeObject<List<ScoresJsonModel>>(json) ?? new List<ScoresJsonModel>();
        }
        else
        {
            scores = new List<ScoresJsonModel>();
        }
        scores.Add(new ScoresJsonModel(){PlayerName = TextPlayerName, Score = TextPlayerScore});
        string jsonWrite = JsonConvert.SerializeObject(scores, Formatting.Indented);
        File.WriteAllText(Path, jsonWrite);
    }
}
