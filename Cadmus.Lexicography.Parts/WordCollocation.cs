using System.Collections.Generic;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A words collocation pattern. This is a sequence of tokens, each
/// representing the word class (part of speech) for each word building
/// the collocation pattern. For example, a collocation like
/// "strong tea" would have the pattern ADJ+NOUN. Usually one word in the
/// pattern is the head token, because it is the lexical entry word
/// including this collocation. So, if the lexical entry word is "tea",
/// then the 2nd token (NOUN) is the head.
/// <para>Examples then provide real-world sentences illustrating the use
/// of the collocation. These include 1 literal per token (each having the
/// ordinal number of the token it refers to in the pattern), plus eventual
/// connective text represented by literals whose token number is 0.</para>
/// </summary>
public class WordCollocation
{
    /// <summary>
    /// A generic tag for this pattern. For instance, it could be used to
    /// target a specific sense of the head word.
    /// </summary>
    public string? Tag { get; set; }

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
        if (!string.IsNullOrEmpty(Tag)) sb.Append($"{Tag}: ");
        if (Rank > 0) sb.Append($"({Rank}) ");
        sb.Append(string.Join(" ", Tokens));
        return sb.ToString();
    }
}
