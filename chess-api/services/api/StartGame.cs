namespace Chess
{
    public class NewGamePayload
    {
        public string? Fen { get; set; }
        public TimeControl TimeControlType { get; set; }
        public int? StockfishStrength { get; set; }
        public Color? StockfishColor { get; set; }
    }

    public class StartGame
    {
        public static void EnableEndpoint(WebApplication app, Mongo mongo)
        {
            app.MapPost("/chess-api/start-game", async (HttpContext context) =>
            {
                var payload = await context.Request.ReadFromJsonAsync<NewGamePayload>();
                if (payload == null)
                {
                    return Results.BadRequest("Unable to read new game payload from request");
                }

                int stockfishStrength = payload.StockfishStrength ?? -1;
                Color stockfishColor = payload.StockfishColor ?? Color.BLACK;
                TimeControl timeControl = payload.TimeControlType;
                Game game = new Game(timeControl, stockfishStrength, stockfishColor);

                try
                {
                    if (payload != null && !string.IsNullOrWhiteSpace(payload.Fen))
                    {
                        game = new Game(payload.Fen, timeControl, stockfishStrength, stockfishColor);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                string fen = FenHelper.BuildFen(game, game.Board);
                game.FenHistory.Add(fen);

                mongo.CreateActiveGame(game);

                return Results.Ok(game);
            })
            .WithName("Start New Game")
            .WithOpenApi();
        }
    }
}
