namespace Chess
{
    public class BoardScannerTests
    {
        [Fact]
        public void GetSquare_BlackRookOnSquare_ExpectBlackRook()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var res = scanner.GetSquare(0);

            Assert.True(res.Piece != null && res.Piece.Color == Color.BLACK && res.Piece.PieceType == PieceType.ROOK);
        }

        [Fact]
        public void GetSquare_WhitePawnOnSquare_ExpectWhitePawn()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var res = scanner.GetSquare(52);

            Assert.True(res.Piece != null && res.Piece.Color == Color.WHITE && res.Piece.PieceType == PieceType.PAWN);
        }

        [Fact]
        public void GetSquare_EmptySquare_ExpectNoPiece()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var res = scanner.GetSquare(32);

            Assert.True(res.Piece == null);
        }

        // ----- Diagonals -------
        //
        // Standard (longer) diagonals

        [Fact]
        public void GetDiagonal_StartingPosA1_BLTTR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(56, Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT);

            Assert.True(diagonal.Count == 8);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece == null);
        }
        [Fact]
        public void GetDiagonal_StartingPosC6_BLTTR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(18, Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT);

            Assert.True(diagonal.Count == 5);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.KING && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece == null);
        }

        [Fact]
        public void GetDiagonal_StartingPosF3_BLTTR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(45, Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT);

            Assert.True(diagonal.Count == 5);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.QUEEN && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece == null);
        }

        [Fact]
        public void GetDiagonal_StartingPosD1_TLTBR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(59, Diagonal.TOP_LEFT_TO_BOTTOM_RIGHT);

            Assert.True(diagonal.Count == 4);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.QUEEN && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece == null);
        }

        [Fact]
        public void GetDiagonal_StartingPosH1_TLTBR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(63, Diagonal.TOP_LEFT_TO_BOTTOM_RIGHT);

            Assert.True(diagonal.Count == 8);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.WHITE);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece == null);
        }

        [Fact]
        public void GetDiagonal_StartingPosH7_TLTBR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(15, Diagonal.TOP_LEFT_TO_BOTTOM_RIGHT);

            Assert.True(diagonal.Count == 2);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.BLACK);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.KNIGHT && square.Piece.Color == Color.BLACK);
        }

        // --- More unique cases ---

        [Fact]
        public void GetDiagonal_StartingPosA1_TLTBR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(56, Diagonal.TOP_LEFT_TO_BOTTOM_RIGHT);

            Assert.True(diagonal.Count == 1);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.WHITE);
        }

        [Fact]
        public void GetDiagonal_StartingPosA8_BLTTP()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(0, Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT);

            Assert.True(diagonal.Count == 1);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.BLACK);
        }

        [Fact]
        public void GetDiagonal_StartingPosH8_TLTBR_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(7, Diagonal.TOP_LEFT_TO_BOTTOM_RIGHT);

            Assert.True(diagonal.Count == 1);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.BLACK);
        }

        [Fact]
        public void GetDiagonal_StartingPosH1_BLTTP_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var diagonal = scanner.GetDiagonal(63, Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT);

            Assert.True(diagonal.Count == 1);
            Assert.Contains(diagonal, square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.WHITE);
        }

        // ----- Scan Files & Ranks -----

        [Fact]
        public void GetFile_StartingPos_ExpectCorrectSquares()
        {
            var board = new Board();
            var scanner = new BoardScanner(board);
            var file = scanner.GetFile(BoardFile.D);

            Assert.True(file.Count == 8);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.QUEEN && square.Piece.Color == Color.WHITE);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.WHITE);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.QUEEN && square.Piece.Color == Color.BLACK);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.BLACK);
            Assert.Contains(file, square => square.Piece == null);
        }

        [Fact]
        public void GetFile_ManyPiecesOnFile_ExpectCorrectSquares()
        {
            var board = new Board("rnb1kbnr/ppppqppp/8/4p3/4P3/3PB3/PPP1BPPP/RN1QK1NR");
            var scanner = new BoardScanner(board);
            var file = scanner.GetFile(BoardFile.E);

            Assert.True(file.Count == 8);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.KING && square.Piece.Color == Color.WHITE);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.BISHOP && square.Piece.Color == Color.WHITE);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.WHITE);

            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.QUEEN && square.Piece.Color == Color.BLACK);
            Assert.Contains(file, square => square.Piece != null && square.Piece.PieceType == PieceType.KING && square.Piece.Color == Color.BLACK);
            Assert.Contains(file, square => square.Piece == null);
        }

        [Fact]
        public void GetFile_EmptyFile_ExpectEmptySquares()
        {
            var board = new Board("rnb1kbn1/pppp1pp1/6p1/4p1q1/4Pr2/3PBRP1/PPP1BPP1/RN1QK1N1");
            var scanner = new BoardScanner(board);
            var file = scanner.GetFile(BoardFile.H);

            Assert.True(file.Count == 8);
            Assert.Equal(8, file.Count(square => square.Piece == null));
        }

        [Fact]
        public void GetRank_StartingPos1stRank_ExpectCorrectSquares()
        {
            var board = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
            var scanner = new BoardScanner(board);
            var rank = scanner.GetRank(BoardRank.ONE);

            Assert.True(rank.Count == 8);
            Assert.Equal(2, rank.Count(square => square.Piece != null && square.Piece.PieceType == PieceType.ROOK && square.Piece.Color == Color.WHITE));
            Assert.Equal(2, rank.Count(square => square.Piece != null && square.Piece.PieceType == PieceType.KNIGHT && square.Piece.Color == Color.WHITE));
            Assert.Equal(2, rank.Count(square => square.Piece != null && square.Piece.PieceType == PieceType.BISHOP && square.Piece.Color == Color.WHITE));
            Assert.Contains(rank, square => square.Piece != null && square.Piece.PieceType == PieceType.QUEEN && square.Piece.Color == Color.WHITE);
            Assert.Contains(rank, square => square.Piece != null && square.Piece.PieceType == PieceType.KING && square.Piece.Color == Color.WHITE);
        }

        [Fact]
        public void GetRank_StartingPos7thRank_ExpectCorrectSquares()
        {
            var board = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
            var scanner = new BoardScanner(board);
            var rank = scanner.GetRank(BoardRank.SEVEN);

            Assert.True(rank.Count == 8);
            Assert.Equal(8, rank.Count(square => square.Piece != null && square.Piece.PieceType == PieceType.PAWN && square.Piece.Color == Color.BLACK));
        }

        [Fact]
        public void GetRank_EmptyRank_ExpectEmptySquares()
        {
            var board = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
            var scanner = new BoardScanner(board);
            var rank = scanner.GetRank(BoardRank.FOUR);

            Assert.True(rank.Count == 8);
            Assert.Equal(8, rank.Count(square => square.Piece == null));
        }

        // ----- Evaluate Diagonal -----
        [Fact]
        public void EvaluateDiagonalPieceMoves_WhiteStartPos_ExpectNoMoves()
        {
            var game = new Game();
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 58);

            Assert.True(res.Count == 0);
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_BlackStartPos_ExpectNoMoves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 5);

            Assert.True(res.Count == 0);
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_OnlySouthEastWithCapture_Expect4Moves()
        {
            var game = new Game();
            game.Board = new Board("rn1qkbnr/pppppppp/b7/8/8/8/PPPPPPPP/RNBQKBNR");
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 16);

            Assert.True(res.Count == 4);
            Assert.Equal(3, res.Count(res => res.IsCapture == false));
            Assert.Equal(1, res.Count(res => res.IsCapture == true));

            Assert.Contains(res, res => res.Index == 25);
            Assert.Contains(res, res => res.Index == 34);
            Assert.Contains(res, res => res.Index == 43);
            Assert.Contains(res, res => res.Index == 52);
        }
        [Fact]
        public void EvaluateDiagonalPieceMoves_OnlyEastWithCaptures_Expect5Moves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("rn1qkbnr/pppppppp/b7/8/8/8/PPPPPPPP/RNBQKBNRrn1qkbnr/ppp1pp1p/b5p1/3p4/4B3/3P2P1/PPP1PP1P/RN1QKBNR");
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 16);

            Assert.True(res.Count == 5);
            Assert.Equal(2, res.Count(res => res.IsCapture == true));
            Assert.Equal(3, res.Count(res => res.IsCapture == false));

            Assert.Contains(res, res => res.Index == 27);
            Assert.Contains(res, res => res.Index == 29);
            Assert.Contains(res, res => res.Index == 22);
            Assert.Contains(res, res => res.Index == 45);
            Assert.Contains(res, res => res.Index == 54);
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_OnlySouthNoCaptures_Expect7Moves()
        {
            var game = new Game();
            game.Board = new Board("rn1q1knr/ppp1bp1p/b3p1p1/3p4/4B3/3P2P1/PPP1PP1P/RN1QKBNR");
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 12);

            Assert.True(res.Count == 7);
            Assert.Equal(7, res.Count(res => res.IsCapture == false));

            Assert.Contains(res, res => res.Index == 19);
            Assert.Contains(res, res => res.Index == 26);
            Assert.Contains(res, res => res.Index == 33);
            Assert.Contains(res, res => res.Index == 40);

            Assert.Contains(res, res => res.Index == 21);
            Assert.Contains(res, res => res.Index == 30);
            Assert.Contains(res, res => res.Index == 39);
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_SurroundedByOpponent_Expect4Captures()
        {
            var game = new Game();
            game.Board = new Board("r2q1knr/pp1pbp1p/2n1b1p1/3B4/2p1p3/6P1/PPPPPP1P/RN1QKBNR");
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 27);

            Assert.True(res.Count == 4);
            Assert.Equal(4, res.Count(res => res.IsCapture == true));

            Assert.Contains(res, res => res.Index == 18);
            Assert.Contains(res, res => res.Index == 20);
            Assert.Contains(res, res => res.Index == 36);
            Assert.Contains(res, res => res.Index == 34);
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_InvalidIndex_ExpectThrow()
        {
            var game = new Game();
            var scanner = new BoardScanner(game.Board);

            Assert.Throws<Exception>(() => scanner.EvaluateDiagonalPieceMove(game, 66));
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_NoPieceOnSquare_ExpectThrow()
        {
            var game = new Game();
            var scanner = new BoardScanner(game.Board);

            Assert.Throws<Exception>(() => scanner.EvaluateDiagonalPieceMove(game, 34));
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_InvalidPiece_ExpectThrow()
        {
            var game = new Game();
            var scanner = new BoardScanner(game.Board);

            Assert.Throws<Exception>(() => scanner.EvaluateDiagonalPieceMove(game, 0));
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_BlackCommonMove_Expect6MovesWithCapture()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("r2q1knr/pp1pbp1p/2n3p1/3B4/2p1p1b1/6P1/PPPPPP1P/RN1QKBNR");
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 38);

            Assert.True(res.Count == 6);
            Assert.Equal(5, res.Count(res => res.IsCapture == false));
            Assert.Equal(1, res.Count(res => res.IsCapture == true));

            Assert.Contains(res, res => res.Index == 20);
            Assert.Contains(res, res => res.Index == 29);
            Assert.Contains(res, res => res.Index == 31);
            Assert.Contains(res, res => res.Index == 47);
            Assert.Contains(res, res => res.Index == 45);
            Assert.Contains(res, res => res.Index == 52);
        }

        [Fact]
        public void EvaluateDiagonalPieceMoves_QueenCommonMove_Expect9MovesWithCaptures()
        {
            var game = new Game();
            game.Board = new Board("r2q1knr/pp1pbp1p/2n3p1/3B4/2pQp1b1/6P1/PPPPPP1P/RN2KBNR");
            var scanner = new BoardScanner(game.Board);
            var res = scanner.EvaluateDiagonalPieceMove(game, 35);

            Assert.True(res.Count == 9);
            Assert.Equal(7, res.Count(res => res.IsCapture == false));
            Assert.Equal(2, res.Count(res => res.IsCapture == true));

            Assert.Contains(res, res => res.Index == 7);
            Assert.Contains(res, res => res.Index == 8);
            Assert.Contains(res, res => res.Index == 14);
            Assert.Contains(res, res => res.Index == 17);
            Assert.Contains(res, res => res.Index == 21);
            Assert.Contains(res, res => res.Index == 26);
            Assert.Contains(res, res => res.Index == 28);
            Assert.Contains(res, res => res.Index == 42);
            Assert.Contains(res, res => res.Index == 44);
        }
    }
}
