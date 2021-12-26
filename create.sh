#! /bin/sh

rm -rf BookmarkManagerCore
rm -rf BookmarkManagerCore.Tests
rm -rf BookmarkManagerTerminal
rm -rf BookmarkManager.sln

rm -rf packages

echo Creating core library
dotnet new classlib -n BookmarkManagerCore

echo Creating core test library
dotnet new xunit -n BookmarkManagerCore.Tests
dotnet add BookmarkManagerCore.Tests reference BookmarkManagerCore

echo Creating Terminal Project
dotnet new console --name BookmarkManagerTerminal
dotnet add BookmarkManagerTerminal package Terminal.Gui
dotnet add BookmarkManagerTerminal reference BookmarkManagerCore

dotnet new sln --name BookmarkManager
dotnet sln add BookmarkManagerTerminal
dotnet sln add BookmarkManagerCore
dotnet sln add BookmarkManagerCore.Tests

