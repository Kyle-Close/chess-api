namespace Chess
{
    // Contains all information about a move
    public class MoveMetaData
    {
        public int StartIndex { get; set; } // The pieces' starting square index
        public int EndIndex { get; set; }   // The pieces' new square index after move is executed

        public bool IsCapture { get; set; }
        public bool IsEnPassantCapture { get; set; }
        public bool IsCastle { get; set; }
        public bool IsPromotion { get; set; }
        public bool CausesCheck { get; set; }

        public string Notation { get; set; }

        public MoveMetaData(Board board, int start, int end, bool isCapture = false, bool isEnPassantCapture = false,
                              bool isCastle = false, bool isPromotion = false, bool causesCheck = false)
        {
            StartIndex = start;
            EndIndex = end;

            IsEnPassantCapture = isEnPassantCapture;
            IsCapture = isCapture;
            IsCastle = isCastle;
            IsPromotion = isPromotion;
            CausesCheck = causesCheck;

            // Set by using BuildMoveNotation a
            Notation = UpdateMoveNotation(board);
        }

        private string UpdateMoveNotation(Board board)
        {
            var piece = board.Squares[StartIndex].Piece;
            if (piece == null)
            {
                throw new Exception("Attempted to build algebraic move notation on square without a piece (" + StartIndex.ToString() + ")");
            }

            // Cover special cases
            if (IsCastle)
            {
                // Determine if king side or queen side
                if (piece.Color == Color.WHITE)
                {
                    // King Side target square = 62
                    if (EndIndex == 62)
                    {
                        return "O-O";
                    }
                    // Queen side target square = 58
                    if (EndIndex == 58)
                    {
                        return "O-O-O";
                    }
                }
                else
                {
                    // King Side target square = 6
                    if (EndIndex == 6)
                    {
                        return "O-O";
                    }
                    // Queen side target square = 2
                    if (EndIndex == 2)
                    {
                        return "O-O-O";
                    }
                }
            }

            StringWriter algebraicNotation = new StringWriter();

            // Get piece letter
            switch (piece.PieceType)
            {
                case PieceType.KNIGHT:
                    algebraicNotation.Write('N');
                    break;
                case PieceType.BISHOP:
                    algebraicNotation.Write('B');
                    break;
                case PieceType.ROOK:
                    algebraicNotation.Write('R');
                    break;
                case PieceType.QUEEN:
                    algebraicNotation.Write('Q');
                    break;
                case PieceType.KING:
                    algebraicNotation.Write('K');
                    break;
            }

            char fileOriginLetter = Square.GetFileLetter(Square.GetFile(StartIndex));

            char fileDestinationLetter = Square.GetFileLetter(Square.GetFile(EndIndex));
            char rankDestinationLetter = Square.GetRankLetter(Square.GetRank(EndIndex));

            // Handle capture
            if (IsCapture)
            {
                // If capturing piece is a pawn we need to append the pawn starting file (exd4)
                if (piece.PieceType == PieceType.PAWN)
                {
                    algebraicNotation.Write(fileOriginLetter.ToString().ToLower());
                }

                algebraicNotation.Write('x');
            }

            // Append destination square
            algebraicNotation.Write(fileDestinationLetter.ToString().ToLower());
            algebraicNotation.Write(rankDestinationLetter);

            // Handle promotion case
            if (IsPromotion)
            {
                algebraicNotation.Write('=');
                algebraicNotation.Write('?'); // At this point we don't know what the user will choose to promote to.
            }

            // Append check if applicable
            if (CausesCheck)
            {
                algebraicNotation.Write('+');
            }

            return algebraicNotation.ToString();
        }
    }
}
