namespace Chess
{
    public class Game
    {
        public string Id { get; set; }

        public Color ActiveColor { get; set; }
        public CastleRights CastleRights { get; set; }
        public string EnPassantSquare { get; set; }
        public int HalfTurns { get; set; }
        public int FullTurns { get; set; }

        public List<string> FenHistory { get; set; }

        public Game()
        {
            Id = Guid.NewGuid().ToString();
            ActiveColor = Color.WHITE;
            HalfTurns = 0;
            FullTurns = 0;
            EnPassantSquare = "-";
            CastleRights = new CastleRights();
            FenHistory = new List<string>();
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
