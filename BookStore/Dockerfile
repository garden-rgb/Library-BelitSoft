#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BookStore/BookLibrary.Web.csproj", "BookStore/"]
COPY ["BookstoreBLL/BookLibrary.BLL.csproj", "BookstoreBLL/"]
COPY ["BookstoreDAL/BookLibrary.DAL.csproj", "BookstoreDAL/"]
RUN dotnet restore "BookStore/BookLibrary.Web.csproj"
COPY . .
WORKDIR "/src/BookStore"
RUN dotnet build "BookLibrary.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookLibrary.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLibrary.Web.dll"]