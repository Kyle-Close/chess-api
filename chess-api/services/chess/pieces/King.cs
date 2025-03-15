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
            throw new NotImplementedException();
        }
    }
}
