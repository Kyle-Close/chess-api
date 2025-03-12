using Chess;
namespace chess_api.tests;

public class PawnTests
{
    [Fact]
    public void IsBlocked_WhiteBlocked1AheadEnemy_ExpectBlocked1Ahead()
    {
        var game = new Game();
        var board = new Board("rnbqkbnr/p1pp1ppp/1p2p3/4P3/8/8/PPPP1PPP/RNBQKBNR");
        var res = Pawn.IsBlocked(board, 28);

        Assert.True(res == PawnBlockStatus.BLOCKED_ONE_RANK_AHEAD);
    }
    [Fact]
    public void IsBlocked_BlackBlocked2AheadEnemy_ExpectBlocked2Ahead()
    {
        var game = new Game();
        var board = new Board("rnbqkbnr/p1pppppp/1p6/4P3/8/8/PPPP1PPP/RNBQKBNR");
        var res = Pawn.IsBlocked(board, 12);

        Assert.True(res == PawnBlockStatus.BLOCKED_TWO_RANKS_AHEAD);
    }
    [Fact]
    public void IsBlocked_WhiteBlocked1AheadAlly_ExpectBlocked1Ahead()
    {
        var game = new Game();
        var board = new Board("rnbqkbnr/p1pp1ppp/1p2p3/4P3/8/3B4/PPPP1PPP/RNBQK1NR");
        var res = Pawn.IsBlocked(board, 51);

        Assert.True(res == PawnBlockStatus.BLOCKED_ONE_RANK_AHEAD);
    }
    [Fact]
    public void IsBlocked_WhiteBlocked2AheadAlly_ExpectBlocked2Ahead()
    {
        var game = new Game();
        var board = new Board("rnbqkbnr/p1pp1ppp/1p2p3/4P3/2B5/8/PPPP1PPP/RNBQK1NR");
        var res = Pawn.IsBlocked(board, 50);

        Assert.True(res == PawnBlockStatus.BLOCKED_TWO_RANKS_AHEAD);
    }
    [Fact]
    public void IsBlocked_WhiteUnBlocked2Ahead_ExpectNotBlocked()
    {
        var game = new Game();
        var board = game.Board;
        var res = Pawn.IsBlocked(board, 52);

        Assert.True(res == PawnBlockStatus.NOT_BLOCKED);
    }
    [Fact]
    public void IsBlocked_BlackUnBlocked2Ahead_ExpectNotBlocked()
    {
        var game = new Game();
        var board = game.Board;
        var res = Pawn.IsBlocked(board, 12);

        Assert.True(res == PawnBlockStatus.NOT_BLOCKED);
    }
    [Fact]
    public void IsBlocked_InvalidPosition_ThrowsException()
    {
        var game = new Game();
        var board = game.Board;

        Assert.Throws<Exception>(() => Pawn.IsBlocked(board, -1)); // Example: Invalid index
    }
    [Fact]
    public void IsBlocked_WhitePawnOn2ndLastRank_ExpectNoOutOfBoundsError()
    {
        var game = new Game();
        var board = new Board("rn1qk1nr/p1pb2P1/1p2p2p/3pPp2/1bBP4/2N2P2/PPP3P1/R1BQK1NR");
        var res = Pawn.IsBlocked(board, 14);

        Assert.True(res == PawnBlockStatus.BLOCKED_ONE_RANK_AHEAD);
    }
    [Fact]
    public void IsBlocked_BlackPawnOn2ndLastRank_ExpectNoOutOfBoundsError()
    {
        var game = new Game();
        var board = new Board("rn1qk1nr/p1pb2P1/4p2p/3pPp2/1bBP4/2N2P2/PpP3P1/R1BQK1NR");
        var res = Pawn.IsBlocked(board, 49);

        Assert.True(res == PawnBlockStatus.NOT_BLOCKED);
    }

    // ---- Attacking Tests ----
    //
    [Fact]
    public void GetAttackIndexes_WhiteOnAFile_ExpectOnlyBFileAttack()
    {
        var board = new Board();
        var result = Pawn.GetAttackIndexes(board, 48);

        Assert.True(result.Count == 1 && result.Contains(41));
    }
    [Fact]
    public void GetAttackIndexes_WhiteOnHFile_ExpectOnlyGFileAttack()
    {
        var board = new Board();
        var result = Pawn.GetAttackIndexes(board, 55);

        Assert.True(result.Count == 1 && result.Contains(46));
    }
    [Fact]
    public void GetAttackIndexes_BlackOnAFile_ExpectOnlyBFileAttack()
    {
        var board = new Board();
        var result = Pawn.GetAttackIndexes(board, 8);

        Assert.True(result.Count == 1 && result.Contains(17));
    }
    [Fact]
    public void GetAttackIndexes_BlackOnHFile_ExpectOnlyGFileAttack()
    {
        var board = new Board();
        var result = Pawn.GetAttackIndexes(board, 15);

        Assert.True(result.Count == 1 && result.Contains(22));
    }
    [Fact]
    public void GetAttackIndexes_WhiteInMiddleOfBoard_ExpectBothIndexes()
    {
        var board = new Board();
        var result = Pawn.GetAttackIndexes(board, 52);

        Assert.True(result.Count == 2 && result.Contains(43) && result.Contains(45));
    }
    [Fact]
    public void GetAttackIndexes_BlackInMiddleOfBoard_ExpectBothIndexes()
    {
        var board = new Board();
        var result = Pawn.GetAttackIndexes(board, 11);

        Assert.True(result.Count == 2 && result.Contains(18) && result.Contains(20));
    }
    [Fact]
    public void GetAttackIndexes_PiecesOnAttackSquares_ExpectBothIndexes()
    {
        var board = new Board("rnbqkbnr/ppp3pp/8/3ppp2/3PPP2/8/PPP3PP/RNBQKBNR");
        var result = Pawn.GetAttackIndexes(board, 36);

        Assert.True(result.Count == 2 && result.Contains(27) && result.Contains(29));
    }

