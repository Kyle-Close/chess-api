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
                        fenString += Piece.ConvertPieceTypeToChar(square.Piece.PieceType, square.Piece.Color == Color.WHITE);
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

        public void Print()
        {
            if (Squares == null) return;

            for (int i = 0; i < Squares.Length; i++)
            {
                var piece = Squares[i].Piece;
                BoardFile file = Square.GetFile(i);

                if (piece == null)
                {
                    System.Console.Write(" ");
                }
                else
                {
                    char letter = Piece.ConvertPieceTypeToChar(piece.PieceType, piece.Color == Color.WHITE);
                    System.Console.Write(letter);
                }

                if (file == BoardFile.H) // indicates we are at the end of the rank
                {
                    System.Console.WriteLine("\n");
                }
            }
        }

        private Square[] BuildStartingBoard()
        {
            Square[] squares = new Square[64];

            // Rank 8
            squares[0] = new Square(new Piece(PieceType.ROOK, Color.BLACK, false), BoardFile.A, BoardRank.EIGHT);
            squares[1] = new Square(new Piece(PieceType.KNIGHT, Color.BLACK, false), BoardFile.B, BoardRank.EIGHT);
            squares[2] = new Square(new Piece(PieceType.BISHOP, Color.BLACK, false), BoardFile.C, BoardRank.EIGHT);
            squares[3] = new Square(new Piece(PieceType.QUEEN, Color.BLACK, false), BoardFile.D, BoardRank.EIGHT);
            squares[4] = new Square(new Piece(PieceType.KING, Color.BLACK, false), BoardFile.E, BoardRank.EIGHT);
            squares[5] = new Square(new Piece(PieceType.BISHOP, Color.BLACK, false), BoardFile.F, BoardRank.EIGHT);
            squares[6] = new Square(new Piece(PieceType.KNIGHT, Color.BLACK, false), BoardFile.G, BoardRank.EIGHT);
            squares[7] = new Square(new Piece(PieceType.ROOK, Color.BLACK, false), BoardFile.H, BoardRank.EIGHT);

            // Rank 7
            squares[8] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.A, BoardRank.SEVEN);
            squares[9] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.B, BoardRank.SEVEN);
            squares[10] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.C, BoardRank.SEVEN);
            squares[11] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.D, BoardRank.SEVEN);
            squares[12] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.E, BoardRank.SEVEN);
            squares[13] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.F, BoardRank.SEVEN);
            squares[14] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.G, BoardRank.SEVEN);
            squares[15] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.H, BoardRank.SEVEN);

            // Ranks 6-3: Empty
            squares[16] = new Square(BoardFile.A, BoardRank.SIX);
            squares[17] = new Square(BoardFile.B, BoardRank.SIX);
            squares[18] = new Square(BoardFile.C, BoardRank.SIX);
            squares[19] = new Square(BoardFile.D, BoardRank.SIX);
            squares[20] = new Square(BoardFile.E, BoardRank.SIX);
            squares[21] = new Square(BoardFile.F, BoardRank.SIX);
            squares[22] = new Square(BoardFile.G, BoardRank.SIX);
            squares[23] = new Square(BoardFile.H, BoardRank.SIX);

            squares[24] = new Square(BoardFile.A, BoardRank.FIVE);
            squares[25] = new Square(BoardFile.B, BoardRank.FIVE);
            squares[26] = new Square(BoardFile.C, BoardRank.FIVE);
            squares[27] = new Square(BoardFile.D, BoardRank.FIVE);
            squares[28] = new Square(BoardFile.E, BoardRank.FIVE);
            squares[29] = new Square(BoardFile.F, BoardRank.FIVE);
            squares[30] = new Square(BoardFile.G, BoardRank.FIVE);
            squares[31] = new Square(BoardFile.H, BoardRank.FIVE);

            squares[32] = new Square(BoardFile.A, BoardRank.FOUR);
            squares[33] = new Square(BoardFile.B, BoardRank.FOUR);
            squares[34] = new Square(BoardFile.C, BoardRank.FOUR);
            squares[35] = new Square(BoardFile.D, BoardRank.FOUR);
            squares[36] = new Square(BoardFile.E, BoardRank.FOUR);
            squares[37] = new Square(BoardFile.F, BoardRank.FOUR);
            squares[38] = new Square(BoardFile.G, BoardRank.FOUR);
            squares[39] = new Square(BoardFile.H, BoardRank.FOUR);

            squares[40] = new Square(BoardFile.A, BoardRank.THREE);
            squares[41] = new Square(BoardFile.B, BoardRank.THREE);
            squares[42] = new Square(BoardFile.C, BoardRank.THREE);
            squares[43] = new Square(BoardFile.D, BoardRank.THREE);
            squares[44] = new Square(BoardFile.E, BoardRank.THREE);
            squares[45] = new Square(BoardFile.F, BoardRank.THREE);
            squares[46] = new Square(BoardFile.G, BoardRank.THREE);
            squares[47] = new Square(BoardFile.H, BoardRank.THREE);

            // Rank 2
            squares[48] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.A, BoardRank.TWO);
            squares[49] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.B, BoardRank.TWO);
            squares[50] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.C, BoardRank.TWO);
            squares[51] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.D, BoardRank.TWO);
            squares[52] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.E, BoardRank.TWO);
            squares[53] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.F, BoardRank.TWO);
            squares[54] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.G, BoardRank.TWO);
            squares[55] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.H, BoardRank.TWO);

            // Rank 1
            squares[56] = new Square(new Piece(PieceType.ROOK, Color.WHITE, false), BoardFile.A, BoardRank.ONE);
            squares[57] = new Square(new Piece(PieceType.KNIGHT, Color.WHITE, false), BoardFile.B, BoardRank.ONE);
            squares[58] = new Square(new Piece(PieceType.BISHOP, Color.WHITE, false), BoardFile.C, BoardRank.ONE);
            squares[59] = new Square(new Piece(PieceType.QUEEN, Color.WHITE, false), BoardFile.D, BoardRank.ONE);
            squares[60] = new Square(new Piece(PieceType.KING, Color.WHITE, false), BoardFile.E, BoardRank.ONE);
            squares[61] = new Square(new Piece(PieceType.BISHOP, Color.WHITE, false), BoardFile.F, BoardRank.ONE);
            squares[62] = new Square(new Piece(PieceType.KNIGHT, Color.WHITE, false), BoardFile.G, BoardRank.ONE);
            squares[63] = new Square(new Piece(PieceType.ROOK, Color.WHITE, false), BoardFile.H, BoardRank.ONE);

            return squares;
        }
    }
}
