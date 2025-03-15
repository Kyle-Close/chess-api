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
            throw new NotImplementedException();
        }
    }
}
