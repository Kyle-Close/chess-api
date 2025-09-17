namespace Chess
{
    public class Bishop : Piece
    {
        public override PieceType PieceType { get; }
        public override int Value { get; }

        public Bishop(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.BISHOP;
            Value = 3;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'B' : 'b';
        }

        // Gets the standard bishop moves that are possible.
        // considers being blocked and ensuring not to go off board.
        public override List<MoveMetaData> GetStandardMoves(Game game)
        {
            var scanner = new BoardScanner(game.Board);
            return scanner.EvaluateDiagonalPieceMove(game.Board, Index, Color);
        }
    }
}
