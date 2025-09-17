namespace Chess
{
    public class Rook : Piece
    {
        public override PieceType PieceType { get; }
        public override int Value { get; }

        public Rook(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.ROOK;
            Value = 5;
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
