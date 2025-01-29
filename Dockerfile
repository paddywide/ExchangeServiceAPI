# Use official .NET Core SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the project file and restore dependencies
COPY ExchangeServiceAPI.sln ./

COPY API/*.csproj API/
COPY Application/*.csproj Application/
COPY Core/*.csproj Core/
COPY Infrastructure/*.csproj Infrastructure/
COPY ExchangeRate.Persistence/*.csproj ExchangeRate.Persistence/
COPY Identity/*.csproj Identity/
COPY DB/SQLite/*.csproj DB/SQLite/
COPY Application.Test/*.csproj Application.Test/
COPY ExchangeServiceAPI.Application.IntegrationTests/*.csproj ExchangeServiceAPI.Application.IntegrationTests/

RUN dotnet restore ExchangeServiceAPI.sln

# Copy the rest of the application source code
COPY . ./

# Build the application
Run dotnet build -o /app
RUN dotnet publish -c Release -o /publish

# Use a smaller runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /publish /app

# Expose the application's port
EXPOSE 5000
EXPOSE 5001

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ExchangeRate.API.dll"]
