# ===================================================================
# Est�gio 1: Build
# ===================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia o arquivo .sln (solu��o) e os arquivos .csproj (projetos)
# mantendo a estrutura de pastas.
COPY *.sln .
COPY DevBrecho/*.csproj ./DevBrecho/

# Restaura as depend�ncias para toda a solu��o.
RUN dotnet restore

# Copia todo o resto do c�digo para o container.
COPY . .

# Define o diret�rio de trabalho para a pasta do projeto antes de publicar.
WORKDIR /source/DevBrecho

# Publica a aplica��o em modo Release.
RUN dotnet publish -c Release -o /app/publish


# ===================================================================
# Est�gio 2: Final
# ===================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia APENAS a aplica��o publicada do est�gio de build.
COPY --from=build /app/publish .

# Exp�e a porta 80.
EXPOSE 80

# Comando para iniciar a API.
ENTRYPOINT ["dotnet", "DevBrecho.dll"]