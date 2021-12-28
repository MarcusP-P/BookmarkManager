namespace BookmarkManagerCore.Models;

/// <summary>
/// A node that defines a node that contains the bookmarks
/// </summary>
public class BookmarkNode
{
    /// <summary>
    /// The URL of the bookmark
    /// </summary>
    public Uri Url { get; set; } = default!;

    /// <summary>
    /// The title of the bookmark
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Initialise the node
    /// </summary>
    public BookmarkNode()
    {

    }

    /// <summary>
    /// Conpare another bookmark's has the same URL
    /// </summary>
    /// <remarks>
    /// A bookmark is considered equal if the host, path, query and User info
    /// are the same. The host portion is case insentitive, but the rest of the
    /// path, query and User info are case sensitive.
    /// </remarks>
    /// <param name="other">The other bookmark to com pare</param>
    /// <returns>true if the two bookmarks are equal</returns>
    /// <exception cref="NotImplementedException"></exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Simplifying it makes it harder to read")]
    public bool CompareURL(BookmarkNode? other)
    {
        if (other == null)
        {
            return false;
        }

        return Uri.Compare(this.Url,
            other.Url,
                UriComponents.NormalizedHost
                | UriComponents.Path
                | UriComponents.Query
                | UriComponents.UserInfo,
            UriFormat.Unescaped,
            StringComparison.CurrentCulture) == 0;
    }
}

