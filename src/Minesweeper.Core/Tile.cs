namespace Minesweeper.Core;

public class Tile
{
    public bool IsMine { get; set; }
    public bool IsRevealed { get; set; }
    public bool IsFlagged { get; set; }
    public int AdjacentMines { get; set; }

    public override string ToString()
    {
        if (IsFlagged) return "F";
        if (!IsRevealed) return ".";
        if (IsMine) return "*";
        return AdjacentMines == 0 ? " " : AdjacentMines.ToString();
    }
}