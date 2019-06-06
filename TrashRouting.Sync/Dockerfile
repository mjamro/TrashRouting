FROM microsoft/dotnet:2.0-sdk AS build
COPY . ./TrashRouting.Sync
WORKDIR /TrashRouting.Sync/
RUN dotnet build -c Release -o output

FROM microsoft/dotnet:2.0-runtime AS runtime
COPY --from=build /TrashRouting.Sync/output .
ENTRYPOINT ["dotnet", "TrashRouting.Sync.dll"]