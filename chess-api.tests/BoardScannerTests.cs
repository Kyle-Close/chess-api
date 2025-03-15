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
    }
}
