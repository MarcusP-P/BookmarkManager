namespace BookmarkManagerCore.BookmarkModel;

public interface IBookmarkFolder
{
    IEnumerable<IBookmarkFolder>? BookmarkFolders { get; set; }
    IEnumerable<IBookmark> Bookmarks { get; set; }
    IBookmarkFolder? Parent { get; set; }
    string Title { get; set; }

    void AddBookmark(IBookmark bookmark);
    void AddBookmarkFolder(IBookmarkFolder bookmarkFolder);
    void AddBookmarkWithPath(string[] path, IBookmark bookmark);
    IBookmarkFolder CreateNewBookmark(string folderName);
    IBookmarkFolder GetBookmarkFolder(string folderName);
}
