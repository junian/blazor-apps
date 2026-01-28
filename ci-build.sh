#!/usr/bin/env bash

DOTNET_VERSION=10.0

curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh

./dotnet-install.sh -c $DOTNET_VERSION -InstallDir ./dotnet
./dotnet/dotnet --version
./dotnet/dotnet publish \
    src/BlazorApps/BlazorApps.csproj \
    -c Release \
    -o publish
