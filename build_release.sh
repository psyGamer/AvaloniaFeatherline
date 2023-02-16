#!/bin/sh

rm -rd bin/Release

dotnet publish -p:RID=win-x64 -c Release
dotnet publish -p:RID=osx-x64 -c Release
dotnet publish -p:RID=linux-x64 -c Release

pushd bin/Release/net6.0

# Windows
pushd win-x64/publish
zip Featherline_Windows-x64.zip **
mv Featherline_Windows-x64.zip ../../..
popd

# MacOS
pushd osx-x64/publish
zip Featherline_MacOS-x64.zip **
mv Featherline_MacOS-x64.zip ../../..
popd

# Linux
pushd linux-x64/publish
zip Featherline_Linux-x64.zip **
mv Featherline_Linux-x64.zip ../../..
popd

popd