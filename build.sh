#!/bin/bash
set -e
# restore dependencies and build
dotnet build

# remove output folder for publishing
rm -rf $(pwd)/out

# publish main project
dotnet publish DbUp.Migration.Runner/DbUp.Migration.Runner.csproj -c release -o $(pwd)/out
