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
    /// Test the setter
    /// </summary>
    [Fact]
    public void BookmarkFolderTitle_Set()
    {
        var bookmarkFolder = new BookmarkFolder
        {
            Title = "Foo"
        };

        Assert.Equal("Foo", bookmarkFolder.titleBacking);
    }

    /// <summary>
    /// Test the setter
    /// </summary>
    [Fact]
    public void BookmarkFolderTitle_Set_WhiteSpace()
    {
        var bookmarkFolder = new BookmarkFolder
        {
            Title = "Foo "
        };

        Assert.Equal("Foo", bookmarkFolder.titleBacking);
    }

    /// <summary>
    /// Test the getter
    /// </summary>
    [Fact]
    public void BookmarkFolderTitle_Get()
    {
        var bookmarkFolder = new BookmarkFolder
        {
            titleBacking = "Foo"
        };

        Assert.Equal("Foo", bookmarkFolder.Title);
    }

    /// <summary>
    /// Test adding the bookmark to a folder that has existing bookmarks
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmark_AddBookmarkToExisting()
    {
        var newBookmarkStub = new Mock<IBookmark>().Object;
        var exisitingBookmarkStub = new Mock<IBookmark>().Object;

        var bookmarkFolder = new BookmarkFolder
        {
            Bookmarks = new List<IBookmark>
            {
                exisitingBookmarkStub,
            },
        };

        bookmarkFolder.AddBookmark(newBookmarkStub);

        Assert.NotNull(bookmarkFolder.Bookmarks);
        Assert.Collection(bookmarkFolder.Bookmarks,
            item => Assert.Equal(exisitingBookmarkStub, item),
            item => Assert.Equal(newBookmarkStub, item));
    }

    /// <summary>
    /// Ensure that the bookmark's parent is correctly set
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmark_VerifyParentIsSet()
    {
        var bookmarkMock = new Mock<IBookmark>();

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmark(bookmarkMock.Object);

        bookmarkMock.VerifySet(x => x.Parent = bookmarkFolder, Times.Once);
    }

    /// <summary>
    /// Test adding a new child bookmarkFolder gets added to the list if there are no children
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmarkFolder_AddBookmarkFolderToEmpty()
    {
        var bookmarkFolderStub = new Mock<IBookmarkFolder>().Object;

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmarkFolder(bookmarkFolderStub);

        Assert.NotNull(bookmarkFolder.BookmarkFolders);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal(bookmarkFolderStub, item));
    }

    /// <summary>
    /// Make sure that if there are folders that already exist, they are added without disturbing the rest of the items
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmarkFolder_AddBookmarkFolderToExistingList()
    {
        var newBookmarkFolderStub = new Mock<IBookmarkFolder>().Object;
        var existingBookmarkFolderStub = new Mock<IBookmarkFolder>().Object;

        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkFolders = new List<IBookmarkFolder>
            {
                existingBookmarkFolderStub,
            },
        };

        bookmarkFolder.AddBookmarkFolder(newBookmarkFolderStub);

        Assert.NotNull(bookmarkFolder.BookmarkFolders);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal(existingBookmarkFolderStub, item),
            item => Assert.Equal(newBookmarkFolderStub, item));
    }

    /// <summary>
    /// Ensure that the parent is correctly set when we add a child BookmarkFolder
    /// </summary>
    [Fact]
    public void BookmarkFolderAddBookmarkFolder_VerifyParentIsSet()
    {
        var newBookmarkFolderMock = new Mock<IBookmarkFolder>();

        var bookmarkFolder = new BookmarkFolder();

        bookmarkFolder.AddBookmarkFolder(newBookmarkFolderMock.Object);

        newBookmarkFolderMock.VerifySet(x => x.Parent = bookmarkFolder);
    }

    /// <summary>
    /// Search for an existing Bookmark Folder by name
    /// </summary>
    [Fact]
    public void BookmarkFolderGetBookmarkFolder_FolderExists()
    {
        var bookmarkFolderMock = new Mock<IBookmarkFolder>();

        _ = bookmarkFolderMock.Setup(x => x.Title)
            .Returns("Foo");

        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkFolders = new List<IBookmarkFolder>
            {
                bookmarkFolderMock.Object,
            }
        };

        var result = bookmarkFolder.GetBookmarkFolder("Foo");
        Assert.Equal(bookmarkFolderMock.Object, result);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal(bookmarkFolderMock.Object, item));
        _ = Assert.Single(bookmarkFolder.BookmarkFolders);
    }

    /// <summary>
    /// Search for an existing Bookmark Folder by name
    /// </summary>
    [Fact]
    public void BookmarkFolderGetBookmarkFolder_FolderExistsWhitespaceInSearch()
    {
        var bookmarkFolderMock = new Mock<IBookmarkFolder>();

        _ = bookmarkFolderMock.Setup(x => x.Title)
            .Returns("Foo");

        var bookmarkFolder = new BookmarkFolder
        {
            BookmarkFolders = new List<IBookmarkFolder>
            {
                bookmarkFolderMock.Object,
            }
        };

        var result = bookmarkFolder.GetBookmarkFolder("Foo ");
        Assert.Equal(bookmarkFolderMock.Object, result);
        Assert.Collection(bookmarkFolder.BookmarkFolders,
            item => Assert.Equal(bookmarkFolderMock.Object, item));
        _ = Assert.Single(bookmarkFolder.BookmarkFolders);
    }

    /// <summary>
    /// Test creation of a new child folder if it doesn't exist
    /// </summary>
    [Theory()]
    [InlineData("Foo", "Foo")]
    [InlineData("Foo ", "Foo")]
    public void BookmarkFolderGetBookmarkFolder_FolderDoesNotExist(string request, string expected)
    {
        var bookmarkFolderMock = new Mock<BookmarkFolder>();

        _ = bookmarkFolderMock.Setup(d => d.GetBookmarkFolder(It.IsAny<string>()))
            .CallBase();

        // When the base method calls this.GetBookmarkFolder, return our inner mocked object.
        _ = bookmarkFolderMock.Setup(d => d.AddBookmarkFolder(It.IsAny<IBookmarkFolder>()));
        // _ = bookmarkFolderMock.Setup(x => x.BookmarkFolders)
        //    .Returns(new List<IBookmarkFolder>());

        var bookmarkFolder = bookmarkFolderMock.Object;

        var result = bookmarkFolder.GetBookmarkFolder(request);

        Assert.Equal(expected, result.Title);
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
