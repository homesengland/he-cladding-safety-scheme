#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
RUN apt-get update && \ 
apt-get install -yq --no-install-recommends \ 
libasound2 libatk1.0-0 libc6 libcairo2 libcups2 libdbus-1-3 \ 
libexpat1 libfontconfig1 libgcc1 libgconf-2-4 libgdk-pixbuf2.0-0 libglib2.0-0 libgtk-3-0 libnspr4 \ 
libpango-1.0-0 libpangocairo-1.0-0 libstdc++6 libx11-6 libx11-xcb1 libxcb1 \ 
libxcursor1 libxdamage1 libxext6 libxfixes3 libxi6 libxrandr2 libxrender1 libxss1 libxtst6 \ 
libnss3 libgbm1
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_HTTP_PORTS=80

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src
COPY ["HE.Remediation.WebApp/HE.Remediation.WebApp.csproj", "HE.Remediation.WebApp/"]
RUN dotnet restore "HE.Remediation.WebApp/HE.Remediation.WebApp.csproj"
COPY . .
WORKDIR "/src/HE.Remediation.WebApp"
RUN dotnet build "HE.Remediation.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HE.Remediation.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HE.Remediation.WebApp.dll"]