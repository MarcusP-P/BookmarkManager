﻿using System.Diagnostics.CodeAnalysis;

namespace BookmarkManagerCore.Model;

/// <summary>
/// A bookmark that contains all the relevent information
/// </summary>
public class Bookmark
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
    /// Folder containign this bookmark
    /// </summary>
    public BookmarkFolder Parent { get; set; } = null!;

    /// <summary>
    /// Initialise the bookmark
    /// </summary>
    public Bookmark()
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
    [SuppressMessage(
        "Style",
        "IDE0046:Convert to conditional expression",
        Justification = "Simplifying it makes it harder to read")]
    public bool CompareURL(Bookmark? other)
    {
        if (other is null)
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
