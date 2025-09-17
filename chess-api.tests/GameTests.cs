namespace Chess;

public class GameTests
{
    #region IsEnemySquare
    [Fact]
    public void IsEnemySquare_SendInvalidIndex_ExpectException()
    {
        var game = new Game();
        Assert.Throws<Exception>(() => game.IsEnemySquare(-1, Color.WHITE));
    }
    [Fact]
    public void IsEnemySquare_WhiteTurnTargetWhite_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(52, Color.BLACK);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_WhiteTurnTargetEmpty_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(36, Color.BLACK);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_WhiteTurnTargetBlack_ExpectTrue()
    {
        var game = new Game();
        var res = game.IsEnemySquare(12, Color.BLACK);

        Assert.True(res);
    }
    [Fact]
    public void IsEnemySquare_BlackTurnTargetBlack_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(12, Color.WHITE);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_BlackTurnTargetEmpty_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(27, Color.WHITE);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_BlackTurnTargetWhite_ExpectTrue()
    {
        var game = new Game();
        var res = game.IsEnemySquare(52, Color.WHITE);

        Assert.True(res);
    }
    #endregion

    #region ExecuteMove
    // Mostly interesting in the following:
    // - Is the board updated correctly?
    // - Is the game state updated correctly (turn, castling rights, en passant square, halfmove clock, fullmove number)?

    [Fact]
    public void ExecuteMove_EdgeCase_AttemptingMoveOnEmptySquare()
    {
        var game = new Game();
        Assert.Throws<Exception>(() => Move.ExecuteMove(game, 36, 28));
    }

    [Fact]
    public void ExecuteMove_EdgeCase_AttemptingMoveOnNonActivePiece()
    {
        var game = new Game();
        Assert.Throws<Exception>(() => Move.ExecuteMove(game, 12, 28));
    }

    [Fact]
    public void ExecuteMove_EdgeCase_PawnPromotion_WithNoPromotionPieceProvided()
    {
        var game = new Game("rnbqkbnr/ppppppp1/8/8/8/8/PPPP1PPp/RNBQKB2 b Qkq - 0 1");
        var ex = Assert.Throws<Exception>(() => Move.ExecuteMove(game, 55, 63));
        Assert.Equal("Promotion move attempted without specifying promotion piece type.", ex.Message);
    }

    [Fact]
    public void ExecuteMove_EdgeCase_PawnDoubleMove_SetEnPassantSquare()
    {
        var game = new Game();
        Move.ExecuteMove(game, 52, 36); // e2 to e4
        Assert.Equal(44, game.EnPassantIndex); // e3 should be set as en passant square
    }

    [Fact]
    public void ExecuteMove_EdgeCase_PawnDoubleMove_SetEnPassantSquare2()
    {
        var game = new Game("r1bqk1nr/1pp1bpp1/p2p3p/3Pp3/1nP1P3/5N2/PP2QPPP/RNB2RK1 b kq - 0 1");
        Move.ExecuteMove(game, 10, 26); // c7 to c5
        Assert.Equal(18, game.EnPassantIndex); // c6 should be set as en passant square
    }

    [Fact]
    public void ExecuteMove_EdgeCase_ResetEnPassantSquare()
    {
        var game = new Game("r1bqk1nr/1p2bpp1/p2p3p/2pPp3/1nP1P3/5N2/PP2QPPP/RNB2RK1 w kq c6 0 1");
        Move.ExecuteMove(game, 58, 51); // bishop on c1 to d2
        Assert.Null(game.EnPassantIndex);
    }

    [Fact]
    public void ExecuteMove_EdgeCase_EnPassantCapture()
    {
        var game = new Game("rnbqkbnr/ppp2ppp/4p3/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 1");
        Move.ExecuteMove(game, 28, 19); // En passant capture e5 to d6

        var targetSquare = game.Board.Squares[19];
        Assert.NotNull(targetSquare.Piece); // White pawn captured on d6 should be here
        Assert.Equal(PieceType.PAWN, targetSquare.Piece.PieceType);

        Assert.Null(game.Board.Squares[27].Piece); // Pawn on d5 should be captured
        Assert.Null(game.EnPassantIndex); // En passant square should be cleared
    }

    [Fact]
    public void ExecuteMove_EdgeCase_CastleRights_KingMoved()
    {
        var game = new Game("r1bqkb1r/pppp1ppp/2n2n2/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R w KQkq - 0 1");
        Move.ExecuteMove(game, 60, 61); // White king e1 to f1
        Assert.False(game.WhiteCastleRights.QueenSide);
        Assert.False(game.WhiteCastleRights.KingSide);
        Assert.True(game.BlackCastleRights.QueenSide);
        Assert.True(game.BlackCastleRights.KingSide);
    }

    [Fact]
    public void ExecuteMove_EdgeCase_CastleRights_KingSideRookMoved()
    {
        var game = new Game("r1bqkb1r/pppp1ppp/2n2n2/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R w KQkq - 0 1");
        Move.ExecuteMove(game, 63, 62); // White rook h1 to g1
        Assert.True(game.WhiteCastleRights.QueenSide);
        Assert.False(game.WhiteCastleRights.KingSide);
        Assert.True(game.BlackCastleRights.QueenSide);
        Assert.True(game.BlackCastleRights.KingSide);
    }

    [Fact]
    public void ExecuteMove_EdgeCase_CastleRights_QueenSideRookMoved()
    {
        var game = new Game("r2qkb1r/ppp2ppp/2npbn2/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 0 1");
        Move.ExecuteMove(game, 0, 1); // Black rook a8 to b8

        Assert.False(game.BlackCastleRights.QueenSide);
        Assert.True(game.BlackCastleRights.KingSide);
        Assert.True(game.WhiteCastleRights.QueenSide);
        Assert.True(game.WhiteCastleRights.KingSide);
    }

