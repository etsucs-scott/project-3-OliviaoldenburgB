using System;

namespace Minesweeper.Core;

public class Game
{
    public Board Board { get; }
    public bool IsGameOver { get; private set; }
    public bool IsWin { get; private set; }
    public int Moves { get; private set; }

    public DateTime StartTime { get; }
    public DateTime? EndTime { get; private set; }

    public Game(int size, int mineCount, int seed)
    {
        Board = new Board(size, mineCount, seed);
        IsGameOver = false;
        IsWin = false;
        Moves = 0;
        StartTime = DateTime.Now;
    }

    public void Reveal(int r, int c)
    {
        if (IsGameOver || !Board.IsInBounds(r, c))
            return;

        var tile = Board.Grid[r, c];

        if (tile.IsFlagged || tile.IsRevealed)
            return;

        Moves++;

        if (tile.IsMine)
        {
            tile.IsRevealed = true;
            IsGameOver = true;
            IsWin = false;
            EndTime = DateTime.Now;
            return;
        }

        Board.Reveal(r, c);

        CheckWin();
    }

    public void ToggleFlag(int r, int c)
    {
        if (IsGameOver || !Board.IsInBounds(r, c))
            return;

        Board.ToggleFlag(r, c);
    }

    private void CheckWin()
    {
        if (Board.AllSafeTilesRevealed())
        {
            IsGameOver = true;
            IsWin = true;
            EndTime = DateTime.Now;
        }
    }
}