using System.Collections.Generic;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A token in a word collocation pattern.
/// </summary>
public class WordCollocationToken
{
    /// <summary>
    /// The POS tag of the token, usually from hierarchical thesaurus
    /// <c>lex-word-ctoken-pos-tags</c>.
    /// </summary>
    public string Pos { get; set; } = "";

    /// <summary>
    /// Optional features of the token, usually from hierarchical thesaurus
    /// <c>lex-word-ctoken-features</c>.
    /// </summary>
    public List<string>? Features { get; set; }

    /// <summary>
    /// True if this token is the head of the collocation pattern.
    /// </summary>
    public bool IsHead { get; set; }

    /// <summary>
    /// True if this token is optional in the collocation pattern.
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// An optional note about this token.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string representation of this instance.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        if (IsOptional) sb.Append('[');
        if (IsHead) sb.Append('*');
        sb.Append(Pos);
        if (Features != null && Features.Count > 0)
            sb.Append($"({string.Join(",", Features)})");
        if (IsOptional) sb.Append(']');
        return sb.ToString();
    }
}
