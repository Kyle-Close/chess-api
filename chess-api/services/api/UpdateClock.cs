namespace Chess;

public class UpdateClock
{
    public string? GameId { get; set; }

    public UpdateClock(string gameId)
    {
        GameId = gameId;
    }

    public static void EnableEndpoint(WebApplication app)
    {
        app.MapPost("/chess-api/update-clock", async (HttpContext context) =>
        {
            var payload = await context.Request.ReadFromJsonAsync<ExecuteMoveApi>();
            if (payload == null)
            {
                return Results.BadRequest("Invalid request payload.");
            }

            var game = await Mongo.GetActiveGame(payload.GameId);

            if (game == null)
            {
                throw new Exception("Could not find game in active games");
            }

            // We only want to update the time based on who's turn it is.
            // If it's black's turn, then the time should be removed from black's clock
            // If it's white's turn, then the time should be removed from white's clock
            game.UpdateRemainingTime(game.ActiveColor);
            bool isWhiteExpired = false, isBlackExpired = false;

            if (game.WhiteRemainingTime <= 0) isWhiteExpired = true;
            if (game.BlackRemainingTime <= 0) isBlackExpired = true;

            // Update the game state to indicate winner if needed
            if (isWhiteExpired)
            {
                game.EndGame(GameStatus.TIMEOUT, Color.BLACK);
            }
            else if (isBlackExpired)
            {
                game.EndGame(GameStatus.TIMEOUT, Color.WHITE);
            }

            await Mongo.UpdateActiveGame(game);
            return Results.Ok(game); // Return the game with the updated timer and winner if necessary

        })
        .WithName("Checks if either player's clock has expired and updates game state accordingly")
        .WithOpenApi();
    }
}
