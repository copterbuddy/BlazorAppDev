FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY BlazorAppDev/Client/BlazorAppDev.Client.csproj .
RUN dotnet restore BlazorAppDev.Client.csproj
COPY . .
RUN dotnet build BlazorAppDev/Client/BlazorAppDev.Client.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish BlazorAppDev/Client/BlazorAppDev.Client.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf