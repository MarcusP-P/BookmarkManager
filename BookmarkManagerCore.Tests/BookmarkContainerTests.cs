using Xunit;

namespace BookmarkManagerCore.Tests;

public class BookmarkContainerTests
{
    // Get a new bookmark container
    [Fact()]
    public void GetGetBookmarkContainer()
    {
        var bookmarkContainer = BookmarkContainer.GetBookmarkContainer();
        Assert.NotNull(bookmarkContainer);
        _ = Assert.IsType<BookmarkContainer>(bookmarkContainer);
    }
}
