FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Copy source code and build / publish app and libraries
WORKDIR /src
COPY ./source/SimpleStoreApp/ ./
RUN ls

RUN dotnet restore
RUN dotnet publish -c release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "SimpleStoreApp.dll"]
