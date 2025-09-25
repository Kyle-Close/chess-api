namespace Chess
{
    public class NewGamePayload
    {
        public string? Fen { get; set; }
        public TimeControl TimeControlType { get; set; }
    }

    public class StartGame
    {
        public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
        {
            app.MapPost("/chess-api/start-game", async (HttpContext context) =>
            {
                var payload = await context.Request.ReadFromJsonAsync<NewGamePayload>();

                Game game = new Game(payload.TimeControlType);

                try
                {
                    if (payload != null && !string.IsNullOrWhiteSpace(payload.Fen))
                    {
                        game = new Game(payload.Fen, payload.TimeControlType);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                string fen = FenHelper.BuildFen(game, game.Board);
                game.FenHistory.Add(fen);
                activeGames.Add(game);

                return game;
            })
            .WithName("Start New Game")
            .WithOpenApi();
        }
    }
}
