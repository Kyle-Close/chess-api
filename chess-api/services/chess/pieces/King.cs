namespace Chess
{
    public class King : Piece
    {
        public King(PieceType pieceType, int squareIndex, Color color) : base(pieceType, squareIndex, color)
        {
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'K' : 'k';
        }

        public override List<MoveMetaData> GetStandardMoves(Game game)
        {
            var scanner = new BoardScanner(game.Board);
            return scanner.EvaluateSurroundingPieceMove(game.Board, Index, Color);
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