    // ---- Get Standard Pawn Moves ----
    //
    [Fact]
    public void GetStandardMoveIndexes_WhiteNoAttackSquares_ExpectStartingIndexes()
    {
        var game = new Game();
        var res = Pawn.GetStandardMoveIndexes(game, 52);

        Assert.Contains(res, move => move.Index == 36 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 44 && move.IsCapture == false);
        Assert.True(res.Count == 2);
    }
    [Fact]
    public void GetStandardMoveIndexes_BlackNoAttackSquares_ExpectStartingIndexes()
    {
        var game = new Game();
        var res = Pawn.GetStandardMoveIndexes(game, 12);

        Assert.Contains(res, move => move.Index == 20 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 28 && move.IsCapture == false);
        Assert.True(res.Count == 2);
    }
    [Fact]
    public void GetStandardMoveIndexes_WhiteOneAttackerAndBlocked_ExpectSingleCapture()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/ppp3pp/3p4/4pp2/4P3/8/PPPP1PPP/RNBQKBNR");
        var res = Pawn.GetStandardMoveIndexes(game, 36);

        Assert.Contains(res, move => move.Index == 29 && move.IsCapture == true);
        Assert.True(res.Count == 1);
    }
    [Fact]
    public void GetStandardMoveIndexes_WhiteTwoAttackersAndBlocked_ExpectDoubleCapture()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/ppp3pp/8/3ppp2/4P3/8/PPPP1PPP/RNBQKBNR");
        var res = Pawn.GetStandardMoveIndexes(game, 36);

        Assert.Contains(res, move => move.Index == 27 && move.IsCapture == true);
        Assert.Contains(res, move => move.Index == 29 && move.IsCapture == true);
        Assert.True(res.Count == 2);
    }
    [Fact]
    public void GetStandardMoveIndexes_WhiteTwoAttackers_ExpectDoubleCaptureAndPush()
    {
        var game = new Game();
        game.Board = new Board("rnbqkbnr/ppp1p1pp/8/3p1p2/4P3/8/PPPP1PPP/RNBQKBNR");
        var piece = game.Board.Squares[36].Piece;
        if (piece is null)
        {
            throw new Exception("No piece found at the expected position (36).");
        }
        piece.HasMoved = true;
        var res = Pawn.GetStandardMoveIndexes(game, 36);

        Assert.Contains(res, move => move.Index == 27 && move.IsCapture == true);
        Assert.Contains(res, move => move.Index == 28 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 29 && move.IsCapture == true);
        Assert.True(res.Count == 3);
    }
    [Fact]
    public void GetStandardMoveIndexes_BlackTwoAttackers_ExpectDoubleCaptureAndPush()
    {
        var game = new Game();
        game.ActiveColor = Color.BLACK;
        game.Board = new Board("rnbqkbnr/pppp1ppp/8/4p3/3P1P2/8/PPP1P1PP/RNBQKBNR");
        var piece = game.Board.Squares[28].Piece;
        if (piece is null)
        {
            throw new Exception("No piece found at the expected position (28).");
        }
        piece.HasMoved = true;
        var res = Pawn.GetStandardMoveIndexes(game, 28);

        Assert.Contains(res, move => move.Index == 35 && move.IsCapture == true);
        Assert.Contains(res, move => move.Index == 36 && move.IsCapture == false);
        Assert.Contains(res, move => move.Index == 37 && move.IsCapture == true);
        Assert.True(res.Count == 3);
    }
    [Fact]
    public void GetStandardMoveIndexes_BlackTwoAttackersAndBlocked_ExpectDoubleCapture()
    {
        var game = new Game();
        game.ActiveColor = Color.BLACK;
        game.Board = new Board("rnbqkbnr/pppp1ppp/8/4p3/3PPP2/8/PPP3PP/RNBQKBNR");
        var res = Pawn.GetStandardMoveIndexes(game, 28);

        Assert.Contains(res, move => move.Index == 35 && move.IsCapture == true);
        Assert.Contains(res, move => move.Index == 37 && move.IsCapture == true);
        Assert.True(res.Count == 2);
    }
    [Fact]
    public void GetStandardMoveIndexes_BlackOneAttackerAndBlocked_ExpectCapture()
    {
        var game = new Game();
        game.ActiveColor = Color.BLACK;
        game.Board = new Board("rnbqkbnr/pppp1ppp/8/4p3/3PP3/8/PPP2PPP/RNBQKBNR");
        var res = Pawn.GetStandardMoveIndexes(game, 28);

        Assert.Contains(res, move => move.Index == 35 && move.IsCapture == true);
        Assert.True(res.Count == 1);
    }
}
