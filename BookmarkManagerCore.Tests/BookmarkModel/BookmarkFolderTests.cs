using BookmarkManagerCore.BookmarkModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookmarkManagerCore.Tests.BookmarkModel;

/// <summary>
/// Test the Bookmark Folder
/// </summary>
public class BookmarkFolderTests
{
    public BookmarkFolderTests()
    {

    }

    /// <summary>
    /// Test that adding a bookmark to the folder correctly adds it to an empty collection
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmark_AddBookmarkToEmpty()
    {
        var bookmark = new Bookmark
        {
            Title = "Apple",
            Url = new Uri("http://apple.com"),
        };

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmark(bookmark);

        Assert.NotNull(bookmarkFolder.Bookmarks);
        Assert.Collection(bookmarkFolder.Bookmarks,
            item => Assert.Equal(bookmark, item));
    }

    /// <summary>
    /// Test adding the bookmark to a folder that has existing bookmarks
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmark_AddBookmarkToExisting()
    {
        var bookmark = new Bookmark
        {
            Title = "Apple",
            Url = new Uri("http://apple.com"),
        };

        var bookmarkFolder = new BookmarkFolder
        {
            Bookmarks = new List<Bookmark>
            {
                new Bookmark
                {
                    Title="Foo",
                    Url=new Uri("http://foo.com/"),
                },
            },
        };

        bookmarkFolder.AddBookmark(bookmark);

        Assert.NotNull(bookmarkFolder.Bookmarks);
        Assert.Collection(bookmarkFolder.Bookmarks,
            item => Assert.Equal("Foo", item.Title),
            item => Assert.Equal(bookmark, item));
    }

    /// <summary>
    /// Ensure that the bookmark's parent is correctly set
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmark_VerifyParentIsSet()
    {
        var bookmark = new Bookmark
        {
            Title = "Apple",
            Url = new Uri("http://apple.com"),
        };

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmark(bookmark);

        Assert.NotNull(bookmark.Parent);
        Assert.Equal(bookmark.Parent, bookmarkFolder);
    }

    /// <summary>
    /// Test adding a new child bookmarkFolder gets added to the list if there are no children
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmarkFolder_AddBookmarkFolderToEmpty()
    {
        var newBookmarkFolder = new BookmarkFolder
        {
            Title = "BookmarkFolder",
        };

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmarkFolder(newBookmarkFolder);

        Assert.NotNull(bookmarkFolder.BookmarkFolders);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal(newBookmarkFolder, item));
    }

    /// <summary>
    /// Make sure that if there are folders that already exist, they are added without disturbing the rest of the items
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmarkFolder_AddBookmarkFolderToExistingList()
    {
        var newBookmarkFolder = new BookmarkFolder
        {
            Title = "BookmarkFolder",
        };

        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkFolders = new List<BookmarkFolder>
            {
                new BookmarkFolder
                {
                    Title="Foo",
                },
            },
        };

        bookmarkFolder.AddBookmarkFolder(newBookmarkFolder);

        Assert.NotNull(bookmarkFolder.BookmarkFolders);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal("Foo", item.Title),
            item => Assert.Equal(newBookmarkFolder, item));
    }

    /// <summary>
    /// Ensure that the parent is correctly set when we add a child BookmarkFolder
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmarkFolder_VerifyParentIsSet()
    {
        var newBookmarkFolder = new BookmarkFolder
        {
            Title = "Apple",
        };

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmarkFolder(newBookmarkFolder);

        Assert.NotNull(newBookmarkFolder.Parent);
        Assert.Equal(newBookmarkFolder.Parent, bookmarkFolder);
    }

    /// <summary>
    /// Search for an existing Bookmark Folder by name
    /// </summary>
    [Fact]
    public void BookmarkFolderGetBookmarkFolder_FolderExists()
    {
        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkFolders = new List<BookmarkFolder>
            {
                new BookmarkFolder
                {
                    Title = "Foo",
                },
            }
        };

        var result = bookmarkFolder.GetBookmarkFolder("Foo");
        Assert.Equal("Foo", result.Title);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal("Foo", item.Title));
    }

    /// <summary>
    /// Test creation of a new child folder if it doesn't exist
    /// </summary>
    [Fact]
    public void BookmarkFolderGetBookmarkFolder_FolderDoesNotExist()
    {
        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkFolders = new List<BookmarkFolder>(),
        };

        var result = bookmarkFolder.GetBookmarkFolder("Foo");
        Assert.Equal("Foo", result.Title);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal("Foo", item.Title));
    }
}
