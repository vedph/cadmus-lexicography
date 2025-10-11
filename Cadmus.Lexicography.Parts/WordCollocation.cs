using System.Collections.Generic;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A words collocation pattern.
/// </summary>
public class WordCollocation
{
    /// <summary>
    /// The pattern frequency rank. Lower is more frequent.
    /// Zero means unknown or not specified.
    /// </summary>
    public short Rank { get; set; }

    /// <summary>
    /// The pattern's tokens.
    /// </summary>
    public List<WordCollocationToken> Tokens { get; set; } = [];

    /// <summary>
    /// The pattern's examples.
    /// </summary>
    public List<WordCollocationExample>? Examples { get; set; }

    /// <summary>
    /// A note about this pattern.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string representation of this instance.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        if (Rank > 0) sb.Append($"({Rank}) ");
        sb.Append(string.Join(" ", Tokens));
        return sb.ToString();
    }
}
