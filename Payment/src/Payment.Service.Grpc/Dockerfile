#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Payment.Service.Grpc/Payment.Service.Grpc.csproj", "Payment.Service.Grpc/"]
RUN dotnet restore "Payment.Service.Grpc/Payment.Service.Grpc.csproj"
COPY . .
WORKDIR "/src/Payment.Service.Grpc"
RUN dotnet build "Payment.Service.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Service.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Service.Grpc.dll"]