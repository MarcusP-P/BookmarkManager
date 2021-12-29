using BookmarkManagerCore.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookmarkManagerCore.Tests.Model;

public class BookmarkFolderTests
{
    public BookmarkFolderTests()
    {

    }

    [Fact]
    public void BookmarkFolderAddBookmark_AddBookmarkToEmpty()
    {
        var bookmark = new BookmarkNode
        {
            Title = "Apple",
            Url = new Uri("http://apple.com"),
        };

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmark(bookmark);

        Assert.NotNull(bookmarkFolder.BookmarkNodes);
        Assert.Collection(bookmarkFolder.BookmarkNodes,
            item => Assert.Equal(bookmark, item));
    }

    [Fact]
    public void BookmarkFolderAddBookmark_AddBookmarkToExisting()
    {
        var bookmark = new BookmarkNode
        {
            Title = "Apple",
            Url = new Uri("http://apple.com"),
        };

        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkNodes = new List<BookmarkNode>
            {
                new BookmarkNode
                {
                    Title="Foo",
                    Url=new Uri("http://foo.com/"),
                },
            },
        };

        bookmarkFolder.AddBookmark(bookmark);

        Assert.NotNull(bookmarkFolder.BookmarkNodes);
        Assert.Collection(bookmarkFolder.BookmarkNodes,
            item => Assert.Equal("Foo", item.Title),
            item => Assert.Equal(bookmark, item));
    }

    [Fact]
    public void BookmarkFolderAddBookmark_VerifyParentIsSet()
    {
        var bookmark = new BookmarkNode
        {
            Title = "Apple",
            Url = new Uri("http://apple.com"),
        };

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmark(bookmark);

        Assert.NotNull(bookmark.Parent);
        Assert.Equal(bookmark.Parent, bookmarkFolder);
    }

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
