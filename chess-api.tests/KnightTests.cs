using Chess;
namespace chess_api.tests;

public class KnightTests
{
    [Fact]
    public void GetUnfilteredMoves_CenterOfBoard_Expect8Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/2N5/PPPPPPPP/R1BQKBNR");
        var knight = new Knight(42, Color.WHITE);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 8);
        Assert.Contains(res, move => move.Index == 25); // north-west
        Assert.Contains(res, move => move.Index == 27); // north-east
        Assert.Contains(res, move => move.Index == 36);
        Assert.Contains(res, move => move.Index == 52);
        Assert.Contains(res, move => move.Index == 59);
        Assert.Contains(res, move => move.Index == 57);
        Assert.Contains(res, move => move.Index == 48);
        Assert.Contains(res, move => move.Index == 32);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfAFile_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/1N6/PPPPPPPP/R1BQKBNR");
        var knight = new Knight(40, Color.WHITE);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move.Index == 25);
        Assert.Contains(res, move => move.Index == 34);
        Assert.Contains(res, move => move.Index == 50);
        Assert.Contains(res, move => move.Index == 57);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfBFile_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/1N6/PPPPPPPP/R1BQKBNR");
        var knight = new Knight(41, Color.WHITE);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move.Index == 24);
        Assert.Contains(res, move => move.Index == 27);
        Assert.Contains(res, move => move.Index == 35);
        Assert.Contains(res, move => move.Index == 51);
        Assert.Contains(res, move => move.Index == 58);
        Assert.Contains(res, move => move.Index == 56);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfHFile_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/7N/PPPPPPPP/RNBQKB1R");
        var knight = new Knight(47, Color.WHITE);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move.Index == 62);
        Assert.Contains(res, move => move.Index == 53);
        Assert.Contains(res, move => move.Index == 37);
        Assert.Contains(res, move => move.Index == 30);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfGFile_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/6N1/PPPPPPPP/RNBQKB1R");
        var knight = new Knight(46, Color.WHITE);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move.Index == 29);
        Assert.Contains(res, move => move.Index == 31);
        Assert.Contains(res, move => move.Index == 63);
        Assert.Contains(res, move => move.Index == 61);
        Assert.Contains(res, move => move.Index == 52);
        Assert.Contains(res, move => move.Index == 36);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf1stRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("r1bqkbnr/pppppppp/8/8/8/6N1/PPPPPPPP/RNBnKBQR");
        var knight = new Knight(59, Color.BLACK);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move.Index == 42);
        Assert.Contains(res, move => move.Index == 44);
        Assert.Contains(res, move => move.Index == 53);
        Assert.Contains(res, move => move.Index == 49);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf2ndRank_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("r1bqkbnr/pppppppp/8/8/8/6N1/PPPnP1PP/RNB1KBQR");
        var knight = new Knight(51, Color.BLACK);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move.Index == 34);
        Assert.Contains(res, move => move.Index == 36);
        Assert.Contains(res, move => move.Index == 45);
        Assert.Contains(res, move => move.Index == 61);
        Assert.Contains(res, move => move.Index == 57);
        Assert.Contains(res, move => move.Index == 41);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf7thRank_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkb1r/ppppnppp/8/4p3/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(12, Color.BLACK);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move.Index == 6);
        Assert.Contains(res, move => move.Index == 22);
        Assert.Contains(res, move => move.Index == 29);
        Assert.Contains(res, move => move.Index == 27);
        Assert.Contains(res, move => move.Index == 18);
        Assert.Contains(res, move => move.Index == 2);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf8thRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbnkbqr/pppp1ppp/8/4p3/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(3, Color.BLACK);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move.Index == 13);
        Assert.Contains(res, move => move.Index == 20);
        Assert.Contains(res, move => move.Index == 18);
        Assert.Contains(res, move => move.Index == 9);
    }

    [Fact]
    public void GetUnfilteredMoves_BFileAnd2ndRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("");
        var knight = new Knight(3, Color.BLACK);
        var res = knight.GetStandardMoveIndexes(game);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move.Index == 13);
        Assert.Contains(res, move => move.Index == 20);
        Assert.Contains(res, move => move.Index == 18);
        Assert.Contains(res, move => move.Index == 9);
    }
}
