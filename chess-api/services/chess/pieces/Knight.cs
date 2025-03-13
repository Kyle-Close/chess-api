namespace Chess
{
    public class Knight : Piece
    {
        public override PieceType PieceType { get; }

        public Knight(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.KNIGHT;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'N' : 'n';
        }

        // Gets the standard knight moves that are possible.
        // considers being blocked and ensuring not to go off board.
        public override List<ValidMove> GetStandardMoveIndexes(Game game)
        {
            throw new NotImplementedException();
        }

        // Does not consider if the square is already occupied.
        // It will get only valid moves. Cross-board moves are considered and prevented.
        public List<int> GetUnfilteredMoveIndexes(int index)
        {
            throw new NotImplementedException();
        }
    }
}
