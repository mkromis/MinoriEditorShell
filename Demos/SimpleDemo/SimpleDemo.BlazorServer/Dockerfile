#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Demos/SimpleDemo/SimpleDemo.BlazorServer/SimpleDemo.BlazorServer.csproj", "Demos/SimpleDemo/SimpleDemo.BlazorServer/"]
RUN dotnet restore "Demos/SimpleDemo/SimpleDemo.BlazorServer/SimpleDemo.BlazorServer.csproj"
COPY . .
WORKDIR "/src/Demos/SimpleDemo/SimpleDemo.BlazorServer"
RUN dotnet build "SimpleDemo.BlazorServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleDemo.BlazorServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleDemo.BlazorServer.dll"]