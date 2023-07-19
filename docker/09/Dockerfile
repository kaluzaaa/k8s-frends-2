FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY SampleApp/*.sln .
COPY SampleApp/*.csproj .
RUN dotnet restore

COPY SampleApp/. .
RUN dotnet publish -c Release -o out

FROM  mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://*:9090
ENTRYPOINT ["dotnet", "SampleApp.dll"]