FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Payment.Api/Payment.Api.csproj", "src/Payment.Api/"]
COPY ["src/Payment.Infrastructure/Payment.Infrastructure.csproj", "src/Payment.Infrastructure/"]
COPY ["src/Payment.Application/Payment.Application.csproj", "src/Payment.Application/"]
COPY ["src/Payment.Domain/Payment.Domain.csproj", "src/Payment.Domain/"]
COPY ["src/Payment.Acquiring/Payment.Acquiring.csproj", "src/Payment.Acquiring/"]
#RUN dotnet restore "src/Payment.Api/Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Payment.Api"
RUN dotnet build "Payment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Api.dll"]