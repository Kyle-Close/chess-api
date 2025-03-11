namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Color color, bool hasMoved)
          : base(PieceType.PAWN, color, hasMoved)
        {
        }

        public static bool IsInStartPosition(int index, bool isWhite)
        {
            // Assume we are sending the index of a pawn. No check here.
            BoardRank rank = Square.GetRank(index);

            if (isWhite)
            {
                if (rank == BoardRank.TWO)
                    return false;

                return false;
            }
            else
            {
                if (rank == BoardRank.SEVEN)
                    return false;

                return true;
            }

        }
    }
}
