namespace Chess;

class DrawByAgreementPayload
{
    public string GameId { get; set; }
}

public class DrawByAgreement
{
    public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
    {
        app.MapPost("/chess-api/draw-by-agreement", async (HttpContext context) =>
        {
            var payload = await context.Request.ReadFromJsonAsync<DrawByAgreementPayload>();
            var game = activeGames.Find(game => game.Id == payload.GameId);

            if (game == null)
            {
                throw new Exception("Could not find game in active games");
            }

            game.EndGame(GameStatus.DRAW_BY_AGREEMENT, null);
            return game;
        })
        .WithName("Draw by agreement")
        .WithOpenApi();
    }
}
