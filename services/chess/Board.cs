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
            Squares[0] = new Square(new Piece(PieceType.ROOK, Color.BLACK, false), BoardFile.A, BoardRank.EIGHT);
            Squares[1] = new Square(new Piece(PieceType.KNIGHT, Color.BLACK, false), BoardFile.B, BoardRank.EIGHT);
            Squares[2] = new Square(new Piece(PieceType.BISHOP, Color.BLACK, false), BoardFile.C, BoardRank.EIGHT);
            Squares[3] = new Square(new Piece(PieceType.QUEEN, Color.BLACK, false), BoardFile.D, BoardRank.EIGHT);
            Squares[4] = new Square(new Piece(PieceType.KING, Color.BLACK, false), BoardFile.E, BoardRank.EIGHT);
            Squares[5] = new Square(new Piece(PieceType.BISHOP, Color.BLACK, false), BoardFile.F, BoardRank.EIGHT);
            Squares[6] = new Square(new Piece(PieceType.KNIGHT, Color.BLACK, false), BoardFile.G, BoardRank.EIGHT);
            Squares[7] = new Square(new Piece(PieceType.ROOK, Color.BLACK, false), BoardFile.H, BoardRank.EIGHT);

            // Rank 7
            Squares[8] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.A, BoardRank.SEVEN);
            Squares[9] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.B, BoardRank.SEVEN);
            Squares[10] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.C, BoardRank.SEVEN);
            Squares[11] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.D, BoardRank.SEVEN);
            Squares[12] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.E, BoardRank.SEVEN);
            Squares[13] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.F, BoardRank.SEVEN);
            Squares[14] = new Square(new Piece(PieceType.PAWN, Color.BLACK, false), BoardFile.G, BoardRank.SEVEN);
            Squares[15] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.H, BoardRank.SEVEN);

            // Ranks 6-3: Empty
            Squares[16] = new Square(BoardFile.A, BoardRank.SIX);
            Squares[17] = new Square(BoardFile.B, BoardRank.SIX);
            Squares[18] = new Square(BoardFile.C, BoardRank.SIX);
            Squares[19] = new Square(BoardFile.D, BoardRank.SIX);
            Squares[20] = new Square(BoardFile.E, BoardRank.SIX);
            Squares[21] = new Square(BoardFile.F, BoardRank.SIX);
            Squares[22] = new Square(BoardFile.G, BoardRank.SIX);
            Squares[23] = new Square(BoardFile.H, BoardRank.SIX);

            Squares[24] = new Square(BoardFile.A, BoardRank.FIVE);
            Squares[25] = new Square(BoardFile.B, BoardRank.FIVE);
            Squares[26] = new Square(BoardFile.C, BoardRank.FIVE);
            Squares[27] = new Square(BoardFile.D, BoardRank.FIVE);
            Squares[28] = new Square(BoardFile.E, BoardRank.FIVE);
            Squares[29] = new Square(BoardFile.F, BoardRank.FIVE);
            Squares[30] = new Square(BoardFile.G, BoardRank.FIVE);
            Squares[31] = new Square(BoardFile.H, BoardRank.FIVE);

            Squares[32] = new Square(BoardFile.A, BoardRank.FOUR);
            Squares[33] = new Square(BoardFile.B, BoardRank.FOUR);
            Squares[34] = new Square(BoardFile.C, BoardRank.FOUR);
            Squares[35] = new Square(BoardFile.D, BoardRank.FOUR);
            Squares[36] = new Square(BoardFile.E, BoardRank.FOUR);
            Squares[37] = new Square(BoardFile.F, BoardRank.FOUR);
            Squares[38] = new Square(BoardFile.G, BoardRank.FOUR);
            Squares[39] = new Square(BoardFile.H, BoardRank.FOUR);

            Squares[40] = new Square(BoardFile.A, BoardRank.THREE);
            Squares[41] = new Square(BoardFile.B, BoardRank.THREE);
            Squares[42] = new Square(BoardFile.C, BoardRank.THREE);
            Squares[43] = new Square(BoardFile.D, BoardRank.THREE);
            Squares[44] = new Square(BoardFile.E, BoardRank.THREE);
            Squares[45] = new Square(BoardFile.F, BoardRank.THREE);
            Squares[46] = new Square(BoardFile.G, BoardRank.THREE);
            Squares[47] = new Square(BoardFile.H, BoardRank.THREE);

            // Rank 2
            Squares[48] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.A, BoardRank.TWO);
            Squares[49] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.B, BoardRank.TWO);
            Squares[50] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.C, BoardRank.TWO);
            Squares[51] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.D, BoardRank.TWO);
            Squares[52] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.E, BoardRank.TWO);
            Squares[53] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.F, BoardRank.TWO);
            Squares[54] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.G, BoardRank.TWO);
            Squares[55] = new Square(new Piece(PieceType.PAWN, Color.WHITE, false), BoardFile.H, BoardRank.TWO);

            // Rank 1
            Squares[56] = new Square(new Piece(PieceType.ROOK, Color.WHITE, false), BoardFile.A, BoardRank.ONE);
            Squares[57] = new Square(new Piece(PieceType.KNIGHT, Color.WHITE, false), BoardFile.B, BoardRank.ONE);
            Squares[58] = new Square(new Piece(PieceType.BISHOP, Color.WHITE, false), BoardFile.C, BoardRank.ONE);
            Squares[59] = new Square(new Piece(PieceType.QUEEN, Color.WHITE, false), BoardFile.D, BoardRank.ONE);
            Squares[60] = new Square(new Piece(PieceType.KING, Color.WHITE, false), BoardFile.E, BoardRank.ONE);
            Squares[61] = new Square(new Piece(PieceType.BISHOP, Color.WHITE, false), BoardFile.F, BoardRank.ONE);
            Squares[62] = new Square(new Piece(PieceType.KNIGHT, Color.WHITE, false), BoardFile.G, BoardRank.ONE);
            Squares[63] = new Square(new Piece(PieceType.ROOK, Color.WHITE, false), BoardFile.H, BoardRank.ONE);

            return squares;
        }
    }
}
