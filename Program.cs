using Chess;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Custom below

List<Game> activeGames = new List<Game>();

app.MapPost("/chess-api/start-new-game", () =>
{
    //TODO: Make an option to send in a fen string and start the game in that state
    Game game = new Game();
    var board = new Board();

    string fen = FenHelper.BuildFen(game, board);
    game.FenHistory.Add(fen);
    activeGames.Add(game);

    System.Console.WriteLine(game.Id);

    return fen;
})
.WithName("Start New Game")
.WithOpenApi();

app.MapPost("/chess-api/execute-move", async (HttpContext httpContext) =>
{
    var payload = await httpContext.Request.ReadFromJsonAsync<ExecuteMoveApiPayload>();
    if (payload == null)
    {
        return Results.BadRequest("Invalid request payload.");
    }

    var game = Game.FindActiveGame(activeGames, payload.GameId);
    if (game == null)
    {
        return Results.NotFound("Game is not currently active.");
    }

    bool doesMatch = game.DoesMatchLatestFen(payload.Fen);
    if (!doesMatch)
    {
        return Results.BadRequest("Fen string does not match the last game move.");
    }

    // Check if the move is valid. If it is then update the board and return.

    return Results.Ok(doesMatch);
})
.WithName("Execute Move")
.WithOpenApi();

GetValidMovesApi.EnableEndpoint(app, activeGames);

app.MapPost("/chess-api/build-board", (BuildBoardApiPayload payload) =>
{
    Console.WriteLine(payload);
    var board = new Board(payload.Fen);
    board.Print();
    return board;
})
.WithName("Board")
.WithOpenApi();

app.Run();
