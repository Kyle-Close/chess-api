namespace Chess
{
    public class Bishop : Piece
    {
        public override PieceType PieceType { get; }

        public Bishop(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.BISHOP;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'B' : 'b';
        }

        // Gets the standard bishop moves that are possible.
        // considers being blocked and ensuring not to go off board.
        public override List<ValidMove> GetStandardMoves(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
