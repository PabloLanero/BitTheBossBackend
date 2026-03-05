#!/bin/bash
rm -rf ./Migrations
dotnet ef migrations add InitialCreate -p ./API.csproj -s ./API.csproj
dotnet ef database update -p ./API.csproj -s ./API.csproj