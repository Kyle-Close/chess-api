FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /chess/app

COPY ./chess-api .

RUN dotnet build
CMD ["dotnet", "run"]
