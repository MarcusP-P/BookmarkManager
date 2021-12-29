namespace BookmarkManagerCore.Model;

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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The list of bookmarks in this folder
    /// </summary>
    /// <remarks>
    /// This is only the list of bookmarks in this current folder, not those in
    /// sub-folders of this folder.
    /// 
    /// This is public because we will use EF Core in the future.
    /// </remarks>
    public IEnumerable<BookmarkNode> BookmarkNodes { get; set; } = null!;

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
}

