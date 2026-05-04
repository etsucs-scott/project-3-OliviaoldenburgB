using System;

namespace Minesweeper.Core;

public class Board
{
    public int Size { get; }
    public Tile[,] Grid { get; }

    private Random random;

    public Board(int size, int mineCount, int seed)
    {
        Size = size;
        Grid = new Tile[size, size];
        random = new Random(seed);

        InitializeBoard();
        PlaceMines(mineCount);
        CalculateAdjacency();
    }

    private void InitializeBoard()
    {
        for (int r = 0; r < Size; r++)
            for (int c = 0; c < Size; c++)
                Grid[r, c] = new Tile();
    }

    private void PlaceMines(int mineCount)
    {
        int placed = 0;

        while (placed < mineCount)
        {
            int r = random.Next(Size);
            int c = random.Next(Size);

            if (!Grid[r, c].IsMine)
            {
                Grid[r, c].IsMine = true;
                placed++;
            }
        }
    }

    private void CalculateAdjacency()
    {
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                if (Grid[r, c].IsMine)
                    continue;

                int count = 0;

                for (int dr = -1; dr <= 1; dr++)
                {
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        if (dr == 0 && dc == 0)
                            continue;

                        int nr = r + dr;
                        int nc = c + dc;

                        if (IsInBounds(nr, nc) && Grid[nr, nc].IsMine)
                            count++;
                    }
                }

                Grid[r, c].AdjacentMines = count;
            }
        }
    }

    public bool IsInBounds(int r, int c)
    {
        return r >= 0 && r < Size && c >= 0 && c < Size;
    }

    public void Reveal(int r, int c)
    {
        if (!IsInBounds(r, c))
            return;

        var tile = Grid[r, c];

        if (tile.IsRevealed || tile.IsFlagged)
            return;

        tile.IsRevealed = true;

        if (tile.IsMine || tile.AdjacentMines != 0)
            return;

        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                Reveal(r + dr, c + dc);
            }
        }
    }

    public void ToggleFlag(int r, int c)
    {
        if (!IsInBounds(r, c))
            return;

        var tile = Grid[r, c];

        if (!tile.IsRevealed)
            tile.IsFlagged = !tile.IsFlagged;
    }

    public bool AllSafeTilesRevealed()
    {
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                var tile = Grid[r, c];

                if (!tile.IsMine && !tile.IsRevealed)
                    return false;
            }
        }
        return true;
    }
}