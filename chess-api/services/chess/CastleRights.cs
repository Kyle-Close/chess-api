namespace Chess
{
    public enum CastlePaths
    {
        BLACK_QUEEN_SIDE,
        BLACK_KING_SIDE,
        WHITE_QUEEN_SIDE,
        WHITE_KING_SIDE
    }

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

        public static bool IsCastlePathClear(Board board, CastlePaths castlePath)
        {
            switch (castlePath)
            {
                case CastlePaths.BLACK_QUEEN_SIDE:
                    return board.AreSquaresEmpty([1, 2, 3]);
                case CastlePaths.BLACK_KING_SIDE:
                    return board.AreSquaresEmpty([5, 6]);
                case CastlePaths.WHITE_QUEEN_SIDE:
                    return board.AreSquaresEmpty([57, 58, 59]);
                case CastlePaths.WHITE_KING_SIDE:
                    return board.AreSquaresEmpty([61, 62]);
                default:
                    throw new Exception("Passed invalid castle path.");
            }
        }

        public static bool IsCastlePathAttacked(Game game, CastlePaths castlePath)
        {
            switch (castlePath)
            {
                case CastlePaths.BLACK_QUEEN_SIDE:
                    return game.Board.AreSquaresAttackedByColor([2, 3], Color.WHITE);
                case CastlePaths.BLACK_KING_SIDE:
                    return game.Board.AreSquaresAttackedByColor([5, 6], Color.WHITE);
                case CastlePaths.WHITE_QUEEN_SIDE:
                    return game.Board.AreSquaresAttackedByColor([58, 59], Color.BLACK);
                case CastlePaths.WHITE_KING_SIDE:
                    return game.Board.AreSquaresAttackedByColor([61, 62], Color.BLACK);
                default:
                    throw new Exception("Passed invalid castle path.");
            }
        }
    }
}
