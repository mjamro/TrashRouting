FROM microsoft/dotnet:2.0-sdk AS build
COPY . ./TrashRouting.Routes
WORKDIR /TrashRouting.Routes/
RUN dotnet build -c Release -o output

FROM microsoft/dotnet:2.0-runtime AS runtime
COPY --from=build /TrashRouting.Routes/output .
ENTRYPOINT ["dotnet", "TrashRouting.Routes.dll"]