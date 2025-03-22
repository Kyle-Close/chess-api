namespace Chess
{
    public class Queen : Piece
    {
        public override PieceType PieceType { get; }

        public Queen(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.QUEEN;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'Q' : 'q';
        }

        public override List<ValidMove> GetStandardMoves(Game game)
        {
            var result = new List<ValidMove>();

            var scanner = new BoardScanner(game.Board);
            result.AddRange(scanner.EvaluateDiagonalPieceMove(game, Index));
            result.AddRange(scanner.EvaluateSlidingPieceMove(game, Index));

            return result;
        }
    }
}
