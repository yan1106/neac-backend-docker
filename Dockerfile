# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /source
EXPOSE 80
ENV ConnectionStrings:NEACSwimmingPoolMangContext "server=45.76.110.87;user=neacswpdevloperqas1;password=0>vv+52_^tY+6R:1~E)~bJ^8x;database=NEACSwimmingPoolMangQas1"
# copy csproj and restore as distinct layers
COPY *.sln .
COPY NEACSwimmingPoolMang.API/*.csproj ./neacdotnetapp/
RUN dotnet restore -r linux-x64

# copy everything else and build app
COPY NEACSwimmingPoolMang.API/. ./neacdotnetapp/
WORKDIR /source/neacdotnetapp
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal-amd64
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./neacdotnetapp"]
