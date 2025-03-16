namespace Chess
{
    public class CastleRights
    {
        // The castle rights indicate if the castle rights on each side are available.
        //  Meaning, something that temporarily blocks a castle doesn't make the castle right false.
        //  If the kings moves or a rook moves that will invalidate.

        public bool KingSide { get; set; }
        public bool QueenSide { get; set; }

        public CastleRights()
        {
            KingSide = true;
            QueenSide = true;
        }

        public CastleRights(bool kingSide, bool queenSide)
        {
            KingSide = kingSide;
            QueenSide = queenSide;
        }
    }
}
