# ===================================================================
# Estágio 1: Build
# ===================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia o arquivo .sln (solução) e os arquivos .csproj (projetos)
# mantendo a estrutura de pastas.
COPY *.sln .
COPY DevBrecho/*.csproj ./DevBrecho/

# Restaura as dependências para toda a solução.
RUN dotnet restore

# Copia todo o resto do código para o container.
COPY . .

# Define o diretório de trabalho para a pasta do projeto antes de publicar.
WORKDIR /source/DevBrecho

# Publica a aplicação em modo Release.
RUN dotnet publish -c Release -o /app/publish


# ===================================================================
# Estágio 2: Final
# ===================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia APENAS a aplicação publicada do estágio de build.
COPY --from=build /app/publish .

# Expõe a porta 80.
EXPOSE 80

# Comando para iniciar a API.
ENTRYPOINT ["dotnet", "DevBrecho.dll"]