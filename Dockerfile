
FROM mcr.microsoft.com/dotnet/aspnet:latest
COPY bin/Release/net6.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "GenericApi.dll"]



#-----------------
#START API
#docker buildx build --platform linux/amd64,linux/arm/v7 -t termisfa/genericapi --push .
#docker run -d -p 8080:80 --name api --mount src=mysqldata,dst=/var/lib/mysql --network mysqlnetwork termisfa/genericapi



#-----------------
#CREATE VOLUME. CHECK IF IS CREATED FIRST WITH  docker volume ls
#docker volume prune    
#docker volume create mysqldata

#-----------------
#CREATE NETOWRK. CHECK IF IS CREATED FIRST WITH  docker network ls
#docker network prune    
#docker network create mysqlnetwork

#-----------------
#OPEN MYSQL SERVER
#docker run -d -p 33060:3306 --name mysqlserver -e MYSQL_ROOT_PASSWORD=secret --network mysqlnetwork --mount src=mysqldata,dst=/var/lib/mysql biarms/mysql

#-------------------
#ENTER MYSQL SHELL
#docker exec -it mysqlserver mysql -p

#-------------------
#ENTER TO CONTAINER
#docker exec -it mysqlserver /bin/bash
#docker exec -it api /bin/bash
#docker exec -it cryptoalerts sh

