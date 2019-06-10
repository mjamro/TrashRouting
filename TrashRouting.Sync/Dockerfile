#FROM microsoft/dotnet:2.2-sdk AS build
#COPY . ./TrashRouting.Sync
#WORKDIR /TrashRouting.Sync/
#RUN dotnet build -c Release -o output
#
#FROM microsoft/dotnet:2.2-runtime AS runtime
#COPY --from=build /TrashRouting.Sync/output .
#ENTRYPOINT ["dotnet", "TrashRouting.Sync.dll"]

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
ENTRYPOINT ["dotnet", "TrashRouting.Sync.dll"]