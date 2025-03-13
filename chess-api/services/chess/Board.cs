namespace Chess
{
    public class Board
    {
        // The start of the array represents the top left corner
        //  of the board where the black rook would be
        //  
        // rnbqkbnr
        // pppppppp
        // ...
        // =
        // [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]

        public Square[] Squares { get; set; }

        public Board(Square[] squares)
        {
            Squares = squares;
        }

        public Board(string fen)
        {
            Squares = BuildBoardFromFEN(fen);
        }

        public Board()
        {
            Squares = BuildStartingBoard();
        }

        public static bool IsValidSquareIndex(int index)
        {
            return index >= 0 && index < 64;
        }

        public static Piece ValidatePieceOnSquare(Board board, int index, PieceType expectedPieceType)
        {
            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Index provided is not in range.");
            }

            var piece = board.Squares[index].Piece;
            if (piece == null || piece.PieceType != expectedPieceType)
            {
                var pieceType = piece == null ? "none" : piece.PieceType.ToString();
                throw new Exception($"Expected piece type: {expectedPieceType}. But got {pieceType}");
            }

            return piece;
        }

        public string BuildFen()
        {
            // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR
            string fenString = "";
            for (int i = 0; i < 8; i++)
            {
                int consecutiveEmptySquares = 0;

                for (int j = 0; j < 8; j++)
                {
                    int index = i * 8 + j;
                    Square square = Squares[index];

                    if (square.Piece != null)
                    {
                        if (consecutiveEmptySquares != 0)
                        {
                            fenString += consecutiveEmptySquares;
                            consecutiveEmptySquares = 0;
                        }

                        fenString += square.Piece.GetPieceChar();
                    }
                    else
                    {
                        consecutiveEmptySquares += 1;
                    }
                }
                if (consecutiveEmptySquares != 0) fenString += consecutiveEmptySquares;
                if (i != 7) fenString += '/';
            }

            return fenString;

        }

        private Square[] BuildBoardFromFEN(string fen)
        {
            Square[] board = new Square[64];

            string[] ranks = fen.Split('/');
            if (ranks.Length != 8)
            {
                throw new Exception("Fen string is malformed. Expected 8 segments.");
            }

            int count = 0;
            for (int i = 0; i < ranks.Length; i++)
            {
                for (int j = 0; j < ranks[i].Length; j++)
                {
                    char letter = ranks[i][j];
                    BoardFile file = Square.GetFile(count);
                    BoardRank rank = Square.GetRank(count);
                    int num;

                    // A number indicates the amount of empty consecutive squares
                    if (int.TryParse(letter.ToString(), out num))
                    {
                        for (int k = 0; k < num; k++)
                        {
                            file = Square.GetFile(count);
                            rank = Square.GetRank(count);
                            board[count] = new Square(file, rank);
                            count++;
                        }
                    }
                    else
                    {
                        Piece piece = Piece.ConvertCharToPiece(letter, count);
                        Square square = new Square(piece, file, rank);

                        board[count] = square;
                        count++;
                    }
                }
            }

            return board;
        }

        private Square[] BuildStartingBoard()
        {
            Square[] squares = new Square[64];
            return BuildBoardFromFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        }
    }
}
