namespace Chess
{
    public class GetValidMovesApiPayload
    {
        public string GameId { get; set; }
        public string MoveNotation { get; set; }

        public GetValidMovesApiPayload(string gameId, string moveNotation)
        {
            GameId = gameId;
            MoveNotation = moveNotation;
        }
    }
}
