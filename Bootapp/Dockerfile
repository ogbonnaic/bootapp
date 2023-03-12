#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AutoAPI/AutoAPI.csproj", "AutoAPI/"]
COPY ["DbUtil/DbUtil.csproj", "DbUtil/"]
COPY ["EmailService/EmailService.csproj", "EmailService/"]
RUN dotnet restore "AutoAPI/AutoAPI.csproj"
COPY . .
WORKDIR "/src/AutoAPI"


RUN dotnet build "AutoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoAPI.dll"]
