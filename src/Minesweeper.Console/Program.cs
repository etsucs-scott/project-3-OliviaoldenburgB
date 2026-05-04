using System;
using Minesweeper.Core;

class Program
{
    static void Main()
    {
        HighScoreManager manager = new HighScoreManager();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MINESWEEPER ===");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. High Scores");
            Console.WriteLine("3. Exit");

            string choice = Console.ReadLine();

            if (choice == "1")
                StartGame(manager);
            else if (choice == "2")
                ShowScores(manager);
            else if (choice == "3")
                break;
        }
    }

    static void StartGame(HighScoreManager manager)
    {
        Console.Clear();

        Console.WriteLine("Choose difficulty:");
        Console.WriteLine("1. Easy (8x8, 10 mines)");
        Console.WriteLine("2. Medium (12x12, 20 mines)");
        Console.WriteLine("3. Hard (16x16, 40 mines)");

        string choice = Console.ReadLine();

        int size = 8;
        int mines = 10;

        switch (choice)
        {
            case "2":
                size = 12;
                mines = 20;
                break;
            case "3":
                size = 16;
                mines = 40;
                break;
        }

        int seed = new Random().Next();
        Game game = new Game(size, mines, seed);

        while (!game.IsGameOver)
        {
            PrintBoard(game.Board);

            Console.Write("Enter move (r c or f r c): ");
            var input = Console.ReadLine()?.Split(' ');

            if (input == null) continue;

            try
            {
                if (input[0] == "f")
                {
                    int r = int.Parse(input[1]);
                    int c = int.Parse(input[2]);
                    game.ToggleFlag(r, c);
                }
                else
                {
                    int r = int.Parse(input[0]);
                    int c = int.Parse(input[1]);
                    game.Reveal(r, c);
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        PrintBoard(game.Board);

        if (game.IsWin)
        {
            Console.WriteLine("You win!");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "Player";

            int time = (int)(game.EndTime.Value - game.StartTime).TotalSeconds;

            manager.SaveScore(new Score
            {
                PlayerName = name,
                Time = time,
                Date = DateTime.Now
            });

            Console.WriteLine("Score saved!");
        }
        else
        {
            Console.WriteLine("Game Over!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ShowScores(HighScoreManager manager)
    {
        Console.Clear();

        var scores = manager.LoadScores();

        Console.WriteLine("=== HIGH SCORES ===");

        foreach (var s in scores)
        {
            Console.WriteLine($"{s.PlayerName} - {s.Time}s - {s.Date}");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void PrintBoard(Board board)
    {
        Console.Write("  ");
        for (int c = 0; c < board.Size; c++)
            Console.Write(c + " ");
        Console.WriteLine();

        for (int r = 0; r < board.Size; r++)
        {
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
}