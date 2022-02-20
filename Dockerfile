#FROM mcr.microsoft.com/dotnet/aspnet:6.0

#COPY bin/Release/net6.0/publish/ App/
#WORKDIR /App
#ENTRYPOINT ["dotnet", "GenericApi.dll"]




#docker buildx build --platform linux/amd64,linux/arm/v7 -t termisfa/genericapi --push .
#docker run -d -p 8080:80 --name api --mount src=mysqldata,dst=/var/lib/mysql --network mysqlnetwork termisfa/genericapi




#WORKING IN WINDOWS
#FROM mcr.microsoft.com/dotnet/aspnet:latest
#COPY bin/Release/net6.0/publish/ App/
#WORKDIR /App
#ENTRYPOINT ["dotnet", "GenericApi.dll"]






#FROM mcr.microsoft.com/dotnet/aspnet:6.0.1-bullseye-slim-arm32v7
FROM mcr.microsoft.com/dotnet/aspnet:latest
COPY bin/Release/net6.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "GenericApi.dll"]


