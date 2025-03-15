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
        var res = knight.GetUnfilteredMoveIndexes(42);

        Assert.True(res.Count == 8);
        Assert.Contains(res, move => move == 25); // north-west
        Assert.Contains(res, move => move == 27); // north-east
        Assert.Contains(res, move => move == 36);
        Assert.Contains(res, move => move == 52);
        Assert.Contains(res, move => move == 59);
        Assert.Contains(res, move => move == 57);
        Assert.Contains(res, move => move == 48);
        Assert.Contains(res, move => move == 32);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfAFile_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/1N6/PPPPPPPP/R1BQKBNR");
        var knight = new Knight(40, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(40);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 25);
        Assert.Contains(res, move => move == 34);
        Assert.Contains(res, move => move == 50);
        Assert.Contains(res, move => move == 57);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfBFile_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/1N6/PPPPPPPP/R1BQKBNR");
        var knight = new Knight(41, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(41);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move == 24);
        Assert.Contains(res, move => move == 26);
        Assert.Contains(res, move => move == 35);
        Assert.Contains(res, move => move == 51);
        Assert.Contains(res, move => move == 58);
        Assert.Contains(res, move => move == 56);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfHFile_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/7N/PPPPPPPP/RNBQKB1R");
        var knight = new Knight(47, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(47);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 62);
        Assert.Contains(res, move => move == 53);
        Assert.Contains(res, move => move == 37);
        Assert.Contains(res, move => move == 30);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOfGFile_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/6N1/PPPPPPPP/RNBQKB1R");
        var knight = new Knight(46, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(46);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move == 29);
        Assert.Contains(res, move => move == 31);
        Assert.Contains(res, move => move == 63);
        Assert.Contains(res, move => move == 61);
        Assert.Contains(res, move => move == 52);
        Assert.Contains(res, move => move == 36);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf1stRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("r1bqkbnr/pppppppp/8/8/8/6N1/PPPPPPPP/RNBnKBQR");
        var knight = new Knight(59, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(59);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 42);
        Assert.Contains(res, move => move == 44);
        Assert.Contains(res, move => move == 53);
        Assert.Contains(res, move => move == 49);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf2ndRank_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("r1bqkbnr/pppppppp/8/8/8/6N1/PPPnP1PP/RNB1KBQR");
        var knight = new Knight(51, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(51);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move == 34);
        Assert.Contains(res, move => move == 36);
        Assert.Contains(res, move => move == 45);
        Assert.Contains(res, move => move == 61);
        Assert.Contains(res, move => move == 57);
        Assert.Contains(res, move => move == 41);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf7thRank_Expect6Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkb1r/ppppnppp/8/4p3/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(12, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(12);

        Assert.True(res.Count == 6);
        Assert.Contains(res, move => move == 6);
        Assert.Contains(res, move => move == 22);
        Assert.Contains(res, move => move == 29);
        Assert.Contains(res, move => move == 27);
        Assert.Contains(res, move => move == 18);
        Assert.Contains(res, move => move == 2);
    }

    [Fact]
    public void GetUnfilteredMoves_InCenterOf8thRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbnkbqr/pppp1ppp/8/4p3/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(3, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(3);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 13);
        Assert.Contains(res, move => move == 20);
        Assert.Contains(res, move => move == 18);
        Assert.Contains(res, move => move == 9);
    }

    [Fact]
    public void GetUnfilteredMoves_AFileAnd1stRank_Expect2Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/NRBQKBNR");
        var knight = new Knight(56, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(56);

        Assert.True(res.Count == 2);
        Assert.Contains(res, move => move == 41);
        Assert.Contains(res, move => move == 50);
    }

    [Fact]
    public void GetUnfilteredMoves_BFileAnd2ndRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbnkbqr/pppp1ppp/8/4p3/8/1P6/PNPPPPPP/R1BQKBNR");
        var knight = new Knight(49, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(49);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 32);
        Assert.Contains(res, move => move == 34);
        Assert.Contains(res, move => move == 43);
        Assert.Contains(res, move => move == 59);
    }

    [Fact]
    public void GetUnfilteredMoves_HFileAnd1stRank_Expect2Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBRN");
        var knight = new Knight(63, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(63);

        Assert.True(res.Count == 2);
        Assert.Contains(res, move => move == 46);
        Assert.Contains(res, move => move == 53);
    }

    [Fact]
    public void GetUnfilteredMoves_GFileAnd2ndRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/8/8/6P1/PPPPPPNP/RNBQKBR1");
        var knight = new Knight(54, Color.WHITE);
        var res = knight.GetUnfilteredMoveIndexes(54);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 37);
        Assert.Contains(res, move => move == 39);
        Assert.Contains(res, move => move == 60);
        Assert.Contains(res, move => move == 44);
    }

    [Fact]
    public void GetUnfilteredMoves_AFileAnd8thRank_Expect2Moves()
    {
        var game = new Game();
        game.Board = new Board("nrbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(0, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(0);

        Assert.True(res.Count == 2);
        Assert.Contains(res, move => move == 10);
        Assert.Contains(res, move => move == 17);
    }

    [Fact]
    public void GetUnfilteredMoves_BFileAnd7thRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("1rbqkbnr/pnpppppp/1p6/8/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(9, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(9);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 3);
        Assert.Contains(res, move => move == 19);
        Assert.Contains(res, move => move == 26);
        Assert.Contains(res, move => move == 24);
    }

    [Fact]
    public void GetUnfilteredMoves_HFileAnd8thRank_Expect2Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbrn/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(7, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(7);

        Assert.True(res.Count == 2);
        Assert.Contains(res, move => move == 22);
        Assert.Contains(res, move => move == 13);
    }

    [Fact]
    public void GetUnfilteredMoves_GFileAnd7thRank_Expect4Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkb1r/ppppppnp/6p1/8/8/8/PPPPPPPP/RNBQKBNR");
        var knight = new Knight(14, Color.BLACK);
        var res = knight.GetUnfilteredMoveIndexes(14);

        Assert.True(res.Count == 4);
        Assert.Contains(res, move => move == 4);
        Assert.Contains(res, move => move == 31);
        Assert.Contains(res, move => move == 29);
        Assert.Contains(res, move => move == 20);
    }

    [Fact]
    public void GetStandardMoves_WhiteStartingPos_Expect2Moves()
    {
        var game = new Game();
        var knight = new Knight(62, Color.WHITE);
        var res = knight.GetStandardMoves(game);

        Assert.True(res.Count == 2);
        Assert.Contains(res, move => move.Index == 45 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 47 && move.IsCapture == false);
    }

    [Fact]
    public void GetStandardMoves_BlackStartingPos_Expect2Moves()
    {
        var game = new Game();
        game.ActiveColor = Color.BLACK;
        var knight = new Knight(1, Color.BLACK);
        var res = knight.GetStandardMoves(game);

        Assert.True(res.Count == 2);
        Assert.Contains(res, move => move.Index == 16 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 18 && move.IsCapture == false);
    }

    [Fact]
    public void GetStandardMoves_AllSquaresFree_Expect8Moves()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/pppppppp/8/5P2/5NP1/4P3/PPPP3P/RNBQKB1R");
        var knight = new Knight(37, Color.WHITE);
        var res = knight.GetStandardMoves(game);

        Assert.True(res.Count == 8);
        Assert.Contains(res, move => move.Index == 20 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 22 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 31 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 47 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 54 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 52 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 43 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 27 && move.IsCapture == false);
    }


    [Fact]
    public void GetStandardMoves_BlackWithCaptures_ExpectCorrectMoves()
    {
        var game = new Game();
        game.ActiveColor = Color.BLACK;
        game.Board = new Board("rnbqkb1r/pp1ppppp/2p5/4nP2/5NP1/3PP3/PPP4P/RNBQKB1R");
        var knight = new Knight(28, Color.BLACK);
        var res = knight.GetStandardMoves(game);

        Assert.True(res.Count == 5);
        Assert.Contains(res, move => move.Index == 22 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 38 && move.IsCapture == true);
        Assert.Contains(res, move => move.Index == 45 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 43 && move.IsCapture == true);
        Assert.Contains(res, move => move.Index == 34 && move.IsCapture == false);
    }
}
