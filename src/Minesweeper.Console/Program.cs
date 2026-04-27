using System;
using Minesweeper.Core;

class Program
{
    static void Main()
{
    Board board = new Board(8, 10, 42);

    var startTime = DateTime.Now; // ✅ start timer ONCE
    HighScoreManager manager = new HighScoreManager();

    while (true)
    {
        PrintBoard(board);

        Console.Write("Enter move (r c or f r c): ");
        var input = Console.ReadLine()?.Split(' ');

        if (input == null) continue;

        if (input[0] == "f")
        {
            int r = int.Parse(input[1]);
            int c = int.Parse(input[2]);
            board.ToggleFlag(r, c);
        }
        else
        {
            int r = int.Parse(input[0]);
            int c = int.Parse(input[1]);

            if (board.Grid[r, c].IsMine)
            {
                Console.WriteLine(" Game Over!");
                break;
            }

            board.Reveal(r, c);
        }

        if (CheckWin(board))
        {
            Console.WriteLine("You win!");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "Player";

            int time = (int)(DateTime.Now - startTime).TotalSeconds;

            manager.SaveScore(new Score
            {
                PlayerName = name,
                Time = time,
                Date = DateTime.Now
            });

            Console.WriteLine("Score saved!");

            break; // ✅ stop game after win
        }
    }

    // ✅ show scores AFTER game ends
    var scores = manager.LoadScores();

    Console.WriteLine("\n--- High Scores ---");
    foreach (var s in scores)
    {
        Console.WriteLine($"{s.PlayerName} - {s.Time}s");
    }
}

  static void PrintBoard(Board board)
{
    // Print column numbers
    Console.Write("  ");
    for (int c = 0; c < board.Size; c++)
    {
        Console.Write(c + " ");
    }
    Console.WriteLine();

    for (int r = 0; r < board.Size; r++)
    {
        // Print row number
        Console.Write(r + " ");

        for (int c = 0; c < board.Size; c++)
        {
            var tile = board.Grid[r, c];

            if (tile.IsFlagged)
                Console.Write("F ");
            else if (!tile.IsRevealed)
                Console.Write(". ");
            else if (tile.IsMine)
                Console.Write("* ");
            else
                Console.Write(tile.AdjacentMines + " ");
        }

        Console.WriteLine();
    }
}

    static bool CheckWin(Board board)
    {
        for (int r = 0; r < board.Size; r++)
        {
            for (int c = 0; c < board.Size; c++)
            {
                var tile = board.Grid[r, c];

                if (!tile.IsMine && !tile.IsRevealed)
                    return false;
            }
        }
        return true;
    }
}