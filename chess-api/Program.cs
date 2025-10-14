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

string? connectionURI = builder.Configuration["Database:Connection"];
if (connectionURI == null)
{
    throw new Exception("Could not load db connection URI. Check that the secret was configured.");
}

Mongo mongo = new Mongo(connectionURI);

List<Game> activeGames = new List<Game>();

StartGame.EnableEndpoint(app, mongo);
GetGame.EnableEndpoint(app, mongo);
ExecuteMoveApi.EnableEndpoint(app, mongo);
ResignGame.EnableEndpoint(app, mongo);
DrawByAgreement.EnableEndpoint(app, mongo);
UpdateClock.EnableEndpoint(app, mongo);
StockfishMove.EnableEndpoint(app, mongo);

app.Run();
