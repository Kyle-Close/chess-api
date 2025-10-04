namespace Chess;

public class StockfishMove
{
    public string GameId { get; set; }
    public int Strength { get; set; }

    public StockfishMove(string gameId, int strength)
    {
        GameId = gameId;
        Strength = strength;
    }

    public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
    {
        app.MapPost("/chess-api/stockfish-move", async (HttpContext httpContext) =>
        {
            var payload = await httpContext.Request.ReadFromJsonAsync<StockfishMove>();

            if (payload == null)
            {
                return Results.BadRequest("Invalid request payload.");
            }

            var game = Game.FindActiveGame(activeGames, payload.GameId);

            if (game == null)
            {
                return Results.NotFound("Game is not currently active.");
            }

            if (!ValidateStrength(payload.Strength))
            {
                return Results.BadRequest("Invalid strength provided.\r\nValid strengths: 0-20, 1320-4000");
            }

            bool isTurn = game.StockfishInfo != null && game.ActiveColor == game.StockfishInfo.PlayingAs;

            if (!isTurn)
            {
                return Results.BadRequest("Not stockfish turn.");
            }

            string fen = FenHelper.BuildFen(game, game.Board);

            var stockfishProcess = new Stockfish();
            string bestMove = await stockfishProcess.ExecuteMoveAsync(payload.Strength, fen);
            stockfishProcess.Dispose();

            // Take the move notation and convert to a move. Run game.ExecuteMove
            var move = MoveNotationHelper.FindMove(bestMove, game);

            if (move == null)
            {
                return Results.BadRequest($"Error finding move provided by engine: {bestMove}");
            }

            Move.ExecuteMove(game, move.StartIndex, move.EndIndex);

            return Results.Ok(game);
        }).WithName("Stockfish execute move")
          .Accepts<StockfishMove>("application/json")
          .Produces<Game>(StatusCodes.Status200OK)
          .Produces<string>(StatusCodes.Status400BadRequest)
          .WithOpenApi();
    }

    private static bool ValidateStrength(int strength)
    {
        // Values between 0-20 will be used to set Skill Level
        if (strength >= 0 && strength <= 20) return true;
        // Values between 1320-4000 will be used to set Elo
        if (strength >= 1320 && strength <= 4000) return true;
        return false;
    }
}
