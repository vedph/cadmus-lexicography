using System.Collections.Generic;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A single example illustrating a word's sense.
/// </summary>
public class WordSenseExample
{
    /// <summary>
    /// Tags, e.g. <c>archaic</c>, <c>informal</c>, etc. Usually from
    /// hierarchical thesaurus <c>lex-word-sense-tags</c>. Related: TEI
    /// <c>usg</c>, Lemon <c>usage</c>, <c>context</c>.
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// The example text.
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// The optional explanation or translation of the example.
    /// </summary>
    public string? Explanation { get; set; }

    /// <summary>
    /// The optional citation for the example. This usually follows some
    /// conventional citation scheme.
    /// </summary>
    public string? Citation { get; set; }

    /// <summary>
    /// A general note about this example. Related to TEI <c>note</c>.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string representation of the example.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        if (Tags?.Count > 0)
            sb.Append('[').Append(string.Join(',', Tags)).Append("] ");
        sb.Append(Text);
        if (!string.IsNullOrEmpty(Explanation))
            sb.Append(" (").Append(Explanation).Append(')');

        if (!string.IsNullOrEmpty(Citation))
            sb.Append(" [").Append(Citation).Append(']');

        return sb.ToString();
    }
}
