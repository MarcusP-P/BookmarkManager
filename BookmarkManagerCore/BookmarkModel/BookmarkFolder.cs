﻿namespace BookmarkManagerCore.BookmarkModel;

/// <summary>
/// A representation of a bookmark folder
/// </summary>
/// <remarks>
/// There is expected to be one folder at the root. All other folders will be
/// under that root folder
/// </remarks>
public class BookmarkFolder
{
    /// <summary>
    /// The name of the folder
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The list of bookmarks in this folder
    /// </summary>
    /// <remarks>
    /// This is only the list of bookmarks in this current folder, not those in
    /// sub-folders of this folder.
    /// 
    /// This is public because we will use EF Core in the future.
    /// </remarks>
    public IEnumerable<Bookmark> Bookmarks { get; set; } = null!;

    /// <summary>
    /// The list of sub folders
    /// </summary>
    /// <remarks>
    /// This is the list of subfolders directly from this folder. It only
    /// lists direct child folders. Grandchild folders will be under the child
    /// folders.
    /// 
    /// This is public because we will use EF Core in the future 
    /// </remarks>
    public IEnumerable<BookmarkFolder> BookmarkFolders { get; set; } = null!;

    /// <summary>
    /// The parent folder
    /// </summary>
    /// <remarks>
    /// If this is null, then we are the root folder. In all other cases,
    /// it contains the parent folder
    /// </remarks>
    public BookmarkFolder? Parent { get; set; }

    public BookmarkFolder()
    {
    }

    /// <summary>
    /// Add a bookmark to the bookmarks under this stage
    /// </summary>
    /// <remarks>
    /// This will also set the Bookmark's parent property.
    /// </remarks>
    /// <param name="bookmark">Bookmark to add to this folder</param>
    public void AddBookmark(Bookmark bookmark)
    {
        if (this.Bookmarks is null)
        {
            this.Bookmarks = new List<Bookmark>();
        }

        this.Bookmarks = this.Bookmarks.Append(bookmark);

        bookmark.Parent = this;
    }

    /// <summary>
    /// Add a child Bookmark folder
    /// </summary>
    /// <remarks>
    /// This will also correctly set the parent property</remarks>
    /// <param name="bookmarkFolder">The Bookmark folder to add</param>
    public void AddBookmarkFolder(BookmarkFolder bookmarkFolder)
    {
        if (this.BookmarkFolders is null)
        {
            this.BookmarkFolders = new List<BookmarkFolder>();
        }

        this.BookmarkFolders = this.BookmarkFolders.Append(bookmarkFolder);

        bookmarkFolder.Parent = this;
    }

    /// <summary>
    /// Gets the bookmark folder that is named in the paramater
    /// </summary>
    /// <remarks>
    /// If the folder does nto exist, it's created.</remarks>
    /// <param name="folderName">The name of the folder to search for.</param>
    /// <returns></returns>
    public BookmarkFolder GetBookmarkFolder(string folderName)
    {
        // Get the folder we're after
        var bookmarkFolder = this.BookmarkFolders.FirstOrDefault(
            x => x.Title == folderName);

        // If we haven't found it, it doesn't exist, so add it.
        if (bookmarkFolder is null)
        {
            bookmarkFolder = new BookmarkFolder
            {
                Title = folderName,
            };
            this.AddBookmarkFolder(bookmarkFolder);
        }

        return bookmarkFolder;
    }
}
