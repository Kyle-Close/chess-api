namespace Chess
{
    public class Queen : Piece
    {
        public override PieceType PieceType { get; }
        public override int Value { get; }

        public Queen(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.QUEEN;
            Value = 9;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'Q' : 'q';
        }

        public override List<MoveMetaData> GetStandardMoves(Game game)
        {
            var result = new List<MoveMetaData>();

            var scanner = new BoardScanner(game.Board);
            result.AddRange(scanner.EvaluateDiagonalPieceMove(game.Board, Index, Color));
            result.AddRange(scanner.EvaluateSlidingPieceMove(game.Board, Index, Color));

            return result;
        }
    }
}
