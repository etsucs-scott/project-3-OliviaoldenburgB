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

    // Create empty tiles
    private void InitializeBoard()
    {
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                Grid[r, c] = new Tile();
            }
        }
    }

    // Place mines using seed
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

    // Count adjacent mines
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
                        int nr = r + dr;
                        int nc = c + dc;

                        if (nr >= 0 && nr < Size && nc >= 0 && nc < Size)
                        {
                            if (Grid[nr, nc].IsMine)
                                count++;
                        }
                    }
                }

                Grid[r, c].AdjacentMines = count;
            }
        }
    }

    // Reveal tile (with cascade)
    public void Reveal(int r, int c)
    {
        var tile = Grid[r, c];

        if (tile.IsRevealed || tile.IsFlagged)
            return;

        tile.IsRevealed = true;

        // Cascade if no adjacent mines
        if (!tile.IsMine && tile.AdjacentMines == 0)
        {
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    int nr = r + dr;
                    int nc = c + dc;

                    if (nr >= 0 && nr < Size && nc >= 0 && nc < Size)
                    {
                        Reveal(nr, nc);
                    }
                }
            }
        }
    }

    // Flag / unflag tile
    public void ToggleFlag(int r, int c)
    {
        var tile = Grid[r, c];

        if (!tile.IsRevealed)
        {
            tile.IsFlagged = !tile.IsFlagged;
        }
    }
}