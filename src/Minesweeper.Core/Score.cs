namespace Minesweeper.Core;

public class Score
{
    public required string PlayerName { get; set; }
    public int Time { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"{PlayerName},{Time},{Date}";
    }

    public static Score FromString(string line)
    {
        var parts = line.Split(',');

        if (parts.Length != 3)
            throw new FormatException("Invalid score format");

        return new Score
        {
            PlayerName = parts[0],
            Time = int.TryParse(parts[1], out int t) ? t : 0,
            Date = DateTime.TryParse(parts[2], out DateTime d) ? d : DateTime.Now
        };
    }
}