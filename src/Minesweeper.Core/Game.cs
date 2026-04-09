namespace Minesweeper.Core;

public class Game
{
    public Board Board { get; }
    public bool IsGameOver { get; private set; }
    public bool IsWin { get; private set; }
    public int Moves { get; private set; }

    public Game(int size, int mineCount, int seed)
    {
        Board = new Board(size, mineCount, seed);
        IsGameOver = false;
        IsWin = false;
        Moves = 0;
    }

    // Reveal a tile
    public void Reveal(int r, int c)
    {
        if (IsGameOver)
            return;

        var tile = Board.Grid[r, c];

        // Don't reveal flagged tiles
        if (tile.IsFlagged)
            return;

        Moves++;

        // Hit a mine → lose
        if (tile.IsMine)
        {
            tile.IsRevealed = true;
            IsGameOver = true;
            IsWin = false;
            return;
        }

        Board.Reveal(r, c);

        CheckWin();
    }

    // Flag / unflag
    public void ToggleFlag(int r, int c)
    {
        if (IsGameOver)
            return;

        Board.ToggleFlag(r, c);
    }

    // Check win condition
    private void CheckWin()
    {
        foreach (var tile in Board.Grid)
        {
            if (!tile.IsMine && !tile.IsRevealed)
                return;
        }

        IsGameOver = true;
        IsWin = true;
    }
}