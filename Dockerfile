# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj first to leverage restore caching
COPY ./chess-api/*.csproj ./chess-api/
RUN dotnet restore ./chess-api/*.csproj

# copy the rest
COPY ./chess-api ./chess-api
WORKDIR /src/chess-api

# publish to a clean folder
RUN dotnet publish -c Release -o /out

# ---------- runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# install stockfish only in runtime image
RUN apt-get update && apt-get install -y --no-install-recommends stockfish \
    && rm -rf /var/lib/apt/lists/*

# copy published app
COPY --from=build /out ./

# env: make the app listen on $PORT (default 8080) and set stockfish path
ENV STOCKFISH_PATH=/usr/games/stockfish \
    ASPNETCORE_URLS=http://0.0.0.0:8080

# many platforms set $PORT; if yours does, your Program.cs should read it and override
# var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
# builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

# run as non-root (built-in user in aspnet image)
USER 64198:64198

EXPOSE 8080

ENTRYPOINT ["dotnet", "chess-api.dll"]
