namespace BookmarkManagerCore.BookmarkModel;

public interface IBookmark
{
    IBookmarkFolder? Parent { get; set; }
    string Title { get; set; }
    Uri Url { get; set; }

    bool CompareURL(Bookmark? other);
}
