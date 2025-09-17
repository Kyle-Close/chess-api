namespace Chess
{
    public class IsCheck
    {
        public bool WhiteInCheck { get; set; }
        public bool BlackInCheck { get; set; }

        public IsCheck(bool white, bool black)
        {
            WhiteInCheck = white;
            BlackInCheck = black;
        }
    }

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

        public void MovePiece(int start, int end)
        {
            if (!Board.IsValidSquareIndex(start) || !Board.IsValidSquareIndex(end))
            {
                throw new Exception("Passed invalid board index to MovePiece");
            }

            var piece = Squares[start].Piece;
            if (piece == null)
            {
                throw new Exception("There is no piece on this square.");
            }

            var startSquare = Squares[start];
            var endSquare = Squares[end];

            endSquare.Piece = piece;
            startSquare.Piece = null;

            piece.HasMoved = true;
            piece.Index = end;
        }

        public IsCheck IsCheck()
        {
            var wKing = GetPieces<King>(Color.WHITE);
            var bKing = GetPieces<King>(Color.BLACK);

            if (wKing.Count < 1 || bKing.Count < 1)
            {
                throw new Exception("Could not find one of the kings on the board.");
            }

            var isWhiteCheck = wKing[0].IsInCheck(this);
            var isBlackCheck = bKing[0].IsInCheck(this);

            return new IsCheck(isWhiteCheck, isBlackCheck);
        }

        public bool AreSquaresEmpty(int[] indexes)
        {
            bool allEmpty = true;
            foreach (int index in indexes)
            {
                if (Squares[index].Piece != null)
                {
                    allEmpty = false;
                }
            }
            return allEmpty;
        }

        public static bool IsValidSquareIndex(int index)
        {
            return index >= 0 && index < 64;
        }

        public List<Piece> GetPieces(Color color)
        {
            var result = new List<Piece>();

            if (color == Color.WHITE)
            {
                result = Squares.Where(square => square.Piece != null && square.Piece.Color == Color.WHITE)
                  .Select(square => square.Piece!)
                  .ToList();
            }
            else
            {
                result = Squares.Where(square => square.Piece != null && square.Piece.Color == Color.BLACK)
                  .Select(square => square.Piece!)
                  .ToList();
            }

            return result;
        }

        public List<T> GetPieces<T>(Color color) where T : Piece
        {
            return Squares
                .Where(square => square.Piece is T && square.Piece.Color == color)
                .Select(square => (T)square.Piece!)
                .ToList();
        }

        public List<T> GetPieces<T>() where T : Piece
        {
            return Squares
                .Where(square => square.Piece is T)
                .Select(square => (T)square.Piece!)
                .ToList();
        }

        public static T ValidatePieceOnSquare<T>(Board board, int index) where T : Piece
        {
            if (!IsValidSquareIndex(index))
            {
                throw new Exception("Index provided is not in range.");
            }

            var piece = board.Squares[index].Piece;
            if (piece == null)
            {
                throw new Exception($"No piece on this square.");
            }

            if (piece is not T typedPiece)
            {
                throw new Exception($"Expected to get piece of type {typeof(T).Name} but got {piece.GetType().Name}");
            }

            return typedPiece;
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
                            board[count] = new Square(count, file, rank);
                            count++;
                        }
                    }
                    else
                    {
                        Piece piece = Piece.ConvertCharToPiece(letter, count);
                        Square square = new Square(count, piece, file, rank);

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

        public int TotalPieceValue(Color color)
        {
            // Get all the pieces of target color
            var pieces = GetPieces(color);
            return pieces.Sum(piece => piece.Value);
        }
    }
}
