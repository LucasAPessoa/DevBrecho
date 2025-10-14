# ===================================================================
# Est�gio 1: Build (A Cozinha - onde preparamos tudo)
# ===================================================================
# Usamos a imagem completa do .NET 8 SDK (Software Development Kit) para compilar o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definimos o diret�rio de trabalho dentro do container.
WORKDIR /source

# Copia o arquivo do projeto (.csproj) primeiro e restaura as depend�ncias.
# Isso aproveita o cache do Docker. Se as depend�ncias n�o mudarem, ele n�o baixa tudo de novo.
COPY ["DevBrecho/DevBrecho.csproj", "DevBrecho/"]
RUN dotnet restore

# Copia todo o resto do c�digo-fonte do seu projeto para o container.
COPY . .

# Publica a aplica��o em modo Release, otimizada para produ��o.
# O resultado ser� colocado na pasta /app/publish.
RUN dotnet publish "DevBrecho.csproj" -c Release -o /app/publish

# ===================================================================
# Est�gio 2: Final (O Prato Final - leve e pronto para servir)
# ===================================================================
# Usamos a imagem ASP.NET Runtime, que � muito menor que o SDK.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Definimos o diret�rio de trabalho.
WORKDIR /app

# Copia APENAS a pasta com a aplica��o publicada do est�gio de build para este est�gio final.
COPY --from=build /app/publish .

# Exp�e a porta 80, que � a porta padr�o para HTTP dentro do container.
# O Render vai mapear automaticamente a porta p�blica para esta porta interna.
EXPOSE 80

# O comando final que ser� executado quando o container iniciar.
# Ele inicia a sua API.
# IMPORTANTE: Substitua "DevBrecho.dll" pelo nome do seu arquivo .dll principal.
ENTRYPOINT ["dotnet", "DevBrecho.dll"]