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
        public BoardRank Rank { get; set; }
        public BoardFile File { get; set; }

        public GetValidMovesApi(string gameId, BoardRank rank, BoardFile file)
        {
            GameId = gameId;
            Rank = rank;
            File = file;
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

                // Generate a list of valid moves in chess notation based on board and turn
                var targetSquareIndex = Square.GetSquareIndex(payload.File, payload.Rank);
                var piece = game.Board.Squares[targetSquareIndex].Piece;
                if (piece == null)
                {
                    return Results.BadRequest();
                }

                // Check if it's the correct players turn.
                if (piece.Color != game.ActiveColor)
                {
                    //GetValidMovesResponse response = new GetValidMovesResponse(game, [], "Whoops! Looks like it's not your turn.");
                    return Results.BadRequest();
                }

                // Generate possible moves
                MoveMetaData moveMetaData = new MoveMetaData(game, targetSquareIndex);
                //GetValidMovesResponse res = new GetValidMovesResponse(game, [moveResponse]);

                return Results.Ok();
            })
            .WithName("Get Valid Moves")
            .Accepts<GetValidMovesApi>("application/json")
            .Produces<GetValidMovesResponse>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
