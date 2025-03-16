namespace Chess
{
    public class NewGamePayload
    {
        public string? Fen { get; set; }
    }

    public class StartNewGameApi
    {
        public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
        {
            app.MapPost("/chess-api/start-new-game", async (HttpContext context) =>
            {
                Game game = new Game();

                try
                {
                    var payload = await context.Request.ReadFromJsonAsync<NewGamePayload>();
                    if (payload != null && !string.IsNullOrWhiteSpace(payload.Fen))
                    {
                        game = new Game(payload.Fen);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                string fen = FenHelper.BuildFen(game, game.Board);
                game.FenHistory.Add(fen);
                activeGames.Add(game);

                Console.WriteLine(game.Id);

                return fen;
            })
            .WithName("Start New Game")
            .WithOpenApi();
        }
    }
}
