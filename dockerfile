# STAGE - BUILD
FROM microsoft/dotnet:2.0-sdk as build
WORKDIR /docker
COPY ./src .
RUN dotnet build Housing.Forecast.sln
RUN dotnet publish Housing.Forecast.Service/Housing.Forecast.Service.csproj --output ../www

# STAGE - DEPLOY
FROM microsoft/aspnetcore:2.0 as deploy
WORKDIR /webapi
COPY --from=build /docker/www .
ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 80
CMD [ "dotnet", "Housing.Forecast.Service.dll" ]
