using Chess;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://kyle-close.github.io", "http://localhost:5173")
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

Mongo.Ping();

List<Game> activeGames = new List<Game>();

Ping.EnableEndpoint(app);
StartGame.EnableEndpoint(app);
GetGame.EnableEndpoint(app);
ExecuteMoveApi.EnableEndpoint(app);
ResignGame.EnableEndpoint(app, activeGames);
DrawByAgreement.EnableEndpoint(app, activeGames);
UpdateClock.EnableEndpoint(app, activeGames);
StockfishMove.EnableEndpoint(app, activeGames);

app.Run();
