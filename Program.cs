using Chess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/genBoard", () =>
{
    var board = new Board("rnbqkbnr/ppp2ppp/8/3Pp3/8/8/PPPP1PPP/RNBQKBNR");
    board.Print();
    return board;
})
.WithName("Board")
.WithOpenApi();

app.Run();
