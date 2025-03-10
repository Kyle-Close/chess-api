namespace Chess
{
    public class ExecuteMoveApiPayload
    {
        public string GameId { get; set; }
        public string Fen { get; set; }
        public string Notation { get; set; }

        public ExecuteMoveApiPayload(string gameId, string fen, string notation)
        {
            GameId = gameId;
            Fen = fen;
            Notation = notation;
        }
    }
}
