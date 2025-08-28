namespace Chess
{
    public class ValidMove
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public bool IsCapture { get; set; }

        public ValidMove(int startIndex, int endIndex, bool isCapture)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            IsCapture = isCapture;
        }
    }

    // Contains all information about an exectued move
    public class MoveMetaData
    {
        public int StartIndex { get; set; }
        public int? EndIndex { get; set; } // If this is sent that indicates a move was executed

        public List<ValidMove> ValidMoves { get; set; } // List of squares the piece can move to. The index

        public bool? IsEnPassantCapture { get; set; }
        public bool? IsCastle { get; set; }
        public bool? IsCapture { get; set; }

        public string? Notation { get; set; } // Also only gets sent when move is executed. "e4" for ex.
        public string? NewFen { get; set; }

        public MoveMetaData(Game game, int start) // Generate a list of moves.
        {
            StartIndex = start;
            var piece = game.Board.Squares[start].Piece;
            if (piece == null)
            {
                throw new Exception("Cannot generate move meta data. No piece on square.");
            }

            // 1. Get the unfiltered list of squares the piece can moved to based purely on how the piece can move.
            List<ValidMove> validMoves = GetStandardMoves(game, start);

            // 2. Add en-passant square if a pawn is attacking it.
            if (game.EnPassantIndex != null && piece is Pawn pawn)
            {
                bool isPawnAttacking = pawn.IsAttackingEnPassantSquare(game.EnPassantIndex.Value, game.Board);

                if (isPawnAttacking)
                {
                    validMoves.Add(new ValidMove(start, game.EnPassantIndex.Value, true));
                    IsEnPassantCapture = true;
                }
            }

            // 3. Add castling moves if possible
            if (piece.PieceType == PieceType.KING)
            {
                if (piece.Color == Color.WHITE)
                {
                    var castleRights = game.WhiteCastleRights;
                    // TODO: make fn that takes castleRights & returns a List<int>. 
                    // TODO:   empty = no castle possible, 1 = either k or q side castle king index, 2 = both k & q castle king index
                }
                // TODO:
            }

            // 4. Filter out any moves that would put the player in check

            ValidMoves = validMoves;
        }

        public bool ExecuteMove(Game game, int endIndex)
        {
            throw new NotImplementedException();
        }

        // Based on the piece being targeted, this fn returns a list of valid indexes
        //  the piece can move to. A standard move is either a move that the piece can go to
        //  (like a bishop moving diagonally) or any attacking moves (pawn diagonal)
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

            return piece.GetStandardMoves(game);
        }
   }

    public class Move
    {
        public string[] GenerateValidMoves(Game game)
        {

            return [""];
        }
    }
}
