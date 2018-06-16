# STAGE - BUILD
FROM microsoft/aspnetcore-build:2.0 as build
WORKDIR /docker
COPY src/Housing.Forecast.Context/*.csproj Housing.Forecast.Context/
COPY src/Housing.Forecast.Library/*.csproj Housing.Forecast.Library/
COPY src/Housing.Forecast.Service/*.csproj Housing.Forecast.Service/
RUN dotnet restore *.Service
COPY src ./
RUN dotnet publish *.Service --no-restore -o ../www

# STAGE - DEPLOY
FROM microsoft/aspnetcore:2.0 as deploy
WORKDIR /webapi
COPY --from=build /docker/www ./
ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 80
CMD [ "dotnet", "Housing.Forecast.Service.dll" ]
