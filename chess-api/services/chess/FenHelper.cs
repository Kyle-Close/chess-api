namespace Chess
{
    public class FenHelper
    {
        public string BoardSegment { get; set; }
        public string ActiveColorSegment { get; set; }
        public string CastleRightsSegment { get; set; }
        public string EnPassantSegment { get; set; }
        public int? EnPassantIndex { get; set; }
        public string HalfMoveSegment { get; set; }
        public string FullMoveSegment { get; set; }

        public FenHelper(string fen)
        {
            string[] parts = fen.Split(' ');
            if (parts.Length != 6)
            {
                throw new Exception("Error parsing fen string. Expected 6 segments.");
            }

            BoardSegment = parts[0];
            ActiveColorSegment = parts[1];
            CastleRightsSegment = parts[2];
            EnPassantSegment = parts[3];
            HalfMoveSegment = parts[4];
            FullMoveSegment = parts[5];

            // En-passant index
            var index = ConvertEnPassantSegmentToIndex(EnPassantSegment);
            EnPassantIndex = index;
        }

        public static int? ConvertEnPassantSegmentToIndex(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                throw new Exception("Empty en-passant segment provided.");
            }

            if (segment == "-")
            {
                return null;
            }

            return Square.GetSquareIndex(segment[0], segment[1]);
        }

        public static string BuildFen(Game game, Board board)
        {
            string boardSegment = board.BuildFen();
            string turnSegment = game.ActiveColor == Color.WHITE ? "w" : "b";
            string castleRightsSegment = "";

            if (game.WhiteCastleRights.KingSide)
                castleRightsSegment += "K";
            if (game.WhiteCastleRights.QueenSide)
                castleRightsSegment += "Q";
            if (game.BlackCastleRights.KingSide)
                castleRightsSegment += "k";
            if (game.BlackCastleRights.QueenSide)
                castleRightsSegment += "q";

            if (castleRightsSegment == "")
                castleRightsSegment = "-";

            var enPassantSegment = game.EnPassantIndex == null ? "-" : game.EnPassantIndex.ToString();
            string halfMovesSegment = game.HalfMoves.ToString();
            string fullMovesSegment = game.FullMoves.ToString();

            return $"{boardSegment} {turnSegment} {castleRightsSegment} {enPassantSegment} {halfMovesSegment} {fullMovesSegment}";
        }

        public static string GetStartFen()
        {
            return "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        }
    }
}
