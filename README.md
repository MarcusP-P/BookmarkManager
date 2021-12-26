# BookmarkManager
Manage bookmarks (Duplicates, merging files, etc.)

## Re-building templates

The following commands can be used to re-build the projects:

* Core library: `dotnet new classlib -n BookmarkManagerCore`
* Core library test: 
    * `dotnet new xunit -n BookmarkManagerCore.Tests`
    * `dotnet add BookmarkManagerCore.Tests reference BookmarkManagerCore`
* The terminal UI:
    * `dotnet new console --name BookmarkManagerTerminal`
    * `dotnet add BookmarkManagerTerminal package Terminal.Gui`
    * `dotnet add BookmarkManagerTerminal reference BookmarkManagerCore`
* Solution file: `dotnet new sln --name BookmarkManager`
* Add projects to solution:
    * `dotnet sln add BookmarkManagerTerminal`
    * `dotnet sln add BookmarkManagerCore`
    * `dotnet sln add BookmarkManagerCore.Tests`

## Dealing with UTF8 BOM

Some of the .Net generated template files have a pesky UTF* BOM at the start of the file. To find these, you can use `find . -type f -exec grep -Hl "^$(printf '\xef\xbb\xbf')" {} \;`. To remove them, use `find . -type f -exec sed -i  "s/^$(printf '\xef\xbb\xbf')//" {} \;`, but make sure you don't touch the .git directory...