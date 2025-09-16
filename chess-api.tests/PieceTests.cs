namespace Chess;

public class PieceTests
{
    [Fact]
    public void IsBeingAttacked_WhiteNotAttacked()
    {
        var index = 48;
        var game = new Game();

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.False(res);
    }

    [Fact]
    public void IsBeingAttacked_WhiteNotAttacked2()
    {
        var index = 60;
        var game = new Game("rn1qk1nr/ppp3pp/3pbp2/8/1bBQP3/2P2N2/PP3PPP/RNB1K2R w KQkq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.False(res);
    }

    [Fact]
    public void IsBeingAttacked_WhiteAttacked()
    {
        var index = 42;
        var game = new Game("rn1qk1nr/ppp3pp/3pbp2/8/1bBQP3/2P2N2/PP3PPP/RNB1K2R w KQkq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_WhiteAttacked2()
    {
        var index = 60;
        var game = new Game("rn1qk1n1/ppp3pp/3pbp2/8/1bBQP3/2N1rN2/PP3PPP/R1B1K2R w KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_BlackAttacked()
    {
        var index = 19;
        var game = new Game("rn1qk1n1/ppp3pp/3pbp2/8/1bBQP3/2N1rN2/PP3PPP/R1B1K2R b KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_BlackAttacked2()
    {
        var index = 4;
        var game = new Game("rn1qk1n1/ppp3pp/3pbp2/7B/1bBQP3/2N1rN2/PP3PPP/R3K2R b KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_BlackNotAttacked()
    {
        var index = 4;
        var game = new Game("rn1qk1n1/ppp2bpp/3p1p2/7B/1bBQP3/2N1rN2/PP3PPP/R3K2R b KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.False(res);
    }

    [Fact]
    public void IsBeingAttacked_PawnAttck()
    {
        var index = 4;
        var game = new Game("rn1qk1n1/pppP1bpp/3p1p2/7B/1bBQP3/2N1rN2/PP4PP/R3K2R b KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_PawnAttck2()
    {
        var index = 34;
        var game = new Game("rn1qk1n1/p1pP1bpp/3p1p2/1p5B/1bBQP3/2N1rN2/PP4PP/R3K2R w KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_KnightAttack()
    {
        var index = 60;
        var game = new Game("r2qk1n1/ppp3pp/3pbp2/3r4/1bBQP3/2N2n2/PP3PPP/R1B1K2R w KQq - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    [Fact]
    public void IsBeingAttacked_KingAttack()
    {
        var index = 30;
        var game = new Game("r2q2n1/ppp3pp/3pbpk1/3r2P1/1bBQP3/2N2n2/PP3P1P/R1B1K2R w KQ - 0 1");

        var res = Piece.IsPieceBeingAttacked(game.Board, index);
        Assert.True(res);
    }

    // ----- UpdateValidMoves -----
    [Fact]
    public void UpdateValidMoves_MoveWouldPutPlayerInCheck()
    {
        var game = new Game("r5n1/ppp3pp/3pbpk1/3r2P1/1bBqPn2/2N5/PP3P1P/R1B1K2R w KQ - 0 1");

        var piece = game.Board.Squares[60].Piece;
        if (piece == null) throw new Exception("No piece");
        piece.UpdateValidMoves(game, Color.WHITE);

        Assert.False(piece.ValidMoves.Any(piece => piece.EndIndex == 59));
        Assert.False(piece.ValidMoves.Any(piece => piece.EndIndex == 51));
    }
}
