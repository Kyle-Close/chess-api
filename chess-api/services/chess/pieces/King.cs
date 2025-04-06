namespace Chess
{
    public class King : Piece
    {
        public override PieceType PieceType { get; }

        public King(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.KING;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'K' : 'k';
        }

        public override List<ValidMove> GetStandardMoves(Game game)
        {
            var scanner = new BoardScanner(game.Board);
            return scanner.EvaluateSurroundingPieceMove(Index, Color);
        }

        public bool IsInStartPosition()
        {
            if (Color == Color.WHITE)
            {
                return Index == 60;
            }
            else
            {
                return Index == 4;
            }
        }

        public bool IsInCheck(Board board)
        {
            return Piece.IsPieceBeingAttacked(board, Index);
        }
    }
}
