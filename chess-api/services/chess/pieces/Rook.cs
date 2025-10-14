namespace Chess
{
    public class Rook : Piece
    {
        public Rook(PieceType pieceType, int squareIndex, Color color) : base(pieceType, squareIndex, color)
        {
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'R' : 'r';
        }

        public override List<MoveMetaData> GetStandardMoves(Game game)
        {
            var scanner = new BoardScanner(game.Board);
            return scanner.EvaluateSlidingPieceMove(game.Board, Index, Color);
        }

        public bool IsInStartPosition()
        {
            if (Color == Color.WHITE)
            {
                return Index == 56 || Index == 63;
            }
            else
            {
                return Index == 0 || Index == 7;
            }
        }
    }
}
