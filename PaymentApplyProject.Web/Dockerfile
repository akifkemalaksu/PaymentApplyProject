#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV TZ=Europe/Istanbul
ENV LANG tr_TR.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaymentApplyProject.Web/PaymentApplyProject.Web.csproj", "PaymentApplyProject.Web/"]
COPY ["PaymentApplyProject.Infrastructure/PaymentApplyProject.Infrastructure.csproj", "PaymentApplyProject.Infrastructure/"]
COPY ["PaymentApplyProject.Application/PaymentApplyProject.Application.csproj", "PaymentApplyProject.Application/"]
COPY ["PaymentApplyProject.Domain/PaymentApplyProject.Domain.csproj", "PaymentApplyProject.Domain/"]
COPY ["PaymentApplyProject.Persistence/PaymentApplyProject.Persistence.csproj", "PaymentApplyProject.Persistence/"]
RUN dotnet restore "PaymentApplyProject.Web/PaymentApplyProject.Web.csproj"
COPY . .
WORKDIR "/src/PaymentApplyProject.Web"
RUN dotnet build "PaymentApplyProject.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentApplyProject.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentApplyProject.Web.dll"]