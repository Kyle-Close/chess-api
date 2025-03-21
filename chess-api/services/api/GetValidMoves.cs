namespace Chess
{
    public class GetValidMovesResponse
    {
        public Game Game { get; set; }
        public MoveMetaData MoveMetaData { get; set; }
        public string? Message { get; set; }

        public GetValidMovesResponse(Game game, MoveMetaData moveMetaData)
        {
            Game = game;
            MoveMetaData = moveMetaData;
        }

        public GetValidMovesResponse(Game game, MoveMetaData moveMetaData, string message)
        {
            Game = game;
            MoveMetaData = moveMetaData;
            Message = message;
        }
    }

    public class GetValidMovesApi
    {
        public string GameId { get; set; }
        public int Index { get; set; }

        public GetValidMovesApi(string gameId, int index)
        {
            GameId = gameId;
            Index = index;
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

                var piece = game.Board.Squares[payload.Index].Piece;
                if (piece == null)
                {
                    return Results.BadRequest("Selected a square with no piece.");
                }

                // Check if it's the correct players turn.
                if (piece.Color != game.ActiveColor)
                {
                    return Results.BadRequest("Not this colors turn.");
                }

                // Generate possible moves
                MoveMetaData moveMetaData = new MoveMetaData(game, payload.Index);

                return Results.Ok(moveMetaData);
            })
            .WithName("Get Valid Moves")
            .Accepts<GetValidMovesApi>("application/json")
            .Produces<GetValidMovesResponse>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
