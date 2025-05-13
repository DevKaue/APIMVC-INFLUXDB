# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia o arquivo de projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante dos arquivos e compila a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponha a porta que a aplicação irá rodar
EXPOSE 80

# Define o comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "app.dll"]
