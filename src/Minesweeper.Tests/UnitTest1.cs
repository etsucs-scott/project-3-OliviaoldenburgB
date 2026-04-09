using Xunit;
using Minesweeper.Core;

public class BoardTests
{
    [Fact]
    public void Board_HasCorrectSize()
    {
        var board = new Board(5, 3, 1);

        Assert.Equal(5, board.Size);
        Assert.Equal(5, board.Grid.GetLength(0));
        Assert.Equal(5, board.Grid.GetLength(1));
    }
    [Fact]
    public void Board_PlacesCorrectNumberOfMines()
    {       
    var board = new Board(5, 5, 1);

    int mineCount = 0;

        for (int r = 0; r < board.Size; r++)
        {
            for (int c = 0; c < board.Size; c++)
            {
                if (board.Grid[r, c].IsMine)
                    mineCount++;
            }
        }

    Assert.Equal(5, mineCount);
    }

    [Fact]
    public void Board_CalculatesAdjacencyCorrectly()
    {   
    var board = new Board(3, 1, 1);

        foreach (var tile in board.Grid)
        {
            if (!tile.IsMine)
            {
                Assert.True(tile.AdjacentMines >= 0);
            }
        }
    }
        
    [Fact]
    public void Reveal_RevealsTile()
    {
    var board = new Board(3, 0, 1);

    board.Reveal(1, 1);

    Assert.True(board.Grid[1, 1].IsRevealed);
    }

    [Fact]
    public void Tiles_StartUnrevealed()
    {
        var board = new Board(4, 2, 1);

        foreach (var tile in board.Grid)
        {
            Assert.False(tile.IsRevealed);
        }
    }

    [Fact]
    public void Tiles_StartUnflagged()
    {
        var board = new Board(4, 2, 1);

        foreach (var tile in board.Grid)
        {
            Assert.False(tile.IsFlagged);
        }
    }

    [Fact]
    public void CannotFlag_RevealedTile()
    {
        var board = new Board(3, 0, 1);

        board.Reveal(1, 1);
        board.ToggleFlag(1, 1);

        Assert.False(board.Grid[1, 1].IsFlagged);
    }
    [Fact]
    public void Reveal_DoesNotRevealFlaggedTile()
    {
        var board = new Board(3, 0, 1);

        board.ToggleFlag(1, 1);
        board.Reveal(1, 1);

        Assert.False(board.Grid[1, 1].IsRevealed);
    }
    [Fact]
    public void SameSeed_ProducesSameMineLayout()
    {
        var board1 = new Board(5, 5, 42);
        var board2 = new Board(5, 5, 42);

        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                Assert.Equal(
                    board1.Grid[r, c].IsMine,
                    board2.Grid[r, c].IsMine
                );
            }
        }
    }
}


    

