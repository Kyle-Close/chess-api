namespace Chess
{
    public class Game
    {
        public string Id { get; set; }

        public Color ActiveColor { get; set; }
        public CastleRights WhiteCastleRights { get; set; }
        public CastleRights BlackCastleRights { get; set; }
        public string EnPassantSquare { get; set; }
        public int HalfMoves { get; set; }
        public int FullMoves { get; set; }

        public List<string> FenHistory { get; set; }
        public Board Board { get; set; }

        public Game()
        {
            Id = Guid.NewGuid().ToString();
            ActiveColor = Color.WHITE;
            HalfMoves = 0;
            FullMoves = 1;
            EnPassantSquare = "-";
            WhiteCastleRights = new CastleRights();
            BlackCastleRights = new CastleRights();
            FenHistory = new List<string>();
            Board = new Board();
        }

        public bool DoesMatchLatestFen(string fen)
        {
            var latest = FenHistory.Last();
            if (latest == null)
                return false;

            if (latest == fen)
                return true;

            return false;
        }

        public static Game? FindActiveGame(List<Game> activeGames, string id)
        {
            var game = activeGames.Find(game => game.Id == id);
            return game;
        }

        //TODO: lookup game in active games table
    }
}
