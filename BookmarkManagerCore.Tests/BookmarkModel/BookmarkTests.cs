using BookmarkManagerCore.BookmarkModel;
using Moq;
using System;
using Xunit;

namespace BookmarkManagerCore.Tests.BookmarkModel;

/// <summary>
/// Test Bookmarks
/// </summary>
public class BookmarkTests
{
    public BookmarkTests()
    {

    }

    [Fact()]
    public void BookmarkParent_GetSet()
    {
        var parentBookmarkFolderStub = new Mock<IBookmarkFolder>().Object;
        var bookmark = new Bookmark();
        Assert.Null(bookmark.Parent);
        bookmark.Parent = parentBookmarkFolderStub;
        Assert.Equal(parentBookmarkFolderStub, bookmark.Parent);
    }

    /// <summary>
    /// Test the setter
    /// </summary>
    [Theory]
    [InlineData("Foo", "Foo")]
    [InlineData("Foo ", "Foo")]
    public void BookmarkTitle_Set(string request, string expected)
    {
        var bookmarkFolder = new BookmarkFolder
        {
            Title = request
        };

        Assert.Equal(expected, bookmarkFolder.titleBacking);
    }

    /// <summary>
    /// Test the getter
    /// </summary>
    [Fact]
    public void BookmarkFolderTitle_Get()
    {
        var bookmark = new Bookmark
        {
            titleBacking = "Foo"
        };

        Assert.Equal("Foo", bookmark.Title);
    }

    /// <summary>
    /// Two bookmarks are equal. Should return true.
    /// </summary>
    [Theory]
    [InlineData("Title1", "http://apple.com/", "Title1", "http://apple.com/")]
    [InlineData("Title1", "http://apple.com/", "Title2", "http://apple.com/")]
    [InlineData("Title1", "http://apple.com/", "Title1", "https://apple.com/")]
    [InlineData("Title1", "http://apple.com/", "Title1", "https://Apple.com/")]
    [InlineData("Title1", "http://apple.com/", "Title1", "http://apple.com")]
    [InlineData("Title1", "http://apple.com/home/Apple", "Title1", "http://apple.com/home/Apple")]
    [InlineData("Title1", "http://apple.com/home/Apple", "Title1", "http://apple.com/home/Apple#Foo")]
    [InlineData("Title1", "http://Me:you@apple.com/home/Apple", "Title2", "http://Me:you@apple.com/home/Apple")]
    public void BookmarkEquals_ReturnTrue(string Bookmark1Title,
        string Bookmark1Url,
        string Bookmark2Title,
        string Bookmark2Url)
    {
        var a = new Bookmark
        {
            Title = Bookmark1Title,
            Url = new Uri(Bookmark1Url),
        };
        var b = new Bookmark
        {
            Title = Bookmark2Title,
            Url = new Uri(Bookmark2Url),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Test for two unequal bookmarks. Should return false.
    /// </summary>
    [Theory]
    [InlineData("Title1", "http://apple.com/", "Title2", "http://microsoft.com")]
    [InlineData("Title1", "http://apple.com/", "Title1", "http://microsoft.com")]
    [InlineData("Title1", "http://apple.com/home/Apple", "Title1", "http://apple.com/home/apple")]
    [InlineData("Title1", "http://me:you@apple.com/home/Apple", "Title1", "http://apple.com/home/Apple")]
    [InlineData("Title1", "http://me:you@apple.com/home/Apple", "Title1", "http://you:me@apple.com/home/Apple")]
    [InlineData("Title1", "http://Me:you@apple.com/home/Apple", "Title1", "http://me:you@apple.com/home/Apple")]
    public void BookmarkEquals_ReturnFalse(string Bookmark1Title,
        string Bookmark1Url,
        string Bookmark2Title,
        string Bookmark2Url)
    {
        var a = new Bookmark
        {
            Title = Bookmark1Title,
            Url = new Uri(Bookmark1Url),
        };
        var b = new Bookmark
        {
            Title = Bookmark2Title,
            Url = new Uri(Bookmark2Url),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Compare with null. Should return false
    /// </summary>
    [Fact]
    public void BookmarkEquals_ReturnFalseNull()
    {
        var a = new Bookmark
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        bool result = a.CompareURL(null);

        Assert.False(result);
    }
}

