using BookmarkManagerCore.Model;
using System;
using Xunit;

namespace BookmarkManagerCore.Tests.Model;

/// <summary>
/// Test Bookmark Nodes
/// </summary>
public class BookmarkNodeTests
{
    public BookmarkNodeTests()
    {

    }

    /// <summary>
    /// Test for two unequal bookmarks. Should return false.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalse()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com/"),
        };
        var b = new BookmarkNode
        {
            Title = "Title2",
            Url = new Uri("http://microsoft.com"),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Test for two different URLs but identical titles. Should retun false.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalseTitlesSame()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("http://microsoft.com"),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Compare with null. Should return false
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalseNull()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        bool result = a.CompareURL(null);

        Assert.False(result);
    }

    /// <summary>
    /// Two bookmarks are equal. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrue()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = a.Url,
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueDifferentTitle()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = "Title2",
            Url = a.Url,
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs, different protocol. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueDifferentProtocol()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com"),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs, different case on host. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueDifferentHostCase()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://Apple.com"),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs, one has trailing slash. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueTrailingSlash()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com/"),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs, one has trailing dot. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueTrailingDot()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com."),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs with a path. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueWithPath()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com/home/Apple"),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles but identical URLs, one has trailing dot. Should return true.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueHashUrl()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com/home/Apple#Foo"),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }

    /// <summary>
    /// Different titles with a change in case of the Path.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalsePathCase()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com/home/apple"),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Different titles with a change in case of the Path.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalseMissingUserComponent()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://Me:you@apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://apple.com/home/Apple"),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Different titles with a change in UserCompoinent.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalseUserComponentDiffers()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://Me:you@apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://you:me@apple.com/home/Apple"),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Different titles with a change in case of the User Component.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnFalseUserComponentCase()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://Me:you@apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://me:you@apple.com/home/Apple"),
        };

        bool result = a.CompareURL(b);

        Assert.False(result);
    }

    /// <summary>
    /// Different titles with a a URL containing a User Component.
    /// </summary>
    [Fact]
    public void BookmarkNodeEquals_ReturnTrueUserComponent()
    {
        var a = new BookmarkNode
        {
            Title = "Title1",
            Url = new Uri("http://Me:you@apple.com/home/Apple"),
        };
        var b = new BookmarkNode
        {
            Title = a.Title,
            Url = new Uri("https://Me:you@apple.com/home/Apple"),
        };

        bool result = a.CompareURL(b);

        Assert.True(result);
    }
}

