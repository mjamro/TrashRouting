#FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
#COPY . ./TrashRouting.Routes
#WORKDIR /TrashRouting.Routes/
#RUN dotnet build -c Release -o output
#
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS runtime
#COPY --from=build /TrashRouting.Routes/output .
#ENTRYPOINT ["dotnet", "TrashRouting.Routes.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TrashRouting.Routes.dll"]