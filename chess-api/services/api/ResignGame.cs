namespace Chess;

class ResignGamePayload
{
    public string GameId { get; set; }
    public Color ResigningColor { get; set; }
}

public class ResignGame
{
    public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
    {
        app.MapPost("/chess-api/resign", async (HttpContext context) =>
        {
            var payload = await context.Request.ReadFromJsonAsync<ResignGamePayload>();

            var game = activeGames.Find(game => game.Id == payload.GameId);

            if (game == null)
            {
                throw new Exception("Could not find game in active games");
            }

            game.Resign(payload.ResigningColor);
            return game;
        })
        .WithName("Resign in an active game")
        .WithOpenApi();
    }
}
