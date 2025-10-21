namespace Invaders;

public class SaveFile
{
    string fileName;
    string path;
    
    public SaveFile(string fileName)
    {
        this.fileName = fileName;
        path = $"assets/{fileName}.txt";
    }

    public void Save(string name, int value)
    {
        var scores = LoadAll();
        scores.Add((name, value));
        
        scores.Sort((a, b) => b.Score.CompareTo(a.Score));

        if (scores.Count > 5)
        {
            scores = scores.Take(5).ToList();
        }

        File.WriteAllLines(path, scores.Select(s => $"{s.Name}-{s.Score}"));
    }

    public List<(string Name, int Score)> LoadAll()
    {
        List<(string Name, int Score)> scores = new List<(string Name, int Score)>();

        if (File.Exists(path))
        {
            foreach (string line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    scores.Add((parts[0], score));
                }
            }
        }
        
        scores.Sort((a, b) => b.Score.CompareTo(a.Score));
        
        return scores;
    }
}