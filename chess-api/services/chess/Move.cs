namespace Chess
{
    public class ValidMove
    {
        public int Index { get; set; }
        public bool IsCapture { get; set; }

        public ValidMove(int index, bool isCapture)
        {
            Index = index;
            IsCapture = isCapture;
        }
    }

    public class MoveMetaData
    {
        public int StartIndex { get; set; }
        public List<ValidMove> ValidMoves { get; set; } // List of squares the piece can move to. The index

        public bool? IsCapture { get; set; }
        public int? EndIndex { get; set; } // If this is sent that indicates a move was executed
        public string? Notation { get; set; } // Also only gets sent when move is executed. "e4" for ex.

        public MoveMetaData(Game game, int start) // Generate a list of moves.
        {
            StartIndex = start;
            IsCapture = false;

            // 1. Get the unfiltered list of squares the piece can moved to based purely on how the piece can move.
            List<ValidMove> validMoves = GetStandardMoves(game, start);

            // 2. Add en-passant square if a pawn is attacking it.

            // 3. Add castling moves if possible

            // 4. Filter out any moves that would put the player in check

            ValidMoves = validMoves;
        }

        // Based on the piece being targeted, this fn returns a list of valid indexes
        //  the piece can move to. A standard move is either a move that the piece can go to
        //  (like a bishop moving diagonally) or any special attacking moves (pawn diagonal)
        //  This also accounts for if a piece is blocked or off the side of the board.
        public List<ValidMove> GetStandardMoves(Game game, int index)
        {
            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Cannot get standard moves. Index given is off the board.");
            }

            var piece = game.Board.Squares[index].Piece;
            if (piece == null)
            {
                throw new Exception("Cannot get standard moves. Square does not have a piece on it.");
            }

            var result = new List<ValidMove>();

            switch (piece.PieceType)
            {
                case PieceType.PAWN:
                    return piece.GetStandardMoveIndexes(game);
                default:
                    throw new NotImplementedException("");
            }
        }

        /*public MoveMetaData(Game game, int start, int end) // Execute the move.*/
        /*{*/
        /*    StartIndex = start;*/
        /*    EndIndex = end;*/
        /**/
        /*    // 1.*/
        /**/
        /**/
        /*    Notation = ""; // TODO: build the notation string of the move*/
        /*}*/
    }

    public class Move
    {
        public string[] GenerateValidMoves(Game game)
        {

            return [""];
        }
    }
}
