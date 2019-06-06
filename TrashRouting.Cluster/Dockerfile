FROM microsoft/dotnet:2.0-sdk AS build
COPY . ./TrashRouting.Api
WORKDIR /TrashRouting.Api/
RUN dotnet build -c Release -o output

FROM microsoft/dotnet:2.0-runtime AS runtime
COPY --from=build /TrashRouting.Api/output .
ENTRYPOINT ["dotnet", "TrashRouting.Api.dll"]