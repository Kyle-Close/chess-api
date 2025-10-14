namespace Chess;

class DrawByAgreementPayload
{
    public string? GameId { get; set; }
}

public class DrawByAgreement
{
    public static void EnableEndpoint(WebApplication app)
    {
        app.MapPost("/chess-api/draw-by-agreement", async (HttpContext context) =>
        {
            var payload = await context.Request.ReadFromJsonAsync<DrawByAgreementPayload>();
            if (payload == null || payload.GameId == null)
            {
                return Results.BadRequest("Invalid payload recieved");
            }

            var game = await Mongo.GetActiveGame(payload.GameId);
            if (game == null)
            {
                throw new Exception("Could not find game in active games");
            }

            game.EndGame(GameStatus.DRAW_BY_AGREEMENT, null);
            await Mongo.UpdateActiveGame(game);

            return Results.Ok(game);
        })
        .WithName("Draw by agreement")
        .WithOpenApi();
    }
}
