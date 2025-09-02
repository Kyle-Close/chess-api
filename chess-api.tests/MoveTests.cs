namespace Chess;

public class MoveTests
{
    #region BuildMoveNotation Tests
    #region Basic Piece Moves

    [Fact]
    public void BuildMoveNotation_Knight_BasicMoves()
    {
        var game = new Game();
        var moves = game.Board.Squares[57].Piece?.ValidMoves; // White knight on b1

        var m1 = moves.Where(m => m.EndIndex == 40).FirstOrDefault(); // N -> a3
        var m2 = moves.Where(m => m.EndIndex == 42).FirstOrDefault(); // N -> c3

        Assert.Equal("Na3", m1.Notation);
        Assert.Equal("Nc3", m2.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Bishop_BasicMoves()
    {
        var game = new Game("rn1qk1nr/ppp2pp1/3p2bp/2b1p3/1P1PP3/P1N2N2/2P2PPP/R1BQKB1R b KQkq - 0 1");
        var moves = game.Board.Squares[22].Piece?.ValidMoves; // Black bishop on g6

        var m1 = moves.Where(m => m.EndIndex == 15).FirstOrDefault(); // B -> h7
        var m2 = moves.Where(m => m.EndIndex == 31).FirstOrDefault(); // B -> h5
        var m3 = moves.Where(m => m.EndIndex == 29).FirstOrDefault(); // B -> f5
        var m4 = moves.Where(m => m.EndIndex == 36).FirstOrDefault(); // B -> e4

        Assert.Equal("Bh7", m1.Notation);
        Assert.Equal("Bh5", m2.Notation);
        Assert.Equal("Bf5", m3.Notation);
        Assert.Equal("Bxe4", m4.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Rook_BasicMoves()
    {
        var game = new Game("r3k2r/pppn2p1/3q1n1p/1Bb1NbB1/1P6/P1N5/2P2PPP/R2QR1K1 w kq - 0 1");
        var moves = game.Board.Squares[19].Piece?.ValidMoves; // Black queen on d6

        var m1 = moves.Where(m => m.EndIndex == 61).FirstOrDefault(); // Q -> d1
        var m2 = moves.Where(m => m.EndIndex == 36).FirstOrDefault(); // Q -> d2


        Assert.Equal("Qxd1", m1.Notation);
        Assert.Equal("Qd2", m2.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Queen_BasicMoves()
    {
        var game = new Game("r3k2r/pppn2p1/3q1n1p/1Bb1NbB1/1P6/P1N5/2P2PPP/R2QR1K1 w kq - 0 1");
        var moves = game.Board.Squares[60].Piece?.ValidMoves; // White rook on e1

        var m1 = moves.Where(m => m.EndIndex == 61).FirstOrDefault(); // R -> f1
        var m2 = moves.Where(m => m.EndIndex == 36).FirstOrDefault(); // R -> e4


        Assert.Equal("Rf1", m1.Notation);
        Assert.Equal("Re4", m2.Notation);
    }

    [Fact]
    public void BuildMoveNotation_King_BasicMoves()
    {
        var game = new Game("r3k2r/pppn2p1/3q1n1p/1Bb1NbB1/1P6/P1N5/2P2PPP/R2QR1K1 w kq - 0 1");
        var moves = game.Board.Squares[4].Piece?.ValidMoves; // Black queen on e4

        var m1 = moves.Where(m => m.EndIndex == 3).FirstOrDefault(); // K -> d8

        Assert.Equal("Kd3", m1.Notation);
    }
    #endregion

    #region Pawn Moves

    [Fact]
    public void BuildMoveNotation_Pawn_BasicMoves()
    {
        var game = new Game();
        var moves = game.Board.Squares[52].Piece?.ValidMoves;

        var m1 = moves.Where(m => m.EndIndex == 44).FirstOrDefault();
        var m2 = moves.Where(m => m.EndIndex == 36).FirstOrDefault();

        Assert.Equal("e3", m1.Notation);
        Assert.Equal("e4", m2.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Pawn_Capture()
    {
        var game = new Game("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
        var moves = game.Board.Squares[36].Piece?.ValidMoves;

        var m = moves.Where(m => m.EndIndex == 27).FirstOrDefault();

        Assert.Equal("exd5", m.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Pawn_EnPassant()
    {
        var game = new Game("rnbqkbnr/pp2pppp/8/2ppP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 1");

        var moves = game.Board.Squares[28].Piece?.ValidMoves; // White pawn on e5
        var m = moves.Where(m => m.EndIndex == 19).FirstOrDefault();

        Assert.Equal("exd6", m.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Pawn_Promotion()
    {
        var game = new Game("8/4P3/1k6/8/8/8/8/4K3 w - - 0 1");

        var moves = game.Board.Squares[12].Piece?.ValidMoves; // Pawn on e7
        var m = moves.Where(m => m.EndIndex == 4).FirstOrDefault();

        Assert.Equal("e8=?", m.Notation); // Promotion notation not finalized - this is a placeholder, the "?" should be replaced with the chosen piece
    }

    [Fact]
    public void BuildMoveNotation_Pawn_PromotionCapture()
    {
        var game = new Game("5n2/1k2P3/8/8/8/8/5r2/7K w - - 0 1");

        var moves = game.Board.Squares[12].Piece?.ValidMoves; // Pawn on e7
        var m = moves.Where(m => m.EndIndex == 5).FirstOrDefault();

        Assert.Equal("exf8=?", m.Notation);
    }
    #endregion

    #region castling
    [Fact]
    public void BuildMoveNotation_White_Castle()
    {
        var game = new Game("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1");

        var moves = game.Board.Squares[60].Piece?.ValidMoves; // White king on e1

        var m1 = moves.Where(m => m.EndIndex == 62).FirstOrDefault();
        var m2 = moves.Where(m => m.EndIndex == 58).FirstOrDefault();

        Assert.Equal("O-O", m1.Notation);
        Assert.Equal("O-O-O", m2.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Black_Castle()
    {
        var game = new Game("r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 1");

        var moves = game.Board.Squares[4].Piece?.ValidMoves; // Black king on e8

        var m1 = moves.Where(m => m.EndIndex == 7).FirstOrDefault();
        var m2 = moves.Where(m => m.EndIndex == 2).FirstOrDefault();

        Assert.Equal("O-O", m1.Notation);
        Assert.Equal("O-O-O", m2.Notation);
    }
    #endregion

    #region Check and Checkmate
    [Fact]
    public void BuildMoveNotation_Bishop_Check()
    {
        var game = new Game("4k3/8/8/8/8/8/8/3B3K w - - 0 1");

        var moves = game.Board.Squares[59].Piece?.ValidMoves; // White bishop on d1
        var m = moves.Where(m => m.EndIndex == 32).FirstOrDefault(); // Move to a4

        Assert.Equal("Ba4+", m.Notation);
    }

    [Fact]
    public void BuildMoveNotation_Queen_Checkmate()
    {
        var game = new Game("rnbqkbnr/ppppp2p/8/5pp1/4P3/2N5/PPPP1PPP/R1BQKBNR w KQkq - 0 1"); // Fool’s mate setup: Qh5#

        var moves = game.Board.Squares[60].Piece?.ValidMoves; // White queen on d1
        var m = moves.Where(m => m.EndIndex == 31).FirstOrDefault(); // Q -> h5

        Assert.Equal("Qh5#", m.Notation);
    }
    
    [Fact]
    public void BuildMoveNotation_KnightDiscoveryCheck()
    {
        var game = new Game("r3k2r/pppn2p1/3q1n1p/1Bb1NbB1/1P6/P1N5/2P2PPP/R2QR1K1 w kq - 0 1");
        var moves = game.Board.Squares[28].Piece?.ValidMoves; // White knight on e5

        var m1 = moves.Where(m => m.EndIndex == 11).FirstOrDefault(); // N -> d7
        var m2 = moves.Where(m => m.EndIndex == 13).FirstOrDefault(); // N -> f7
        var m3 = moves.Where(m => m.EndIndex == 34).FirstOrDefault(); // N -> c4

        Assert.Equal("Nxd7+", m1.Notation);
        Assert.Equal("Nf7+", m2.Notation);
        Assert.Equal("Nc4+", m3.Notation);
    }
    #endregion

    #endregion
}