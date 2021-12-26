@echo off

echo Removing .vs
rd /s /q .vs

echo Removing packages
rd /s /q packages

echo Cleaning BookmarkManagerCore
rd /s /q BookmarkManagerCore\bin
rd /s /q BookmarkManagerCore\obj

echo Cleaning BookmarkManagerCore.Tests
rd /s /q BookmarkManagerCore.Tests\bin
rd /s /q BookmarkManagerCore.Tests\obj
