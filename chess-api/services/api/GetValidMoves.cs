namespace Chess
{
    public class GetValidMovesResponse
    {
        public List<ValidMove> ValidMoves { get; set; }
        public string? Message { get; set; }

        public GetValidMovesResponse(List<ValidMove> validMoves, string message)
        {
            ValidMoves = validMoves;
            Message = message;
        }

        public GetValidMovesResponse(string message)
        {
            ValidMoves = new List<ValidMove>();
            Message = message;
        }
    }

    public class GetValidMovesApi
    {
        public string GameId { get; set; }

        public GetValidMovesApi(string gameId)
        {
            GameId = gameId;
        }

        public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
        {
            app.MapPost("/chess-api/get-valid-moves", async (HttpContext httpContext) =>
            {
                var payload = await httpContext.Request.ReadFromJsonAsync<GetValidMovesApi>();

                if (payload == null)
                {
                    return Results.BadRequest("Invalid request payload.");
                }

                var game = Game.FindActiveGame(activeGames, payload.GameId);

                if (game == null)
                {
                    return Results.NotFound("Game is not currently active.");
                }

                return Results.Ok(game.GetCurrentValidMoves());
            })
            .WithName("Get Valid Moves")
            .Accepts<GetValidMovesApi>("application/json")
            .Produces<GetValidMovesResponse>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
