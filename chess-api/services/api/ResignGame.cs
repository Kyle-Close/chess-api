namespace Chess;

class ResignGamePayload
{
    public string? GameId { get; set; }
    public Color ResigningColor { get; set; }
}

public class ResignGame
{
    public static void EnableEndpoint(WebApplication app)
    {
        app.MapPost("/chess-api/resign", async (HttpContext context) =>
        {
            var payload = await context.Request.ReadFromJsonAsync<ResignGamePayload>();
            if (payload == null || payload.GameId == null)
            {
                return Results.BadRequest("Missing payload");
            }

            var game = await Mongo.GetActiveGame(payload.GameId);

            if (game == null)
            {
                throw new Exception("Could not find game in active games");
            }

            game.Resign(payload.ResigningColor);
            await Mongo.UpdateActiveGame(game);

            return Results.Ok(game);
        })
        .WithName("Resign in an active game")
        .WithOpenApi();
    }
}