    [Fact]
    public void ExecuteMove_EdgeCase_CastleRights_AttackingKingSidePath()
    {
        var game = new Game("r2qk2r/ppp1b1pp/2np1n2/4pb2/4P3/3B1N2/PPPP1PPP/RNBQK2R w KQkq - 0 1");
        Move.ExecuteMove(game, 43, 34); // White bishop d3 to c4, attacking g8 square
        var bKing = game.Board.GetPieces<King>(Color.BLACK).First();

        // Although g8 is being attacked, the king side castle right should still be available since the king and rook have not moved
        // We do need to make sure that the black king cannot castle in this position though
        Assert.True(bKing.ValidMoves.Any(m => m.EndIndex == 6) == false);

        Assert.True(game.BlackCastleRights.QueenSide);
        Assert.True(game.BlackCastleRights.KingSide);
        Assert.True(game.WhiteCastleRights.QueenSide);
        Assert.True(game.WhiteCastleRights.KingSide);
    }

    [Fact]
    public void ExecuteMove_EdgeCase_CastleRights_CaptureOpponentRook()
    {
        var game = new Game("r2qk2r/1pp1b1pp/2np1n2/4pb2/4P3/3B1N2/1PPP1PPP/RNBQK2R w KQkq - 0 1");
        Move.ExecuteMove(game, 56, 0); // White rook a1 captures black rook a8

        Assert.False(game.BlackCastleRights.QueenSide);
        Assert.True(game.BlackCastleRights.KingSide);
        Assert.False(game.WhiteCastleRights.QueenSide);
        Assert.True(game.WhiteCastleRights.KingSide);
    }

    [Fact]
    public void ExecuteMove_InvalidMove1()
    {
        var game = new Game();
        var ex = Assert.Throws<Exception>(() => Move.ExecuteMove(game, 52, 43));// Pawn on e2 cannot move diagonally to e5
        Assert.Equal("Attempted to execute an invalid move.", ex.Message);
    }

    [Fact]
    public void ExecuteMove_InvalidMove2()
    {
        var game = new Game();
        var ex = Assert.Throws<Exception>(() => Move.ExecuteMove(game, 63, 47)); // Rook on h1 cannot move to h3 because it is blocked by a friendly piece
        Assert.Equal("Attempted to execute an invalid move.", ex.Message);
    }

    [Fact]
    public void ExecuteMove_InvalidMove3()
    {
        var game = new Game();
        var ex = Assert.Throws<Exception>(() => Move.ExecuteMove(game, 60, 52)); // King on e1 cannot move to e2 because it is occupied by a friendly piece
        Assert.Equal("Attempted to execute an invalid move.", ex.Message);
    }

    [Fact]
    public void ExecuteMove_InvalidMove4()
    {
        var game = new Game("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1");
        var ex = Assert.Throws<Exception>(() => Move.ExecuteMove(game, 2, 9)); // Black bishop on c8 cannot move to b7 because it is occupied by a friendly piece
        Assert.Equal("Attempted to execute an invalid move.", ex.Message);
    }

    [Fact]
    public void ExecuteMove_ExpectCorrectMoveCounts1()
    {
        var game = new Game();
        Move.ExecuteMove(game, 52, 36); // e2 to e4
        Move.ExecuteMove(game, 12, 28); // e7 to e5
        Move.ExecuteMove(game, 62, 45); // ng1 to f3
        Move.ExecuteMove(game, 1, 18); // nb8 to c6
        Move.ExecuteMove(game, 59, 52); // qd1 to h5

        Assert.Equal(3, game.FullMoves);
        Assert.Equal(3, game.HalfMoves);
    }

    [Fact]
    public void ExecuteMove_ExpectCorrectMoveCounts2()
    {
        var game = new Game();
        Move.ExecuteMove(game, 52, 36); // e2 to e4
        Move.ExecuteMove(game, 12, 28); // e7 to e5
        Move.ExecuteMove(game, 62, 45); // ng1 to f3
        Move.ExecuteMove(game, 1, 18); // nb8 to c6
        Move.ExecuteMove(game, 59, 52); // qd1 to h5
        Move.ExecuteMove(game, 8, 24); // a7 to a5

        Assert.Equal(4, game.FullMoves);
        Assert.Equal(0, game.HalfMoves);
    }

    [Fact]
    public void ExecuteMove_ExpectStalemate()
    {
        var game = new Game("K7/8/8/8/8/5Q2/8/7k w - - 0 1");
        Move.ExecuteMove(game, 45, 53); // Qf3 to f2
        Assert.True(game.IsStalemate);
    }
    [Fact]
    public void ExecuteMove_ExpectStalemate2()
    {
        var game = new Game("k7/7R/8/7p/5p1P/b4N2/8/R1Q4K w - - 0 1");
        Move.ExecuteMove(game, 58, 57); // Qc1 to b1
        Assert.True(game.IsStalemate);
    }

    [Fact]
    public void ExecuteMove_CorrectMaterialValue()
    {
        var game = new Game("k7/7R/8/7p/5p1P/b4N2/8/R1Q4K w - - 0 1");
        Move.ExecuteMove(game, 58, 57); // Qc1 to b1
        Assert.True(game.WhiteMaterialValue == 23);
        Assert.True(game.BlackMaterialValue == 5);
    }
    #endregion
}
