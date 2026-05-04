using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Minesweeper.Core;

public class HighScoreManager
{
    private readonly string filePath = "highscores.txt";

    public List<Score> LoadScores()
    {
        if (!File.Exists(filePath))
            return new List<Score>();

        return File.ReadAllLines(filePath)
                   .Select(line => Score.FromString(line))
                   .OrderBy(s => s.Time)
                   .ToList();
    }

    public void SaveScore(Score newScore)
    {
        var scores = LoadScores();
        scores.Add(newScore);

        scores = scores.OrderBy(s => s.Time)
                       .Take(10)
                       .ToList();

        File.WriteAllLines(filePath, scores.Select(s => s.ToString()));
    }
}