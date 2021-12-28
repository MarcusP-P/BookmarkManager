// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0007:Use implicit type", Justification = "Force returns to types for unit tests.", Scope = "namespaceanddescendants", Target = "~N:BookmarkManagerCore.Tests")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Allow this in tests", Scope = "namespaceanddescendants", Target = "~N:BookmarkManagerCore.Tests")]
