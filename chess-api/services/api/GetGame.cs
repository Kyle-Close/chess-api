namespace Chess
{
    public class GetGame
    {
        public static void EnableEndpoint(WebApplication app, List<Game> activeGames)
        {
            app.MapGet("/chess-api/game", (HttpContext context) =>
            {
                string gameId = context.Request.Query["gameId"].ToString();
                Console.WriteLine(gameId);

                var game = activeGames.Find(game => game.Id == gameId);

                if (game == null)
                {
                    throw new Exception("Could not find game in active games");
                }

                return game;
            })
            .WithName("Get active game")
            .WithOpenApi();
        }
    }
}
