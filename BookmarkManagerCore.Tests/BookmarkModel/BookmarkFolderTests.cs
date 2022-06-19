using BookmarkManagerCore.BookmarkModel;
using Moq;
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

    [Fact()]
    public void AddBookmarkWithPathTest_EmptyPath()
    {
        var bookmarkFolder = new BookmarkFolder();
        var bookmarkStub = new Mock<IBookmark>().Object;

        bookmarkFolder.AddBookmarkWithPath(Array.Empty<string>(), bookmarkStub);

        Assert.Collection(bookmarkFolder.Bookmarks,
            item => Assert.Equal(item, bookmarkStub));
    }

    [Theory()]
    [InlineData(new string[] { "path" }, "path", new string[] { })]
    [InlineData(new string[] { "path1", "path2" }, "path1", new string[] { "path2" })]
    [InlineData(new string[] { "path1", "path2", "path3" }, "path1", new string[] { "path2", "path3" })]
    public void AddBookmarkWithPathTest_MultiplePathsRemaining(string[] path, string newFolder, string[] checkPath)
    {
        // Mock the top level object that we wil call.
        var bookmarkFolderMock = new Mock<BookmarkFolder>();

        // Mock the object that gets returned when the top level one calls GetBookmarkFolder
        // We will use this to confirm the recursive call is called correctly
        var bookmarkFolderInternalMock = new Mock<IBookmarkFolder>();

        // We don't care about the bookmark
        var bookmarkStub = new Mock<IBookmark>().Object;

        // When we call the top level path, we want to call the real method.
        _ = bookmarkFolderMock.Setup(d => d.AddBookmarkWithPath(It.IsAny<string[]>(), It.IsAny<IBookmark>()))
            .CallBase();

        // When the base method calls this.GetBookmarkFolder, return our inner mocked object.
        _ = bookmarkFolderMock.Setup(d => d.GetBookmarkFolder(It.IsAny<string>()))
            .Returns(bookmarkFolderInternalMock.Object);

        // The second level object, we will jsut want to no-op
        _ = bookmarkFolderInternalMock.Setup(d => d.AddBookmarkWithPath(It.IsAny<string[]>(), It.IsAny<IBookmark>()));

        // Call the top level with the path
        bookmarkFolderMock.Object.AddBookmarkWithPath(path, bookmarkStub);

        // Verify that GetBookmarkFolder is called with the correct folder
        bookmarkFolderMock.Verify(d => d.GetBookmarkFolder(newFolder), Times.Once);

        // Verify that the recursive call to AddBookmarkWithPath is called with the correct remaining paths
        bookmarkFolderInternalMock.Verify(d => d.AddBookmarkWithPath(checkPath, It.IsAny<IBookmark>()),
            Times.Once);
    }
}
