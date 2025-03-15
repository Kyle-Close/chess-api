namespace Chess
{
    public class Rook : Piece
    {
        public override PieceType PieceType { get; }

        public Rook(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.ROOK;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'R' : 'r';
        }

        public override List<ValidMove> GetStandardMoves(Game game)
        {
            var scanner = new BoardScanner(game.Board);
            return scanner.EvaluateSlidingPieceMove(game, PosIndex);
        }
    }
}
