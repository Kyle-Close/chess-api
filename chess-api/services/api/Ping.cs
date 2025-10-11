namespace Chess
{
    public class Ping
    {
        public static void EnableEndpoint(WebApplication app)
        {
            app.MapGet("/chess-api/ping", (HttpContext context) =>
            {
                return Results.Ok();
            })
            .WithName("Ping")
            .WithOpenApi();
        }
    }
}
