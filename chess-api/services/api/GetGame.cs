namespace Chess
{
    public class GetGame
    {
        public static void EnableEndpoint(WebApplication app)
        {
            app.MapGet("/chess-api/game", async (HttpContext context) =>
            {
                string id = context.Request.Query["gameId"].ToString();
                var game = await Mongo.GetActiveGame(id);

                if (game == null)
                {
                    throw new Exception("Could not find game in active games");
                }

                return Results.Ok(game);
            })
            .WithName("Get active game")
            .WithOpenApi();
        }
    }
}
