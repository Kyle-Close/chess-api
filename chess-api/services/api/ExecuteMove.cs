namespace Chess
{
    public class ExecuteMoveApi
    {
        public string GameId { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public PieceType? PromotionPiece { get; set; }

        public ExecuteMoveApi(string gameId, int start, int end, PieceType? promotionPiece = null)
        {
            GameId = gameId;
            Start = start;
            End = end;
            PromotionPiece = promotionPiece;
        }

        public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
        {
            app.MapPost("/chess-api/execute-move", async (HttpContext httpContext) =>
            {
                var payload = await httpContext.Request.ReadFromJsonAsync<ExecuteMoveApi>();

                if (payload == null)
                {
                    return Results.BadRequest("Invalid request payload.");
                }

                var game = Game.FindActiveGame(activeGames, payload.GameId);

                if (game == null)
                {
                    return Results.NotFound("Game is not currently active.");
                }

                try
                {
                    game.ExecuteMove(payload.Start, payload.End, payload.PromotionPiece);
                    return Results.Ok(game);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e.Message);
                }
            })
            .WithName("Execute Move")
            .Accepts<ExecuteMoveApi>("application/json")
            .Produces<Game>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
