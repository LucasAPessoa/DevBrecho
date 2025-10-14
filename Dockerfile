# ===================================================================
# Estágio 1: Build (A Cozinha - onde preparamos tudo)
# ===================================================================
# Usamos a imagem completa do .NET 8 SDK (Software Development Kit) para compilar o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definimos o diretório de trabalho dentro do container.
WORKDIR /source

# Copia o arquivo do projeto (.csproj) primeiro e restaura as dependências.
# Isso aproveita o cache do Docker. Se as dependências não mudarem, ele não baixa tudo de novo.
COPY ["DevBrecho/DevBrecho.csproj", "DevBrecho/"]
RUN dotnet restore

# Copia todo o resto do código-fonte do seu projeto para o container.
COPY . .

# Publica a aplicação em modo Release, otimizada para produção.
# O resultado será colocado na pasta /app/publish.
RUN dotnet publish "DevBrecho.csproj" -c Release -o /app/publish

# ===================================================================
# Estágio 2: Final (O Prato Final - leve e pronto para servir)
# ===================================================================
# Usamos a imagem ASP.NET Runtime, que é muito menor que o SDK.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Definimos o diretório de trabalho.
WORKDIR /app

# Copia APENAS a pasta com a aplicação publicada do estágio de build para este estágio final.
COPY --from=build /app/publish .

# Expõe a porta 80, que é a porta padrão para HTTP dentro do container.
# O Render vai mapear automaticamente a porta pública para esta porta interna.
EXPOSE 80

# O comando final que será executado quando o container iniciar.
# Ele inicia a sua API.
# IMPORTANTE: Substitua "DevBrecho.dll" pelo nome do seu arquivo .dll principal.
ENTRYPOINT ["dotnet", "DevBrecho.dll"]